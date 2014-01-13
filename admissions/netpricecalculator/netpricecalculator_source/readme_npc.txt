How to install and use the Net Price Calculator Template Source Code on your server to create your own, modified version of the Net Price Calculator:

1. Extract the zip file netpricecalculator_source.zip

2. Create a new database in Sql Server 2005/2008 in order to restore the back-up file to it.

3. Restore the back-up file in the "Database" folder of the new database created in step 2.

4. Provide "read" access to the user account that the virtual website created for the Net Price Calculator will use to access the database.

5. The following table name will require "read/write" permissions: "npSessionState".

6. Copy the contents of the "SourceCode" folder to the virtual website folder on your server.

7. Edit the "Web.config" file to modify the connection string. Please do not change the name of the connection string 
provided in the web.config file. You will only need to modify the "connectionString" attribute with the correct
information for your database.

8. Create the following temporary folder "tempfiles/netprice/images" in the web server root directory.

9. Provide "read/write" access permission to the web user account assigned in IIS for the website.

10. Copy the contents of the "TempFiles" folder to "tempfiles/netprice/images".
