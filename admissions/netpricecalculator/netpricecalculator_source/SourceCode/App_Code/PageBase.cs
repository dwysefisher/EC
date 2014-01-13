using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using Inovas.Common;
using System.Text.RegularExpressions;

namespace Inovas.NetPrice
{
    public class PageBase : System.Web.UI.Page
    {
        #region Fields
        private AppContext _appContext = new AppContext();
        private string _sqlInsert = string.Format("INSERT INTO {0} (GUID, SessionKey, SessionXmlValue, Created) VALUES (@guid, @sessionKey, @sessionXmlValue, @created)", AppConfig.AspSessionTable);
        private string _sqlSelect = string.Format("SELECT SessionXmlValue FROM {0} WHERE GUID = @guid AND SessionKey=@sessionKey", AppConfig.AspSessionTable);
        private string _sqlDelete = string.Format("DELETE FROM {0} WHERE GUID = @guid", AppConfig.AspSessionTable);
        private static object _lock = new object();
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            // if session guid doesn't exist, create new
            CreateSessionGuid();
            // Load context from database
            LoadContext();           
        }

        /// <summary>
        /// Get session guid from cookie
        /// </summary>
        /// <returns></returns>
        protected string GetSessionGuid()
        {
            string _guid = GetCookieValue("SessionGuid");
            string returnValue = string.Empty;

            if (!string.IsNullOrEmpty(_guid))
            {
                Regex regex = new Regex("^[a-zA-Z0-9-]{36}$");
                if (regex.IsMatch(_guid))
                    returnValue = _guid;
            }
            return returnValue;
        }
        
        /// <summary>
        /// Create new session guid and save it to cookie
        /// </summary>
        /// <returns></returns>
        protected string CreateSessionGuid()
        {
            string guid = GetSessionGuid();
            if (string.IsNullOrEmpty(guid))
            {
                guid = Guid.NewGuid().ToString();
                SetCookieValue("SessionGuid", guid);
            }
            return guid;
        }

        /// <summary>
        /// Client code has access only to getter
        /// </summary>
        public AppContext AppContext
        {
            get { return _appContext; }
        }

        #region Session related methods

        /// <summary>
        /// Save AppContext into database
        /// </summary>
        protected void SaveContext()
        {
            lock (_lock)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppContext));
                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    xmlSerializer.Serialize(sw, _appContext);
                    sw.Flush();
                    sw.Close();
                    SaveAppContextToDatabase(GetSessionGuid(), "npAppContext", sb.ToString(), true);
                }
            }
        }

        /// <summary>
        /// Save xml string into database 
        /// </summary>
        /// <param name="sessionGuid"></param>
        /// <param name="sessionKey"></param>
        /// <param name="xmlString"></param>
        /// <param name="deleteExisting"></param>
        private void SaveAppContextToDatabase(string sessionGuid, string sessionKey, string xmlString, bool deleteExisting)
        {
            using (SqlConnection connection = new SqlConnection(AppConfig.DefaultConnectionString))
            {
                connection.Open();
                if (deleteExisting)
                {
                    using (SqlCommand command = new SqlCommand(_sqlDelete, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@guid", sessionGuid);
                        command.ExecuteNonQuery();
                    }
                }

                using (SqlCommand command = new SqlCommand(_sqlInsert, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@guid", sessionGuid);
                    command.Parameters.AddWithValue("@sessionKey", sessionKey);
                    command.Parameters.AddWithValue("@sessionXmlValue", xmlString);
                    command.Parameters.AddWithValue("@created", DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Load AppContext from database
        /// </summary>
        protected void LoadContext()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppContext));
            string xml = LoadAppContextFromDatabase(GetSessionGuid(), "npAppContext");
            if (!string.IsNullOrEmpty(xml))
            {
                using (TextReader rd = new StringReader(xml))
                {
                    AppContext tmpAppContext = (AppContext)xmlSerializer.Deserialize(rd);
                    if (tmpAppContext != null)
                    {
                        _appContext = tmpAppContext;
                    }
                }
            }
            else
            {
                // If there is no context in database, create new and save it to database
                _appContext = new AppContext();                
                SaveContext();
            }
        }

        /// <summary>
        /// Retrieves an object from DB session identified by the PageBase.SessionGuid and supplied key
        /// </summary>
        /// <param name="key">DB session key to retrieve the object</param>
        public string LoadAppContextFromDatabase(string sessionGuid, string sessionKey)
        {
            using (SqlConnection connection = new SqlConnection(AppConfig.DefaultConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(_sqlSelect, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@guid", sessionGuid);
                    command.Parameters.AddWithValue("@sessionKey", sessionKey);
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            return reader.IsDBNull(0) ? string.Empty : reader[0].ToString();
                        }
                    }
                }
            }
            return string.Empty;
        }

        #endregion

        #region Cookie related methods

        /// <summary>
        /// Retrieves string value from cookie
        /// </summary>
        /// <param name="key">Cookie key</param>        
        /// <returns>String value</returns>
        public string GetCookieValue(string cookieName)
        {
            HttpCookie cookie = this.Request.Cookies[cookieName];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                return (string)cookie.Value;
            else
                return null;
        }

        /// <summary>
        /// Saves string value to cookie.
        /// </summary>
        /// <param name="key">Cookie key</param>
        /// <param name="value">Object to be stored into cookie</param>
        /// <param name="context">HttpContext with Request/Response</param>
        public void SetCookieValue(string cookieName, string value)
        {
            HttpCookie cookie = new HttpCookie(cookieName, value);
            if (this.Request.IsSecureConnection == true)
                cookie.Secure = true;
            if (this.Request.Cookies[cookieName] != null)
                this.Response.Cookies.Remove(cookieName);
            this.Response.Cookies.Add(cookie);
        }

        #endregion
    }
}
