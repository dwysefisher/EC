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
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;

namespace Inovas.NetPrice
{
    [Serializable]
    public class AppContext: IXmlSerializable
    {
        #region Fields
        private XmlDocument _xmlDocument;
        private int _yearIndex = -1;
        private string _yearText = string.Empty;
        private InstitutionType _institutionType;       
        
        // Fields used when uploading XML files. These fields are not stored in database
        private string _institutionId;
        private string _institutionName;
        private string _institutionBanner;

        // Variables for Institution type: academic
        private bool? _institutionallyControlledHousingOffered = null;
        private bool? _studentsRequiredLiveOnCampusOrHousing = null;
        private bool? _institutionChargeDifferentTuition = null;
        private bool? _chargeForInDistrict = null;       
        private bool? _chargeForInState = null;
        private bool? _chargeOutOfState = null;

        // Variables for Institution type: program
        private int? _numberOfMonths = null;        
        private string _largestProgram = null;

        // variables to store user numbers
        private KeyValue[] _poaValues;       
        private KeyValue[] _tgaValues;

        // Percentage
        private int? _percentage;
        
        // Explanations
        private string _explanation1 = string.Empty;       
        private string _explanation2 = string.Empty;
        private string _explanation3 = string.Empty;

        private int _tuitionInDistrinct = 0;        
        private int _tuitionInState = 0;        
        private int _tuitionOutOfState = 0;        
        private int _tuitionAmount = 0;               

        private int _booksAndSupplies = 0;        

        private int _livingOnCampus = 0;        
        private int _livingOffCampus = 0;        
        private int _livingOffCampusWithFamily = 0;        

        private int _otherOnCampus = 0;        
        private int _otherOffCampus = 0;       
        private int _otherOffCampusWithFamily = 0;        
        
        #endregion

        #region Properties

        public InstitutionType InstitutionType
        {
            get { return _institutionType; }
            set { _institutionType = value; }
        }

        public int YearIndex
        {
            get { return _yearIndex; }
            set { _yearIndex = value; }
        }

        public string YearText
        {
            get { return _yearText; }
            set { _yearText = value; }
        }

        public string InstitutionId
        {
            get { return _institutionId; }
            set { _institutionId = value; }
        }

        public string InstitutionName
        {
            get { return _institutionName; }
            set { _institutionName = value; }
        }

        public string InstitutionBanner
        {
            get { return _institutionBanner; }
            set { _institutionBanner = value; }
        }




        /// <summary>
        /// Does your institution offer institutionally controlled housing?
        /// </summary>
        public bool? InstitutionallyControlledHousingOffered
        {
            get { return _institutionallyControlledHousingOffered; }
            set { _institutionallyControlledHousingOffered = value; }
        }

        /// <summary>
        /// Are all full-time, first-time degree/certificate seeking students required to live on campus or in institutionally-controlled housing?
        /// </summary>
        public bool? StudentsRequiredLiveOnCampusOrHousing
        {
            get { return _studentsRequiredLiveOnCampusOrHousing; }
            set { _studentsRequiredLiveOnCampusOrHousing = value; }
        }

        /// <summary>
        /// Does your institution charge different tuition for in-district, in-state, or out-of-state students?
        /// </summary>
        public bool? InstitutionChargeDifferentTuition
        {
            get { return _institutionChargeDifferentTuition; }
            set { _institutionChargeDifferentTuition = value; }
        }

        /// <summary>
        /// Charge for In-District
        /// </summary>
        public bool? ChargeForInDistrict
        {
            get { return _chargeForInDistrict; }
            set { _chargeForInDistrict = value; }
        }
        
        /// <summary>
        /// Charge for In-State
        /// </summary>        
        public bool? ChargeForInState
        {
            get { return _chargeForInState; }
            set { _chargeForInState = value; }
        }

        /// <summary>
        /// Charge for out-of-state
        /// </summary>
        public bool? ChargeForOutOfState
        {
            get { return _chargeOutOfState; }
            set { _chargeOutOfState = value; }
        }

        /// <summary>
        /// Average number of months it takes a full-time student to complete this program:
        /// </summary>
        public int? NumberOfMonths
        {
            get { return _numberOfMonths; }
            set { _numberOfMonths = value; }
        }

