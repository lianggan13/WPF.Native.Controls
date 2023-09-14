using System.Data;
using System.IO;
using System.Reflection;

namespace WPF.Common.Database
{
    public class StoreDbDataSet
    {
        public DataTable GetProducts()
        {
            return ReadDataSet().Tables[0];
        }

        public DataSet GetCategoriesAndProducts()
        {
            return ReadDataSet();
        }

        internal static DataSet ReadDataSet()
        {
            var @base = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DataSet ds = new DataSet();
            //ds.ReadXmlSchema(@"Database\store.xsd");
            //ds.ReadXml(@"Database\store.xml");

            ds.ReadXmlSchema(Path.Combine(@base, @"Database\store.xsd"));
            ds.ReadXml(Path.Combine(@base, @"Database\store.xml"));
            return ds;
        }

    }
}
