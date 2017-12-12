using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.SysClass
{
    /// <summary>
    /// Json file helper: Read config from local json file and Write config to local
    /// Author: Zhong Wen 2017/08/17
    /// </summary>
    public class JsonFile
    {
        /// <summary>
        /// json config file name
        /// </summary>
        public static string fileName = string.Format("{0}\\Config\\config.json", System.Environment.CurrentDirectory);
        
        /// <summary>
        /// get config from json string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static T GetConfigFromJsonText<T>(string jsonText)
        {
            T config = JsonConvert.DeserializeObject<T>(jsonText);
            return config;
        }

        /// <summary>
        /// get json string from config
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string GetJsonTextFromConfig<T>(T config)
        {
            JsonSerializer serializer = new JsonSerializer();
            StringWriter textWriter = new StringWriter();
            JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
            {
                Formatting = Formatting.Indented,
                Indentation = 4,
                IndentChar = ' '
            };
            serializer.Serialize(jsonWriter, config);
            return textWriter.ToString();
        }

        /// <summary>
        /// read json config string form local file
        /// </summary>
        /// <returns></returns>
        public static string ReadJsonConfigString()
        {
            string jsonString = "";
            if (File.Exists(fileName))
            {
                jsonString = File.ReadAllText(fileName);
            }
            return jsonString;
        }

        /// <summary>
        /// write json config to local file
        /// </summary>
        /// <param name="jsonConfig"></param>
        public static void WriteConfigToLocal(string jsonConfig)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }
                File.WriteAllText(fileName, jsonConfig);
            }
            catch (Exception ee)
            {
                LogHelper.GetLogger<JsonFile>().Error(ee.Message);
                LogHelper.GetLogger<JsonFile>().Error(ee.StackTrace);
            }
        }
    }
}