        /// <summary>
        /// Largest Program
        /// </summary>
        public string LargestProgram
        {
            get { return _largestProgram; }
            set { _largestProgram = value; }
        }

        /// <summary>
        /// User input for POA
        /// </summary>
        public KeyValue[] PoaValues
        {
            get { return this._poaValues; }
            set { this._poaValues = value; }
        }

        /// <summary>
        /// User input for TGA
        /// </summary>
        public KeyValue[] TgaValues
        {
            get { return this._tgaValues; }
            set { this._tgaValues = value; }
        }

        /// <summary>
        /// Percentage of all first-time, full-time students received any grants or scholarship aid
        /// </summary>
        public int? Percentage
        {
            get { return this._percentage; }
            set { this._percentage = value; }
        }

        /// <summary>
        /// Explanation 1
        /// </summary>
        public string Explanation1
        {
            get { return _explanation1; }
            set { _explanation1 = value; }
        }

        /// <summary>
        /// Explanation 2
        /// </summary>
        public string Explanation2
        {
            get { return _explanation2; }
            set { _explanation2 = value; }
        }

        /// <summary>
        /// Explanation 3
        /// </summary>
        public string Explanation3
        {
            get { return _explanation3; }
            set { _explanation3 = value; }
        }

        public int TuitionInDistrinct
        {
            get { return _tuitionInDistrinct; }
            set { _tuitionInDistrinct = value; }
        }
        public int TuitionInState
        {
            get { return _tuitionInState; }
            set { _tuitionInState = value; }
        }
        public int TuitionOutOfState
        {
            get { return _tuitionOutOfState; }
            set { _tuitionOutOfState = value; }
        }
        public int TuitionAmount
        {
            get { return _tuitionAmount; }
            set { _tuitionAmount = value; }
        }
        public int BooksAndSupplies
        {
            get { return _booksAndSupplies; }
            set { _booksAndSupplies = value; }
        }
        public int LivingOnCampus
        {
            get { return _livingOnCampus; }
            set { _livingOnCampus = value; }
        }
        public int LivingOffCampus
        {
            get { return _livingOffCampus; }
            set { _livingOffCampus = value; }
        }
        public int LivingOffCampusWithFamily
        {
            get { return _livingOffCampusWithFamily; }
            set { _livingOffCampusWithFamily = value; }
        }
        public int OtherOnCampus
        {
            get { return _otherOnCampus; }
            set { _otherOnCampus = value; }
        }
        public int OtherOffCampus
        {
            get { return _otherOffCampus; }
            set { _otherOffCampus = value; }
        }
        public int OtherOffCampusWithFamily
        {
            get { return _otherOffCampusWithFamily; }
            set { _otherOffCampusWithFamily = value; }
        }

        #endregion

        #region IXmlSerializable Members
        
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            _xmlDocument = new XmlDocument();
            _xmlDocument.LoadXml(reader.ReadOuterXml());

            YearIndex = int.Parse(ReadString("yearIndex"));
            YearText = ReadString("yearText");
            InstitutionType = NetPriceUtils.ParseInstitutionType(ReadString("institutionType"));

            InstitutionallyControlledHousingOffered = ReadBoolean("InstitutionallyControlledHousingOffered");
            StudentsRequiredLiveOnCampusOrHousing = ReadBoolean("StudentsRequiredLiveOnCampusOrHousing");
            InstitutionChargeDifferentTuition = ReadBoolean("InstitutionChargeDifferentTuition");
            ChargeForInDistrict = ReadBoolean("ChargeForInDistrict");
            ChargeForInState = ReadBoolean("ChargeForInState");
            ChargeForOutOfState = ReadBoolean("ChargeForOutOfState");

            NumberOfMonths = ReadInt32("NumberOfMonths");
            LargestProgram = ReadString("LargestProgram");

            PoaValues = ReadKeyValueArray("PoaValues");
            TgaValues = ReadKeyValueArray("TgaValues");

            Percentage = ReadInt32("Percentage");

