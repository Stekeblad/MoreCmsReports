using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stekeblad.MoreCmsReports.Business
{
    /// <summary>
    /// Contains logic for converting classes to/from json and saving/loading them from the websites App_Data directory.
    /// </summary>
    public static class DataStorage
    {
        public static void WriteObjectToFile<T>(object dataObject)
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(GetFilePath<T>()))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, dataObject);
            }
        }

        public static object ReadObjectFromFile<T>()
        {
            using (TextReader tr = new StreamReader(GetFilePath<T>()))
            using (JsonTextReader reader = new JsonTextReader(tr))
            {
                return JsonConvert.DeserializeObject<T>(reader.ToString());
            }
        }

        private static string GetFilePath<T>()
        {
            string appDataPath = EPiServer.Framework.Configuration.EPiServerFrameworkSection.Instance.AppData.BasePath;
            if (string.Equals(appDataPath, "app_data", StringComparison.OrdinalIgnoreCase))
            {
                appDataPath = AppDomain.CurrentDomain.BaseDirectory + appDataPath;
            }
            return $"{appDataPath}/MoreCmsReports/{typeof(T).Name}.json";
        }
    }
}
