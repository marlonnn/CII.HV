using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Deserialize object from file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T SerializeFromFile<T>(this T source, string fileName)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// serialize object to file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static void SerializeToFile<T>(this T source, string fileName)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (Object.ReferenceEquals(source, null))
            {
                return;
            }
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, source);
            }
        }

        public static bool SerializeEqual<T>(this T source, T target)
        {
            return source.Serialize().SequenceEqual(target.Serialize());
        }

        /// <summary>
        /// get serialize bytes array
        /// </summary>
        /// <returns></returns>
        public static byte[] Serialize<T>(this T source)
        {
            if (source == null) return new byte[] { };

            IFormatter formatter = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    formatter.Serialize(stream, source);
                    return stream.ToArray();
                }
                catch (System.Exception)
                {

                }
                return new byte[] { };
            }
        }
    }
}