            // For explanations, by default return empty strings
            Explanation1 = ReadString("Explanation1") ?? string.Empty;
            Explanation2 = ReadString("Explanation2") ?? string.Empty;
            Explanation3 = ReadString("Explanation3") ?? string.Empty;

            TuitionInDistrinct = ReadInt32("TuituinInDistrinct") ?? 0;
            TuitionInState = ReadInt32("TuituinInState") ?? 0;
            TuitionOutOfState = ReadInt32("TuituinOutOfState") ?? 0;
            TuitionAmount = ReadInt32("TuituinAmount") ?? 0;
            BooksAndSupplies = ReadInt32("BooksAndSupplies") ?? 0;
            LivingOnCampus = ReadInt32("LivingOnCampus") ?? 0;
            LivingOffCampus = ReadInt32("LivingOffCampus") ?? 0;
            LivingOffCampusWithFamily = ReadInt32("LivingOffCampusWithFamily") ?? 0;
            OtherOnCampus = ReadInt32("OtherOnCampus") ?? 0;
            OtherOffCampus = ReadInt32("OtherOffCampus") ?? 0;
            OtherOffCampusWithFamily = ReadInt32("OtherOffCampusWithFamily") ?? 0;
            
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("yearIndex", HttpUtility.HtmlEncode(YearIndex.ToString()));
            writer.WriteElementString("yearText", HttpUtility.HtmlEncode(YearText));
            writer.WriteElementString("institutionType", HttpUtility.HtmlEncode(Convert.ToInt32(InstitutionType).ToString()));

            if(InstitutionallyControlledHousingOffered.HasValue)
                writer.WriteElementString("InstitutionallyControlledHousingOffered", InstitutionallyControlledHousingOffered.ToString());
            else
                writer.WriteElementString("InstitutionallyControlledHousingOffered", null);

            if (StudentsRequiredLiveOnCampusOrHousing.HasValue)
                writer.WriteElementString("StudentsRequiredLiveOnCampusOrHousing", StudentsRequiredLiveOnCampusOrHousing.ToString());
            else
                writer.WriteElementString("StudentsRequiredLiveOnCampusOrHousing", null);

            if (InstitutionChargeDifferentTuition.HasValue)
                writer.WriteElementString("InstitutionChargeDifferentTuition", InstitutionChargeDifferentTuition.ToString());
            else
                writer.WriteElementString("InstitutionChargeDifferentTuition", null);

            if (ChargeForInDistrict.HasValue)
                writer.WriteElementString("ChargeForInDistrict", ChargeForInDistrict.ToString());
            else
                writer.WriteElementString("ChargeForInDistrict", null);

            if (ChargeForInState.HasValue)
                writer.WriteElementString("ChargeForInState", ChargeForInState.ToString());
            else
                writer.WriteElementString("ChargeForInState", null);

            if (ChargeForOutOfState.HasValue)
                writer.WriteElementString("ChargeForOutOfState", ChargeForOutOfState.ToString());
            else
                writer.WriteElementString("ChargeForOutOfState", null);

            if (NumberOfMonths.HasValue)
                writer.WriteElementString("NumberOfMonths", NumberOfMonths.ToString());
            else
                writer.WriteElementString("NumberOfMonths", null);

            if (!string.IsNullOrEmpty(LargestProgram))
                writer.WriteElementString("LargestProgram", LargestProgram.ToString());
            else
                writer.WriteElementString("LargestProgram", null);

            
            WriteKeyValueArray(writer, "PoaValues", this.PoaValues);
            WriteKeyValueArray(writer, "TgaValues", this.TgaValues);


            writer.WriteElementString("Percentage", Percentage.GetValueOrDefault(0).ToString());
            writer.WriteElementString("Explanation1", Explanation1);
            writer.WriteElementString("Explanation2", Explanation2);
            writer.WriteElementString("Explanation3", Explanation3);


