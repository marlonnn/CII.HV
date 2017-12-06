using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR
{
    /// <summary>
    /// byte数组处理类，用于将二进制转成二进制字符串，或者转成字符串。
    /// </summary>
    /// <remark>
    /// 公司：CASCO
    /// 作者：张立鹏
    /// 创建日期：2013-5-14
    /// </remark>
    public class ByteHelper
    {
        /// <summary>
        /// 将二进制数组用16进制输出
        /// <para>BitConverter也可以实现,不过是加短线隔断的格式</para>
        /// </summary>
        public static string Byte2Xstring(IEnumerable<byte> bytes)
        {

            return bytes.Aggregate("", (sum, b) => sum + string.Format("{0:x2}", b));
        }

        /// <summary>
        /// 将形如Byte2Xstring函数输出的16进制字符串转成二进制数组
        /// </summary>
        public static byte[] Xstring2Byte(string str)
        {
            //移除所有空白
            str = str.Replace(" ", "");
            byte[] bytes;
            if (str.Length == 1)
            {
                bytes = new byte[1];
                byte b;
                if (Byte.TryParse(str, NumberStyles.HexNumber, null, out b))
                    bytes[0] = b;
                else
                    bytes = new byte[0];
            }
            else if (str.Length > 1)
            {
                bytes = new byte[str.Length / 2];
                for (int x = 0; x < str.Length / 2; x++)
                {
                    string sub = str.Substring(x * 2, 2);
                    byte b;
                    if (Byte.TryParse(sub, NumberStyles.HexNumber, null, out b))
                        bytes[x] = b;
                    else
                    {
                        bytes = new byte[0];
                        break;
                    }
                }
            }
            else
                bytes = new byte[0];
            return bytes;
        }

        /// <summary>
        /// 取第一个byte
        /// </summary>
        public static byte GetFirstByte(string str)
        {
            string t = str.Replace(" ", "");
            string n = t;
            if (t.Length > 2)
            {
                n = t.Substring(0, 2);
            }
            byte b;
            Byte.TryParse(n, NumberStyles.HexNumber, null, out b);
            return b;
        }

        /// <summary>
        /// 将二进制数组用16进制输出成可阅读格式，如 0x FA 55 9A ...
        /// </summary>
        public static string Byte2ReadalbeXstring(byte[] bytes)
        {
            string msg = "0x ";
            foreach (byte b in bytes)
            {
                msg += string.Format("{0:x2} ", b);
            }
            return msg;
        }

        /// <summary>
        /// 将二进制数组转成字符串（二进制内容是ASCII码）
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Byte2String(byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }

        /// <summary>
        /// 将字符串转成二进制数组（ASCII码）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] String2Byte(string str)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            return bytes;
        }

        /// <summary>
        /// 将4个字节的BYTE数组转成4个字节的INT
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static int Byte2Int32(byte[] bytes)
        {
            int result = BitConverter.ToInt32(bytes, 0);
            return result;
        }

        /// <summary>
        /// 小端转换为大端32位
        /// </summary>
        /// <returns></returns>
        public static byte[] LittleToBig32(string str)
        {
            return Encoding.UTF32.GetBytes(str);
        }

        /// <summary>
        /// 小端转换为大端16位
        /// </summary>
        /// <returns></returns>
        public static byte[] LittleToBig16(string str)
        {
            return Encoding.BigEndianUnicode.GetBytes(str);
        }

        /// <summary>
        /// 大端转换为小端
        /// </summary>
        /// <returns></returns>
        public static string BigToLittle(byte[] bt, int index, int len)
        {
            return Encoding.Unicode.GetString(bt, index, len);
        }

        /// <summary>
        /// 将64位的long转化为BYTE[]
        /// </summary>
        /// <returns></returns>
        public static byte[] longTobyte(long x, bool needbigEnd = true)
        {
            var temp = BitConverter.GetBytes(x);
            if (BitConverter.IsLittleEndian && needbigEnd)
                temp = temp.Reverse().ToArray();
            return temp;
        }
        /// <summary>
        /// 将32位的INT转化为BYTE[]
        /// </summary>
        /// <returns></returns>
        public static byte[] intTobyte(int x)
        {
            string s;
            s = Convert.ToString(x);
            byte[] temp = new byte[4];
            temp = LittleToBig32(s);
            for (int i = 0; i < 4; ++i)
            {
                if (temp[i] != 0x00)
                {
                    temp[i] = Convert.ToByte(temp[i] - Convert.ToByte(48));
                }
            }
            return temp;
        }
        /// <summary>
        /// 将16位的INT转化为BYTE[]
        /// </summary>
        /// <returns></returns>
        public static byte[] shortTobyte(short x)
        {
            string s;
            s = Convert.ToString(x);
            byte[] temp = new byte[2];
            temp = LittleToBig16(s);
            for (int i = 0; i < 2; ++i)
            {
                if (temp[i] != 0x00)
                {
                    temp[i] = Convert.ToByte(temp[i] - Convert.ToByte(48));
                }
            }
            return temp;
        }

        /// <summary>
        /// 将BYTE[]转换为32位的INT
        /// </summary>
        /// <returns></returns>
        public static Int32 byteToint32(byte[] bt, int index)
        {
            string s;
            s = BigToLittle(bt, index, 4);
            return Convert.ToInt32(s);
        }

        /// <summary>
        /// 将BYTE[]转换为16位的INT
        /// </summary>
        /// <returns></returns>
        public static Int16 byteToint16(byte[] bt, int index)
        {
            string s;
            s = BigToLittle(bt, index, 2);
            return Convert.ToInt16(s);
        }

        /// <summary>
        /// 将大端在前BYTE[]转换为32位的UINT
        /// </summary>
        public static UInt32 BigEndianByteToUInt32(byte[] bt, int index)
        {
            byte[] k = new byte[4];

            Array.Copy(bt, index, k, 0, 4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(k);
            }
            return BitConverter.ToUInt32(k, 0);
        }
    }
}
