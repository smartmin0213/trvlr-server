using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;

namespace TRVLR.Data
{
    public static class XpoDataLayer
    {
        public static void Initialize(string connectionString)
        {
            // Initialize the XPO metadata dictionary
            XPDictionary dictionary = new ReflectionDictionary();
            dictionary.GetDataStoreSchema(typeof(TRVLR.Models.User).Assembly);

            // Create a connection provider for SQL Server
            IDataStore provider = XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.DatabaseAndSchema);
            
            // Set up the data layer
            XpoDefault.DataLayer = new SimpleDataLayer(dictionary, provider);

            // Set the default session
            XpoDefault.Session = new Session(XpoDefault.DataLayer);
        }
    }
}