            writer.WriteElementString("TuituinInDistrinct", TuitionInDistrinct.ToString());
            writer.WriteElementString("TuituinInState", TuitionInState.ToString());
            writer.WriteElementString("TuituinOutOfState", TuitionOutOfState.ToString());
            writer.WriteElementString("TuituinAmount", TuitionAmount.ToString());
            writer.WriteElementString("BooksAndSupplies", BooksAndSupplies.ToString());
            writer.WriteElementString("LivingOnCampus", LivingOnCampus.ToString());
            writer.WriteElementString("LivingOffCampus", LivingOffCampus.ToString());
            writer.WriteElementString("LivingOffCampusWithFamily", LivingOffCampusWithFamily.ToString());
            writer.WriteElementString("OtherOnCampus", OtherOnCampus.ToString());
            writer.WriteElementString("OtherOffCampus", OtherOffCampus.ToString());
            writer.WriteElementString("OtherOffCampusWithFamily", OtherOffCampusWithFamily.ToString());
            
                
        }
        
        private string ReadString(string name)
        {
            if (!string.IsNullOrEmpty(_xmlDocument.SelectSingleNode("//" + name).InnerText))
                return _xmlDocument.SelectSingleNode("//" + name).InnerText;
            else
                return null;
        }

        private bool? ReadBoolean(string name)
        {
            bool? result = null;            
            if (!string.IsNullOrEmpty(_xmlDocument.SelectSingleNode("//" + name).InnerText))
                result = bool.Parse(_xmlDocument.SelectSingleNode("//" + name).InnerText);                
            return result;
        }

        private int? ReadInt32(string name)
        {
            int? result = null;
            if (!string.IsNullOrEmpty(_xmlDocument.SelectSingleNode("//" + name).InnerText))                            
                result = int.Parse(_xmlDocument.SelectSingleNode("//" + name).InnerText);            
            return result;
        }

        private void WriteKeyValueArray(XmlWriter xmlWriter, string name, KeyValue[] arrKeyValue)
        {
            xmlWriter.WriteStartElement(name);
            if (arrKeyValue != null)
            {
                for (int i = 0; i < arrKeyValue.Length; i++)
                {
                    xmlWriter.WriteStartElement("Item");
                    xmlWriter.WriteAttributeString("AutoGenerated", arrKeyValue[i].AutoGenerated.ToString().ToLower());
                    xmlWriter.WriteElementString(HttpUtility.HtmlEncode(arrKeyValue[i].Key), HttpUtility.HtmlEncode(arrKeyValue[i].Value));
                    xmlWriter.WriteEndElement();
                }
            }
            xmlWriter.WriteEndElement();
        }

        private KeyValue[] ReadKeyValueArray(string name)
        {
            List<KeyValue> listKeyValues = new List<KeyValue>();            
            foreach (XmlNode xmlNode in _xmlDocument.SelectNodes(String.Format("//{0}/Item", name)))
            {
                bool autoGenerated = Boolean.Parse(HttpUtility.HtmlDecode(xmlNode.Attributes["AutoGenerated"].Value));
                string key = HttpUtility.HtmlDecode(xmlNode.ChildNodes[0].Name);
                string value = HttpUtility.HtmlDecode(xmlNode.ChildNodes[0].InnerText);                
                listKeyValues.Add(new KeyValue(key, value, autoGenerated));                
            }

            KeyValue[] arrKeyValue = new KeyValue[listKeyValues.Count];
            for (int i = 0; i < listKeyValues.Count; i++)
                arrKeyValue[i] = listKeyValues[i];

            return arrKeyValue;
        }

        #endregion

        public AppContext()
        {

        }
    }

    /// <summary>
    /// Represents type of institution
    /// </summary>
    public enum InstitutionType
    {
        Unknown = 0,
        Academic = 1,
        Program = 2
    }

    /// <summary>
    /// Class is used to store key/value pairs
    /// </summary>
    public class KeyValue
    {
        public KeyValue(string key, string value, bool autoGenerated)
        {
            this.key = key;
            this.value = value;
            this.autoGenerated = autoGenerated;
        }
        private string key;
        private string value;
        private bool autoGenerated;        

        public string Key
        {
          get { return this.key; }
          set { this.key = value; }
        }

        public string Value
        {
          get { return this.value; }
          set { this.value = value; }
        }

        public bool AutoGenerated
        {
            get { return autoGenerated; }
            set { autoGenerated = value; }
        }
    }
}
