using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

using System.Text;
using System.Collections.Generic;

namespace Junior.Utility.Utils
{
    /// <summary>
    /// 通用 工具类
    /// </summary>
   public class Util
    {
        #region 数据转换

        #region 补足位数
        /// <summary>
        /// 指定字符串的固定长度，如果字符串小于固定长度，
        /// 则在字符串的前面补足零，可设置的固定长度最大为9位
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <param name="limitedLength">字符串的固定长度</param>
        public static string RepairZero(string text, int limitedLength)
        {
            //补足0的字符串
            string temp = "";

            //补足0
            for (int i = 0; i < limitedLength - text.Length; i++)
            {
                temp += "0";
            }

            //连接text
            temp += text;

            //返回补足0的字符串
            return temp;
        }
        #endregion

        #region 各进制数间转换
        /// <summary>
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string ConvertBase(string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, from);  //先转成10进制
                string result = Convert.ToString(intValue, to);  //再转成目标进制
                if (to == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;
                        case 6:
                            result = "00" + result;
                            break;
                        case 5:
                            result = "000" + result;
                            break;
                        case 4:
                            result = "0000" + result;
                            break;
                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch
            {

                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return "0";
            }
        }
        #endregion

        #region 使用指定字符集将string转换成byte[]
        /// <summary>
        /// 使用指定字符集将string转换成byte[]
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }
        #endregion

        #region 使用指定字符集将byte[]转换成string
        /// <summary>
        /// 使用指定字符集将byte[]转换成string
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <param name="encoding">字符编码</param>
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }
        #endregion

        #region 将byte[]转换成int
        /// <summary>
        /// 将byte[]转换成int
        /// </summary>
        /// <param name="data">需要转换成整数的byte数组</param>
        public static int BytesToInt32(byte[] data)
        {
            //如果传入的字节数组长度小于4,则返回0
            if (data.Length < 4)
            {
                return 0;
            }

            //定义要返回的整数
            int num = 0;

            //如果传入的字节数组长度大于4,需要进行处理
            if (data.Length >= 4)
            {
                //创建一个临时缓冲区
                byte[] tempBuffer = new byte[4];

                //将传入的字节数组的前4个字节复制到临时缓冲区
                Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);

                //将临时缓冲区的值转换成整数，并赋给num
                num = BitConverter.ToInt32(tempBuffer, 0);
            }

            //返回整数
            return num;
        }
        #endregion

        /// <summary>
        /// 返回对象obj的String值,obj为null时返回空值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>字符串。</returns>
        public static string ToObjectString(object obj)
        {
            return null == obj ? String.Empty : obj.ToString();
        }

        /// <summary>
        /// 将对象转换为数值(Int32)类型,转换失败返回-1。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Int32数值。</returns>
        public static int ToInt(object obj)
        {
            try
            {
                return int.Parse(ToObjectString(obj));
            }
            catch
            { return -1; }
        }

        /// <summary>
        /// 将对象转换为数值(Int32)类型。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Int32数值。</returns>
        public static int ToInt(object obj, int returnValue)
        {
            try
            {
                return int.Parse(ToObjectString(obj));
            }
            catch
            { return returnValue; }
        }
        /// <summary>
        /// 将对象转换为数值(Long)类型,转换失败返回-1。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Long数值。</returns>
        public static long ToLong(object obj)
        {
            try
            {
                return long.Parse(ToObjectString(obj));
            }
            catch
            { return -1L; }
        }
        /// <summary>
        /// 将对象转换为数值(Long)类型。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Long数值。</returns>
        public static long ToLong(object obj, long returnValue)
        {
            try
            {
                return long.Parse(ToObjectString(obj));
            }
            catch
            { return returnValue; }
        }
        /// <summary>
        /// 将对象转换为数值(Decimal)类型,转换失败返回-1。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Decimal数值。</returns>
        public static decimal ToDecimal(object obj)
        {
            try
            {
                return decimal.Parse(ToObjectString(obj));
            }
            catch
            { return -1M; }
        }

        /// <summary>
        /// 将对象转换为数值(Decimal)类型。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Decimal数值。</returns>
        public static decimal ToDecimal(object obj, decimal returnValue)
        {
            try
            {
                return decimal.Parse(ToObjectString(obj));
            }
            catch
            { return returnValue; }
        }
        /// <summary>
        /// 将对象转换为数值(Double)类型,转换失败返回-1。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Double数值。</returns>
        public static double ToDouble(object obj)
        {
            try
            {
                return double.Parse(ToObjectString(obj));
            }
            catch
            { return -1; }
        }

        /// <summary>
        /// 将对象转换为数值(Double)类型。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Double数值。</returns>
        public static double ToDouble(object obj, double returnValue)
        {
            try
            {
                return double.Parse(ToObjectString(obj));
            }
            catch
            { return returnValue; }
        }
        /// <summary>
        /// 将对象转换为数值(Float)类型,转换失败返回-1。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Float数值。</returns>
        public static float ToFloat(object obj)
        {
            try
            {
                return float.Parse(ToObjectString(obj));
            }
            catch
            { return -1; }
        }

        /// <summary>
        /// 将对象转换为数值(Float)类型。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Float数值。</returns>
        public static float ToFloat(object obj, float returnValue)
        {
            try
            {
                return float.Parse(ToObjectString(obj));
            }
            catch
            { return returnValue; }
        }
        /// <summary>
        /// 将对象转换为数值(DateTime)类型,转换失败返回Now。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>DateTime值。</returns>
        public static DateTime ToDateTime(object obj)
        {
            try
            {
                DateTime dt = DateTime.Parse(ToObjectString(obj));
                if (dt > DateTime.MinValue && DateTime.MaxValue > dt)
                    return dt;
                return DateTime.Now;
            }
            catch
            { return DateTime.Now; }
        }

        /// <summary>
        /// 将对象转换为数值(DateTime)类型。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>DateTime值。</returns>
        public static DateTime ToDateTime(object obj, DateTime returnValue)
        {
            try
            {
                DateTime dt = DateTime.Parse(ToObjectString(obj));
                if (dt > DateTime.MinValue && DateTime.MaxValue > dt)
                    return dt;
                return returnValue;
            }
            catch
            { return returnValue; }
        }
        /// <summary>
        /// 从Boolean转换成byte,转换失败返回0。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Byte值。</returns>
        public static byte ToByteByBool(object obj)
        {
            string text = ToObjectString(obj).Trim();
            if (text == string.Empty)
                return 0;
            else
            {
                try
                {
                    return (byte)(text.ToLower() == "true" ? 1 : 0);
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 从Boolean转换成byte。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Byte值。</returns>
        public static byte ToByteByBool(object obj, byte returnValue)
        {
            string text = ToObjectString(obj).Trim();
            if (text == string.Empty)
                return returnValue;
            else
            {
                try
                {
                    return (byte)(text.ToLower() == "true" ? 1 : 0);
                }
                catch
                {
                    return returnValue;
                }
            }
        }
        /// <summary>
        /// 从byte转换成Boolean,转换失败返回false。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Boolean值。</returns>
        public static bool ToBoolByByte(object obj)
        {
            try
            {
                string s = ToObjectString(obj).ToLower();
                return s == "1" || s == "true" ? true : false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 从byte转换成Boolean。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Boolean值。</returns>
        public static bool ToBoolByByte(object obj, bool returnValue)
        {
            try
            {
                string s = ToObjectString(obj).ToLower();
                return s == "1" || s == "true" ? true : false;
            }
            catch
            {
                return returnValue;
            }
        }
        #endregion

        #region 数据判断
        /// <summary>
        /// 判断文本obj是否为空值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Boolean值。</returns>
        public static bool IsEmpty(string obj)
        {
            return ToObjectString(obj).Trim() == String.Empty ? true : false;
        }

        /// <summary>
        /// 判断对象是否为正确的日期值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Boolean。</returns>
        public static bool IsDateTime(object obj)
        {
            try
            {
                DateTime dt = DateTime.Parse(ToObjectString(obj));
                if (dt > DateTime.MinValue && DateTime.MaxValue > dt)
                    return true;
                return false;
            }
            catch
            { return false; }
        }
        #region 日期格式判断
        /// <summary>
        /// 日期格式字符串判断
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime(string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    DateTime.Parse(str);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion
        /// <summary>
        /// 判断对象是否为正确的Int32值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Int32值。</returns>
        public static bool IsInt(object obj)
        {
            try
            {
                int.Parse(ToObjectString(obj));
                return true;
            }
            catch
            { return false; }
        }

        /// <summary>
        /// 判断对象是否为正确的Long值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Long值。</returns>
        public static bool IsLong(object obj)
        {
            try
            {
                long.Parse(ToObjectString(obj));
                return true;
            }
            catch
            { return false; }
        }

        /// <summary>
        /// 判断对象是否为正确的Float值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Float值。</returns>
        public static bool IsFloat(object obj)
        {
            try
            {
                float.Parse(ToObjectString(obj));
                return true;
            }
            catch
            { return false; }
        }

        /// <summary>
        /// 判断对象是否为正确的Double值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Double值。</returns>
        public static bool IsDouble(object obj)
        {
            try
            {
                double.Parse(ToObjectString(obj));
                return true;
            }
            catch
            { return false; }
        }

        /// <summary>
        /// 判断对象是否为正确的Decimal值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Decimal值。</returns>
        public static bool IsDecimal(object obj)
        {
            try
            {
                decimal.Parse(ToObjectString(obj));
                return true;
            }
            catch
            { return false; }
        }
        #endregion

        #region 数据操作
        /// <summary>
        /// 去除字符串的所有空格。
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>字符串</returns>
        public static string StringTrimAll(string text)
        {
            string _text = ToObjectString(text);
            string returnText = String.Empty;
            char[] chars = _text.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i].ToString() != string.Empty)
                    returnText += chars[i].ToString();
            }
            return returnText;
        }

        /// <summary>
        /// 去除数值字符串的所有空格。
        /// </summary>
        /// <param name="numricString">数值字符串</param>
        /// <returns>String</returns>
        public static string NumricTrimAll(string numricString)
        {
            string text = ToObjectString(numricString).Trim();
            string returnText = String.Empty;
            char[] chars = text.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i].ToString() == "+" || chars[i].ToString() == "-" || IsDouble(chars[i].ToString()))
                    returnText += chars[i].ToString();
            }
            return returnText;
        }

        /// <summary>
        /// 在数组中查找匹配对象类型
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="obj">对象</param>
        /// <returns>Boolean</returns>
        public static bool ArrayFind(Array array, object obj)
        {
            bool b = false;
            foreach (object obj1 in array)
            {
                if (obj.Equals(obj1))
                {
                    b = true;
                    break;
                }
            }
            return b;
        }

        /// <summary>
        /// 在数组中查找匹配字符串
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="obj">对象</param>
        /// <param name="unUpLower">是否忽略大小写</param>
        /// <returns>Boolean</returns>
        public static bool ArrayFind(Array array, string obj, bool unUpLower)
        {
            bool b = false;
            foreach (string obj1 in array)
            {
                if (!unUpLower)
                {
                    if (obj.Trim().Equals(obj1.ToString().Trim()))
                    {
                        b = true;
                        break;
                    }
                }
                else
                {
                    if (obj.Trim().ToUpper().Equals(obj1.ToString().Trim().ToUpper()))
                    {
                        b = true;
                        break;
                    }
                }
            }
            return b;
        }
        /// <summary>
        /// 替换字符串中的单引号。
        /// </summary>
        /// <param name="inputString">字符串</param>
        /// <returns>String</returns>
        public static string ReplaceInvertedComma(string inputString)
        {
            return inputString.Replace("'", "''");
        }


        /// <summary>
        /// 判断两个字节数组是否具有相同值.
        /// </summary>
        /// <param name="bytea">字节1</param>
        /// <param name="byteb">字节2</param>
        /// <returns>Boolean</returns>
        public static bool CompareByteArray(byte[] bytea, byte[] byteb)
        {
            if (null == bytea || null == byteb)
            {
                return false;
            }
            else
            {
                int aLength = bytea.Length;
                int bLength = byteb.Length;
                if (aLength != bLength)
                    return false;
                else
                {
                    bool compare = true;
                    for (int index = 0; index < aLength; index++)
                    {
                        if (bytea[index].CompareTo(byteb[index]) != 0)
                        {
                            compare = false;
                            break;
                        }
                    }
                    return compare;
                }
            }
        }


        /// <summary>
        /// 日期智能生成。
        /// </summary>
        /// <param name="inputText">字符串</param>
        /// <returns>DateTime</returns>
        public static string BuildDate(string inputText)
        {
            try
            {
                return DateTime.Parse(inputText).ToShortDateString();
            }
            catch
            {
                string text = NumricTrimAll(inputText);
                string year = DateTime.Now.Year.ToString();
                string month = DateTime.Now.Month.ToString();
                string day = DateTime.Now.Day.ToString();
                int length = text.Length;
                if (length == 0)
                    return String.Empty;
                else
                {
                    if (length <= 2)
                        day = text;
                    else if (length <= 4)
                    {
                        month = text.Substring(0, 2);
                        day = text.Substring(2, length - 2);
                    }
                    else if (length <= 6)
                    {
                        year = text.Substring(0, 4);
                        month = text.Substring(4, length - 4);
                    }
                    else if (length > 6)
                    {
                        year = text.Substring(0, 4);
                        month = text.Substring(4, 2);
                        day = text.Substring(6, length - 6);
                    }
                    try
                    {
                        return DateTime.Parse(year + "-" + month + "-" + day).ToShortDateString();
                    }
                    catch
                    {
                        return String.Empty;
                    }
                }
            }
        }



        /// <summary>
        /// 检查文件是否真实存在。
        /// </summary>
        /// <param name="path">文件全名（包括路径）。</param>
        /// <returns>Boolean</returns>
        public static bool IsFileExists(string path)
        {
            try
            {
                return File.Exists(path);
            }
            catch
            { return false; }
        }

        /// <summary>
        /// 检查目录是否真实存在。
        /// </summary>
        /// <param name="path">目录路径.</param>
        /// <returns>Boolean</returns>
        public static bool IsDirectoryExists(string path)
        {
            try
            {
                return Directory.Exists(Path.GetDirectoryName(path));
            }
            catch
            { return false; }
        }

        /// <summary>
        /// 查找文件中是否存在匹配行。
        /// </summary>
        /// <param name="fi">目标文件.</param>
        /// <param name="lineText">要查找的行文本.</param>
        /// <param name="lowerUpper">是否区分大小写.</param>
        /// <returns>Boolean</returns>
        public static bool FindLineTextFromFile(FileInfo fi, string lineText, bool lowerUpper)
        {
            bool b = false;
            try
            {
                if (fi.Exists)
                {
                    StreamReader sr = new StreamReader(fi.FullName);
                    string g = "";
                    do
                    {
                        g = sr.ReadLine();
                        if (lowerUpper)
                        {
                            if (ToObjectString(g).Trim() == ToObjectString(lineText).Trim())
                            {
                                b = true;
                                break;
                            }
                        }
                        else
                        {
                            if (ToObjectString(g).Trim().ToLower() == ToObjectString(lineText).Trim().ToLower())
                            {
                                b = true;
                                break;
                            }
                        }
                    }
                    while (sr.Peek() != -1);
                    sr.Close();
                }
            }
            catch
            { b = false; }
            return b;
        }


        /// <summary>
        /// 判断父子级关系是否正确。
        /// </summary>
        /// <param name="table">数据表。</param>
        /// <param name="columnName">子键列名。</param>
        /// <param name="parentColumnName">父键列名。</param>
        /// <param name="inputString">子键值。</param>
        /// <param name="compareString">父键值。</param>
        /// <returns></returns>
        public static bool IsRightParent(DataTable table, string columnName, string parentColumnName, string inputString, string compareString)
        {
            ArrayList array = new ArrayList();
            SearchChild(array, table, columnName, parentColumnName, inputString, compareString);
            return array.Count == 0;
        }

        // 内部方法
        private static void SearchChild(ArrayList array, DataTable table, string columnName, string parentColumnName, string inputString, string compareString)
        {
            DataView view = new DataView(table);
            view.RowFilter = parentColumnName + "='" + ReplaceInvertedComma(inputString.Trim()) + "'";//找出所有的子类。
                                                                                                      //查找表中的数据的ID是否与compareString相等，相等返回 false;不相等继续迭代。
            for (int index = 0; index < view.Count; index++)
            {
                if (ToObjectString(view[index][columnName]).ToLower() == compareString.Trim().ToLower())
                {
                    array.Add("1");
                    break;
                }
                else
                {
                    SearchChild(array, table, columnName, parentColumnName, ToObjectString(view[index][columnName]), compareString);
                }
            }
        }

        #endregion

        #region 日期

        /// <summary>
        /// 格式化日期类型，返回字符串
        /// </summary>
        /// <param name="dtime">日期</param>
        /// <param name="s">日期年月日间隔符号</param>
        /// <returns></returns>
        public static String Formatdate(DateTime dtime, String s)
        {
            String datestr = "";
            datestr = dtime.Year.ToString() + s + dtime.Month.ToString().PadLeft(2, '0') + s + dtime.Day.ToString().PadLeft(2, '0');
            return datestr;
        }

        /// <summary>
        /// 返回日期差
        /// </summary>
        /// <param name="sdmin">开始日期</param>
        /// <param name="sdmax">结束日期</param>
        /// <returns>日期差：负数为失败</returns>
		public static int Datediff(DateTime sdmin, DateTime sdmax)
        {
            try
            {
                double i = 0;
                while (sdmin.AddDays(i) < sdmax)
                {
                    i++;
                }
                return ToInt(i);
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 返回日期差
        /// </summary>
        /// <param name="sdmin">开始日期</param>
        /// <param name="sdmax">结束日期</param>
        /// <returns>日期差：负数为失败</returns>
		public static int Datediff(String sdmin, String sdmax)
        {
            try
            {
                DateTime dmin;
                DateTime dmax;
                dmin = DateTime.Parse(sdmin);
                dmax = DateTime.Parse(sdmax);
                double i = 0;
                while (dmin.AddDays(i) < dmax)
                {
                    i++;
                }
                return ToInt(i);
            }
            catch
            {
                return -1;
            }
        }

        #endregion

        #region 转换用户输入

        /// <summary>
        /// 将用户输入的字符串转换为可换行、替换Html编码、无危害数据库特殊字符、去掉首尾空白、的安全方便代码。
        /// </summary>
        /// <param name="inputString">用户输入字符串</param>
        public static string ConvertStr(string inputString)
        {
            string retVal = inputString;
            //retVal=retVal.Replace("&","&amp;"); 
            retVal = retVal.Replace("\"", "&quot;");
            retVal = retVal.Replace("<", "&lt;");
            retVal = retVal.Replace(">", "&gt;");
            retVal = retVal.Replace(" ", "&nbsp;");
            retVal = retVal.Replace("  ", "&nbsp;&nbsp;");
            retVal = retVal.Replace("\t", "&nbsp;&nbsp;");
            retVal = retVal.Replace("\r", "<br>");
            return retVal;
        }

        public static string InputText(string inputString)
        {
            string retVal = inputString;
            retVal = ConvertStr(retVal);
            retVal = retVal.Replace("[url]", "");
            retVal = retVal.Replace("[/url]", "");
            return retVal;
        }


        /// <summary>
        /// 将html代码显示在网页上
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
		public static string OutputText(string inputString)
        {
            string retVal = System.Web.HttpUtility.HtmlDecode(inputString);
            retVal = retVal.Replace("<br>", "");
            retVal = retVal.Replace("&amp", "&;");
            retVal = retVal.Replace("&quot;", "\"");
            retVal = retVal.Replace("&lt;", "<");
            retVal = retVal.Replace("&gt;", ">");
            retVal = retVal.Replace("&nbsp;", " ");
            retVal = retVal.Replace("&nbsp;&nbsp;", "  ");
            return retVal;
        }

        public static string ToUrl(string inputString)
        {
            string retVal = inputString;
            retVal = ConvertStr(retVal);
            return Regex.Replace(retVal, @"\[url](?<x>[^\]]*)\[/url]", @"<a href=""$1"" target=""_blank"">$1</a>", RegexOptions.IgnoreCase);
        }

        public static string GetSafeCode(string str)
        {
            str = str.Replace("'", "");
            str = str.Replace(char.Parse("34"), ' ');
            str = str.Replace(";", "");
            return str;
        }

        #endregion

        #region 字符串操作
       
        ///// 字符串操作类
        ///// 1、GetStrArray(string str, char speater, bool toLower)  把字符串按照分隔符转换成 List
        ///// 2、GetStrArray(string str) 把字符串转 按照, 分割 换为数据
        ///// 3、GetArrayStr(List list, string speater) 把 List 按照分隔符组装成 string
        ///// 4、GetArrayStr(List list)  得到数组列表以逗号分隔的字符串
        ///// 5、GetArrayValueStr(Dictionary<int, int> list)得到数组列表以逗号分隔的字符串
        ///// 6、DelLastComma(string str)删除最后结尾的一个逗号
        ///// 7、DelLastChar(string str, string strchar)删除最后结尾的指定字符后的字符
        ///// 8、ToSBC(string input)转全角的函数(SBC case)
        ///// 9、ToDBC(string input)转半角的函数(SBC case)
        ///// 10、GetSubStringList(string o_str, char sepeater)把字符串按照指定分隔符装成 List 去除重复
        ///// 11、GetCleanStyle(string StrList, string SplitString)将字符串样式转换为纯字符串
        ///// 12、GetNewStyle(string StrList, string NewStyle, string SplitString, out string Error)将字符串转换为新样式
        ///// 13、SplitMulti(string str, string splitstr)分割字符串
        ///// 14、SqlSafeString(string String, bool IsDel)
    
            /// <summary>
            /// 把字符串按照分隔符转换成 List
            /// </summary>
            /// <param name="str">源字符串</param>
            /// <param name="speater">分隔符</param>
            /// <param name="toLower">是否转换为小写</param>
            /// <returns></returns>
            public static List<string> GetStrArray(string str, char speater, bool toLower)
            {
                List<string> list = new List<string>();
                string[] ss = str.Split(speater);
                foreach (string s in ss)
                {
                    if (!string.IsNullOrEmpty(s) && s != speater.ToString())
                    {
                        string strVal = s;
                        if (toLower)
                        {
                            strVal = s.ToLower();
                        }
                        list.Add(strVal);
                    }
                }
                return list;
            }
            /// <summary>
            /// 把字符串转 按照, 分割 换为数据
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static string[] GetStrArray(string str)
            {
                return str.Split(new Char[] { ',' });
            }
            /// <summary>
            /// 把 List<string> 按照分隔符组装成 string
            /// </summary>
            /// <param name="list"></param>
            /// <param name="speater"></param>
            /// <returns></returns>
            public static string GetArrayStr(List<string> list, string speater)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i == list.Count - 1)
                    {
                        sb.Append(list[i]);
                    }
                    else
                    {
                        sb.Append(list[i]);
                        sb.Append(speater);
                    }
                }
                return sb.ToString();
            }
            /// <summary>
            /// 得到数组列表以逗号分隔的字符串
            /// </summary>
            /// <param name="list"></param>
            /// <returns></returns>
            public static string GetArrayStr(List<int> list)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i == list.Count - 1)
                    {
                        sb.Append(list[i].ToString());
                    }
                    else
                    {
                        sb.Append(list[i]);
                        sb.Append(",");
                    }
                }
                return sb.ToString();
            }
            /// <summary>
            /// 得到数组列表以逗号分隔的字符串
            /// </summary>
            /// <param name="list"></param>
            /// <returns></returns>
            public static string GetArrayValueStr(Dictionary<int, int> list)
            {
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<int, int> kvp in list)
                {
                    sb.Append(kvp.Value + ",");
                }
                if (list.Count > 0)
                {
                    return DelLastComma(sb.ToString());
                }
                else
                {
                    return "";
                }
            }


            #region 删除最后一个字符之后的字符

            /// <summary>
            /// 删除最后结尾的一个逗号
            /// </summary>
            public static string DelLastComma(string str)
            {
                return str.Substring(0, str.LastIndexOf(","));
            }

            /// <summary>
            /// 删除最后结尾的指定字符后的字符
            /// </summary>
            public static string DelLastChar(string str, string strchar)
            {
                return str.Substring(0, str.LastIndexOf(strchar));
            }

            #endregion




            /// <summary>
            /// 转全角的函数(SBC case)
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            public static string ToSBC(string input)
            {
                //半角转全角：
                char[] c = input.ToCharArray();
                for (int i = 0; i < c.Length; i++)
                {
                    if (c[i] == 32)
                    {
                        c[i] = (char)12288;
                        continue;
                    }
                    if (c[i] < 127)
                        c[i] = (char)(c[i] + 65248);
                }
                return new string(c);
            }

            /// <summary>
            ///  转半角的函数(SBC case)
            /// </summary>
            /// <param name="input">输入</param>
            /// <returns></returns>
            public static string ToDBC(string input)
            {
                char[] c = input.ToCharArray();
                for (int i = 0; i < c.Length; i++)
                {
                    if (c[i] == 12288)
                    {
                        c[i] = (char)32;
                        continue;
                    }
                    if (c[i] > 65280 && c[i] < 65375)
                        c[i] = (char)(c[i] - 65248);
                }
                return new string(c);
            }

            /// <summary>
            /// 把字符串按照指定分隔符装成 List 去除重复
            /// </summary>
            /// <param name="o_str"></param>
            /// <param name="sepeater"></param>
            /// <returns></returns>
            public static List<string> GetSubStringList(string o_str, char sepeater)
            {
                List<string> list = new List<string>();
                string[] ss = o_str.Split(sepeater);
                foreach (string s in ss)
                {
                    if (!string.IsNullOrEmpty(s) && s != sepeater.ToString())
                    {
                        list.Add(s);
                    }
                }
                return list;
            }


            #region 将字符串样式转换为纯字符串
            /// <summary>
            ///  将字符串样式转换为纯字符串
            /// </summary>
            /// <param name="StrList"></param>
            /// <param name="SplitString"></param>
            /// <returns></returns>
            public static string GetCleanStyle(string StrList, string SplitString)
            {
                string RetrunValue = "";
                //如果为空，返回空值
                if (StrList == null)
                {
                    RetrunValue = "";
                }
                else
                {
                    //返回去掉分隔符
                    string NewString = "";
                    NewString = StrList.Replace(SplitString, "");
                    RetrunValue = NewString;
                }
                return RetrunValue;
            }
            #endregion

            #region 将字符串转换为新样式
            /// <summary>
            /// 将字符串转换为新样式
            /// </summary>
            /// <param name="StrList"></param>
            /// <param name="NewStyle"></param>
            /// <param name="SplitString"></param>
            /// <param name="Error"></param>
            /// <returns></returns>
            public static string GetNewStyle(string StrList, string NewStyle, string SplitString, out string Error)
            {
                string ReturnValue = "";
                //如果输入空值，返回空，并给出错误提示
                if (StrList == null)
                {
                    ReturnValue = "";
                    Error = "请输入需要划分格式的字符串";
                }
                else
                {
                    //检查传入的字符串长度和样式是否匹配,如果不匹配，则说明使用错误。给出错误信息并返回空值
                    int strListLength = StrList.Length;
                    int NewStyleLength = GetCleanStyle(NewStyle, SplitString).Length;
                    if (strListLength != NewStyleLength)
                    {
                        ReturnValue = "";
                        Error = "样式格式的长度与输入的字符长度不符，请重新输入";
                    }
                    else
                    {
                        //检查新样式中分隔符的位置
                        string Lengstr = "";
                        for (int i = 0; i < NewStyle.Length; i++)
                        {
                            if (NewStyle.Substring(i, 1) == SplitString)
                            {
                                Lengstr = Lengstr + "," + i;
                            }
                        }
                        if (Lengstr != "")
                        {
                            Lengstr = Lengstr.Substring(1);
                        }
                        //将分隔符放在新样式中的位置
                        string[] str = Lengstr.Split(',');
                        foreach (string bb in str)
                        {
                            StrList = StrList.Insert(int.Parse(bb), SplitString);
                        }
                        //给出最后的结果
                        ReturnValue = StrList;
                        //因为是正常的输出，没有错误
                        Error = "";
                    }
                }
                return ReturnValue;
            }
            #endregion

            /// <summary>
            /// 分割字符串
            /// </summary>
            /// <param name="str"></param>
            /// <param name="splitstr"></param>
            /// <returns></returns>
            public static string[] SplitMulti(string str, string splitstr)
            {
                string[] strArray = null;
                if ((str != null) && (str != ""))
                {
                    strArray = new Regex(splitstr).Split(str);
                }
                return strArray;
            }
            public static string SqlSafeString(string String, bool IsDel)
            {
                if (IsDel)
                {
                    String = String.Replace("'", "");
                    String = String.Replace("\"", "");
                    return String;
                }
                String = String.Replace("'", "&#39;");
                String = String.Replace("\"", "&#34;");
                return String;
            }

            #region 获取正确的Id，如果不是正整数，返回0
            /// <summary>
            /// 获取正确的Id，如果不是正整数，返回0
            /// </summary>
            /// <param name="_value"></param>
            /// <returns>返回正确的整数ID，失败返回0</returns>
            public static int StrToId(string _value)
            {
                if (IsNumberId(_value))
                    return int.Parse(_value);
                else
                    return 0;
            }
            #endregion
            #region 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。
            /// <summary>
            /// 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。(0除外)
            /// </summary>
            /// <param name="_value">需验证的字符串。。</param>
            /// <returns>是否合法的bool值。</returns>
            public static bool IsNumberId(string _value)
            {
                return QuickValidate("^[1-9]*[0-9]*$", _value);
            }
            #endregion
            #region 快速验证一个字符串是否符合指定的正则表达式。
            /// <summary>
            /// 快速验证一个字符串是否符合指定的正则表达式。
            /// </summary>
            /// <param name="_express">正则表达式的内容。</param>
            /// <param name="_value">需验证的字符串。</param>
            /// <returns>是否合法的bool值。</returns>
            public static bool QuickValidate(string _express, string _value)
            {
                if (_value == null) return false;
                Regex myRegex = new Regex(_express);
                if (_value.Length == 0)
                {
                    return false;
                }
                return myRegex.IsMatch(_value);
            }
            #endregion
            #endregion

            public static string GetGuid(string guid)
        {
            return guid.Replace("-", "");
        }

        public static string ReadConfig(string filePath)
        {
            return System.Configuration.ConfigurationManager.AppSettings[filePath];
        }

        #region   字符串长度区分中英文截取
        /// <summary>   
        /// 截取文本，区分中英文字符，中文算两个长度，英文算一个长度
        /// </summary>
        /// <param name="str">待截取的字符串</param>
        /// <param name="length">需计算长度的字符串</param>
        /// <returns>string</returns>
        public static string GetSubString(string str, int length)
        {
            string temp = str;
            int j = 0;
            int k = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                if (Regex.IsMatch(temp.Substring(i, 1), @"[\u4e00-\u9fa5]+"))
                {
                    j += 2;
                }
                else
                {
                    j += 1;
                }
                if (j <= length)
                {
                    k += 1;
                }
                if (j > length)
                {
                    return temp.Substring(0, k) + "...";
                }
            }
            return temp;
        }
        #endregion

        #region 获得用户IP
        /// <summary>
        /// 获得用户IP
        /// </summary>
        public static string GetUserIp()
        {
            string ip;
            string[] temp;
            bool isErr = false;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            else
                ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
            if (ip.Length > 15)
                isErr = true;
            else
            {
                temp = ip.Split('.');
                if (temp.Length == 4)
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i].Length > 3) isErr = true;
                    }
                }
                else
                    isErr = true;
            }

            if (isErr)
                return "1.1.1.1";
            else
                return ip;
        }
        #endregion

        #region 根据配置对指定字符串进行 MD5 加密
        /// <summary>
        /// 根据配置对指定字符串进行 MD5 加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMD5(string s)
        {
            //md5加密
            s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();

            return s.ToLower().Substring(8, 16);
        }
        #endregion

        #region 得到字符串长度，一个汉字长度为2
        /// <summary>
        /// 得到字符串长度，一个汉字长度为2
        /// </summary>
        /// <param name="inputString">参数字符串</param>
        /// <returns></returns>
        public static int StrLength(string inputString)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }
            return tempLen;
        }
        #endregion

        #region 截取指定长度字符串
        /// <summary>
        /// 截取指定长度字符串
        /// </summary>
        /// <param name="inputString">要处理的字符串</param>
        /// <param name="len">指定长度</param>
        /// <returns>返回处理后的字符串</returns>
        public static string ClipString(string inputString, int len)
        {
            bool isShowFix = false;
            if (len % 2 == 1)
            {
                isShowFix = true;
                len--;
            }
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }

            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (isShowFix && mybyte.Length > len)
                tempString += "…";
            return tempString;
        }
        #endregion

        #region 获得两个日期的间隔
        /// <summary>
        /// 获得两个日期的间隔
        /// </summary>
        /// <param name="DateTime1">日期一。</param>
        /// <param name="DateTime2">日期二。</param>
        /// <returns>日期间隔TimeSpan。</returns>
        public static TimeSpan DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }
        #endregion

        #region 格式化日期时间
        /// <summary>
        /// 格式化日期时间
        /// </summary>
        /// <param name="dateTime1">日期时间</param>
        /// <param name="dateMode">显示模式</param>
        /// <returns>0-9种模式的日期</returns>
        public static string FormatDate(DateTime dateTime1, string dateMode)
        {
            switch (dateMode)
            {
                case "0":
                    return dateTime1.ToString("yyyy-MM-dd");
                case "1":
                    return dateTime1.ToString("yyyy-MM-dd HH:mm:ss");
                case "2":
                    return dateTime1.ToString("yyyy/MM/dd");
                case "3":
                    return dateTime1.ToString("yyyy年MM月dd日");
                case "4":
                    return dateTime1.ToString("MM-dd");
                case "5":
                    return dateTime1.ToString("MM/dd");
                case "6":
                    return dateTime1.ToString("MM月dd日");
                case "7":
                    return dateTime1.ToString("yyyy-MM");
                case "8":
                    return dateTime1.ToString("yyyy/MM");
                case "9":
                    return dateTime1.ToString("yyyy年MM月");
                default:
                    return dateTime1.ToString();
            }
        }
        #endregion

        #region 得到随机日期
        /// <summary>
        /// 得到随机日期
        /// </summary>
        /// <param name="time1">起始日期</param>
        /// <param name="time2">结束日期</param>
        /// <returns>间隔日期之间的 随机日期</returns>
        public static DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            Random random = new Random();
            DateTime minTime = new DateTime();
            DateTime maxTime = new DateTime();

            System.TimeSpan ts = new System.TimeSpan(time1.Ticks - time2.Ticks);

            // 获取两个时间相隔的秒数
            double dTotalSecontds = ts.TotalSeconds;
            int iTotalSecontds = 0;

            if (dTotalSecontds > System.Int32.MaxValue)
            {
                iTotalSecontds = System.Int32.MaxValue;
            }
            else if (dTotalSecontds < System.Int32.MinValue)
            {
                iTotalSecontds = System.Int32.MinValue;
            }
            else
            {
                iTotalSecontds = (int)dTotalSecontds;
            }


            if (iTotalSecontds > 0)
            {
                minTime = time2;
                maxTime = time1;
            }
            else if (iTotalSecontds < 0)
            {
                minTime = time1;
                maxTime = time2;
            }
            else
            {
                return time1;
            }

            int maxValue = iTotalSecontds;

            if (iTotalSecontds <= System.Int32.MinValue)
                maxValue = System.Int32.MinValue + 1;

            int i = random.Next(System.Math.Abs(maxValue));

            return minTime.AddSeconds(i);
        }
        #endregion

        #region HTML转行成TEXT
        /// <summary>
        /// HTML转行成TEXT
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlToTxt(string strHtml)
        {
            string[] aryReg ={
            @"<script[^>]*?>.*?</script>",
            @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
            @"([\r\n])[\s]+",
            @"&(quot|#34);",
            @"&(amp|#38);",
            @"&(lt|#60);",
            @"&(gt|#62);",
            @"&(nbsp|#160);",
            @"&(iexcl|#161);",
            @"&(cent|#162);",
            @"&(pound|#163);",
            @"&(copy|#169);",
            @"&#(\d+);",
            @"-->",
            @"<!--.*\n"
            };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, string.Empty);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");


            return strOutput;
        }
        #endregion

        #region 判断对象是否为空
        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>        
        public static bool IsNullOrEmpty<T>(T data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }

        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty(object data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }
        #endregion

        #region 验证IP地址是否合法
        /// <summary>
        /// 验证IP地址是否合法
        /// </summary>
        /// <param name="ip">要验证的IP地址</param>        
        public static bool IsIP(string ip)
        {
            //如果为空，认为验证合格
            if (IsNullOrEmpty(ip))
            {
                return true;
            }

            //清除要验证字符串中的空格
            ip = ip.Trim();

            //模式字符串
            string pattern = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";

            //验证
            return RegexUtil.IsMatch(ip, pattern);
        }
        #endregion

        #region  验证EMail是否合法
        /// <summary>
        /// 验证EMail是否合法
        /// </summary>
        /// <param name="email">要验证的Email</param>
        public static bool IsEmail(string email)
        {
            //如果为空，认为验证不合格
            if (IsNullOrEmpty(email))
            {
                return false;
            }

            //清除要验证字符串中的空格
            email = email.Trim();

            //模式字符串
            string pattern = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";

            //验证
            return RegexUtil.IsMatch(email, pattern);
        }
        #endregion

        #region 验证是否为整数
        /// <summary>
        /// 验证是否为整数 如果为空，认为验证不合格 返回false
        /// </summary>
        /// <param name="number">要验证的整数</param>        
        public static bool IsInt(string number)
        {
            //如果为空，认为验证不合格
            if (IsNullOrEmpty(number))
            {
                return false;
            }

            //清除要验证字符串中的空格
            number = number.Trim();

            //模式字符串
            string pattern = @"^[0-9]+[0-9]*$";

            //验证
            return RegexUtil.IsMatch(number, pattern);
        }
        #endregion

        #region 验证是否为数字
        /// <summary>
        /// 验证是否为数字
        /// </summary>
        /// <param name="number">要验证的数字</param>        
        public static bool IsNumber(string number)
        {
            //如果为空，认为验证不合格
            if (IsNullOrEmpty(number))
            {
                return false;
            }

            //清除要验证字符串中的空格
            number = number.Trim();

            //模式字符串
            string pattern = @"^[0-9]+[0-9]*[.]?[0-9]*$";

            //验证
            return RegexUtil.IsMatch(number, pattern);
        }
        #endregion

        #region 验证日期是否合法
        /// <summary>
        /// 验证日期是否合法,对不规则的作了简单处理
        /// </summary>
        /// <param name="date">日期</param>
        public static bool IsDate(ref string date)
        {
            //如果为空，认为验证合格
            if (IsNullOrEmpty(date))
            {
                return true;
            }

            //清除要验证字符串中的空格
            date = date.Trim();

            //替换\
            date = date.Replace(@"\", "-");
            //替换/
            date = date.Replace(@"/", "-");

            //如果查找到汉字"今",则认为是当前日期
            if (date.IndexOf("今") != -1)
            {
                date = DateTime.Now.ToString();
            }

            try
            {
                //用转换测试是否为规则的日期字符
                date = Convert.ToDateTime(date).ToString("d");
                return true;
            }
            catch
            {
                //如果日期字符串中存在非数字，则返回false
                if (!IsInt(date))
                {
                    return false;
                }

                #region 对纯数字进行解析
                //对8位纯数字进行解析
                if (date.Length == 8)
                {
                    //获取年月日
                    string year = date.Substring(0, 4);
                    string month = date.Substring(4, 2);
                    string day = date.Substring(6, 2);

                    //验证合法性
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }
                    if (Convert.ToInt32(month) > 12 || Convert.ToInt32(day) > 31)
                    {
                        return false;
                    }

                    //拼接日期
                    date = Convert.ToDateTime(year + "-" + month + "-" + day).ToString("d");
                    return true;
                }

                //对6位纯数字进行解析
                if (date.Length == 6)
                {
                    //获取年月
                    string year = date.Substring(0, 4);
                    string month = date.Substring(4, 2);

                    //验证合法性
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }
                    if (Convert.ToInt32(month) > 12)
                    {
                        return false;
                    }

                    //拼接日期
                    date = Convert.ToDateTime(year + "-" + month).ToString("d");
                    return true;
                }

                //对5位纯数字进行解析
                if (date.Length == 5)
                {
                    //获取年月
                    string year = date.Substring(0, 4);
                    string month = date.Substring(4, 1);

                    //验证合法性
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }

                    //拼接日期
                    date = year + "-" + month;
                    return true;
                }

                //对4位纯数字进行解析
                if (date.Length == 4)
                {
                    //获取年
                    string year = date.Substring(0, 4);

                    //验证合法性
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }

                    //拼接日期
                    date = Convert.ToDateTime(year).ToString("d");
                    return true;
                }
                #endregion

                return false;
            }
        }
        #endregion

        #region 验证身份证是否合法
        /// <summary>
        /// 验证身份证是否合法
        /// </summary>
        /// <param name="idCard">要验证的身份证</param>        
        public static bool IsIdCard(string idCard)
        {
            //如果为空，认为验证合格
            if (IsNullOrEmpty(idCard))
            {
                return true;
            }

            //清除要验证字符串中的空格
            idCard = idCard.Trim();

            //模式字符串
            StringBuilder pattern = new StringBuilder();
            pattern.Append(@"^(11|12|13|14|15|21|22|23|31|32|33|34|35|36|37|41|42|43|44|45|46|");
            pattern.Append(@"50|51|52|53|54|61|62|63|64|65|71|81|82|91)");
            pattern.Append(@"(\d{13}|\d{15}[\dx])$");

            //验证
            return RegexUtil.IsMatch(idCard, pattern.ToString());
        }
        #endregion

        #region 检测客户的输入中是否有危险字符串
        /// <summary>
        /// 检测客户输入的字符串是否有效,并将原始字符串修改为有效字符串或空字符串。
        /// 当检测到客户的输入中有攻击性危险字符串,则返回false,有效返回true。
        /// </summary>
        /// <param name="input">要检测的字符串</param>
        public static bool IsValidInput(ref string input)
        {
            try
            {
                if (IsNullOrEmpty(input))
                {
                    //如果是空值,则跳出
                    return true;
                }
                else
                {
                    //替换单引号
                    input = input.Replace("'", "''").Trim();

                    //检测攻击性危险字符串
                    string testString = "and |or |exec |insert |select |delete |update |count |chr |mid |master |truncate |char |declare ";
                    string[] testArray = testString.Split('|');
                    foreach (string testStr in testArray)
                    {
                        if (input.ToLower().IndexOf(testStr) != -1)
                        {
                            //检测到攻击字符串,清空传入的值
                            input = "";
                            return false;
                        }
                    }

                    //未检测到攻击字符串
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


            public static readonly char RowFilter = (char)7;
            public static readonly char ColFilter = (char)9;
            private static readonly char RowFilterN = (char)11;
            private static readonly char ColFilterN = (char)12;

            public static long ToInt64(object data)
            {
                if (data == null)
                    return 0;
                if (data is Enum)
                    return (long)data;

                long result;
                var success = long.TryParse(data.ToString(), out result);
                if (success)
                    return result;
                try
                {
                    return Convert.ToInt64(data);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            public static string[,] StrListToArray(string strlist)
            {
                string[,] arr2 = null;
                StrListToArray(strlist, ref arr2);
                return arr2;
            }
            /// <summary>
            /// 默认字符串列表转为新的列表
            /// </summary>
            /// <param name="strlist">要转换的字符串</param>
            /// <param name="NewColSplitChar">新的列分隔符</param>
            /// <param name="NewRowSplitChar">新的行分隔符</param>
            public static string StrListToNew(string strlist, char NewColSplitChar, char NewRowSplitChar)
            {
                return strlist.Replace(RowFilter, NewRowSplitChar).Replace(ColFilter, NewColSplitChar); ;
            }
            /// <summary>
            /// 将字符串转为数组  弃用
            /// </summary>
            /// <param name="strlist"></param>
            /// <param name="Strarray"></param>
            private static void StrListToArray(string strlist, ref string[,] Strarray)
            {
                string[,] tArray = null;
                try
                {
                    //将字符串转为数组


                    if (string.IsNullOrEmpty(strlist)) { return; }

                    var Rows = strlist.Split((char)7);
                    for (int i = 0; i < Rows.Length; i++)
                    {
                        var Cols = Rows[i].Split((char)9);
                        var c = Cols.Length;
                        if (i == 0)
                        {

                            if (c == -1) { c = 0; }

                            tArray = new string[c, Rows.Length];
                        }
                        for (int j = 0; j < c; j++)
                        {
                            tArray[j, i] = Cols[j].Trim();
                        }
                    }
                    Strarray = tArray;
                }
                catch
                { }
                finally
                {
                    //Cols = null;
                    //Rows = null;
                    //tArray = null;
                    tArray = null;
                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();
                    //GC.Collect();
                }
            }
            /// <summary>
            /// 字符串转为数组
            /// </summary>
            /// <param name="strlist">要转换的字符串</param>
            /// <param name="ColSplitChar">列分隔符</param>
            /// <param name="RowSplitChar">行分隔符</param>
            /// <param name="Strarray">返回数组</param>
            public static string[,] StrListToArray(string strlist, char ColSplitChar, char RowSplitChar)
            {
                try
                {
                    //将字符串转为数组
                    string[,] tArray = null;
                    if (string.IsNullOrEmpty(strlist)) { return null; }
                    var Rows = strlist.Split(RowSplitChar);
                    for (int i = 0; i < Rows.Length; i++)
                    {
                        var Cols = Rows[i].Split(ColSplitChar);
                        int c = Cols.Length;
                        if (i == 0)
                        {
                            tArray = new string[c, Rows.Length];
                        }
                        for (int j = 0; j < c; j++)
                        {
                            tArray[j, i] = Cols[j].Trim();
                        }
                    }
                    return tArray;
                }
                catch
                { }
                finally
                {
                    //Cols = null;
                    //Rows = null;
                    //tArray = null;
                    //GC.GetTotalMemory(false);
                    //GC.Collect();
                    //GC.GetTotalMemory(true);
                    ////GC.WaitForPendingFinalizers();            
                }
                return null;
            }

            /// <summary>
            /// 把数组转换为字符串
            /// </summary>
            /// ColFilter=(char)9  RowFilter = (char)7
            /// <param name="ArrList">数组</param>
            /// <returns>字符串</returns>
            public static string ArrayToStrList(string[,] ArrList)
            {
                if (ArrList == null) return "";
                var sb = new StringBuilder();
                for (int i = 0; i < ArrList.GetLength(1); i++)
                {
                    if (i > 0)
                    {
                        sb.Append(RowFilterN);
                    }

                    for (int j = 0; j < ArrList.GetLength(0); j++)
                    {
                        if (j != 0)
                        {
                            sb.Append(ColFilterN);
                        }
                        var str = ArrList[j, i];
                        if (str != null)
                            sb.Append(str);

                    }
                }

                sb.Replace(RowFilter.ToString(), "").Replace(ColFilter.ToString(), "").Replace(RowFilterN, RowFilter).Replace(ColFilterN, ColFilter);
                return sb.ToString();
            }

            public static void StrListToArrayNuLL(string[,] strlistarray, ref string[,] Strarray)
            {
                try
                {
                    //将数组空字符串去除

                    //          public static readonly char RowFilter = (char)7;
                    //public static readonly char ColFilter = (char)9;

                    var sb = new StringBuilder();
                    Strarray = new string[strlistarray.GetLength(0), strlistarray.GetLength(1)];
                    int i; int j;
                    if (strlistarray.Length == 0) { return; }
                    for (i = 0; i < strlistarray.GetLength(1); i++)
                    {
                        if (i > 0)
                        {
                            sb.Append(RowFilter);
                        }
                        string str2 = "";
                        for (j = 0; j < strlistarray.GetLength(0); j++)
                        {
                            if (!string.IsNullOrEmpty(strlistarray[j, i]))
                            {
                                if (!string.IsNullOrEmpty(str2))
                                {
                                    sb.Append(ColFilter);
                                }

                                if (!string.IsNullOrEmpty(str2))
                                {
                                    sb.Append(strlistarray[j, i]);
                                }
                                else { str2 = strlistarray[j, i]; }
                            }
                        }
                        sb.Append(str2);
                    }
                    Strarray = StrListToArray(sb.ToString());
                }
                catch
                { }
                finally
                {
                    //Cols = null;
                    //Rows = null;
                    //tArray = null;
                    //GC.GetTotalMemory(false);
                    //GC.Collect();
                    //GC.GetTotalMemory(true);
                    ////GC.WaitForPendingFinalizers();            
                }
            }




            public static void Resize<T>(ref T[] array, int newSize)
            {
                try
                {
                    if (newSize < 0)
                    {
                        throw new ArgumentOutOfRangeException("newSize", "ArgumentOutOfRange_NeedNonNegNum");
                    }
                    T[] sourceArray = array;
                    if (sourceArray == null)
                    {
                        array = new T[newSize];
                    }
                    else if (sourceArray.Length != newSize)
                    {
                        T[] destinationArray = new T[newSize];
                        Array.Copy(sourceArray, 0, destinationArray, 0, (sourceArray.Length > newSize) ? newSize : sourceArray.Length);
                        array = destinationArray;
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    throw ex;
                }
            }



            public static int Multi_Find(string[,] arrList, string strFiled, string strFindText)
            {
                int i;
                for (i = 0; i < arrList.GetLength(1); i++)
                {
                    if (arrList[0, i].ToLower() == strFiled.ToLower() && arrList[1, i].ToLower() == strFindText.ToLower())
                    {
                        return i;
                    }
                }
                return -1;
            }
            /// <summary>
            /// 在指定的数组中查找符合的值，返回行索引
            /// </summary>
            /// <param name="StrArray"></param>
            /// <param name="vValue"></param>
            /// <returns></returns>
            public static int ArrayFind(string[,] StrArray, string vValue, int Index)
            {
                int result = -1;
                for (int i = 0; i < StrArray.GetLength(1); i++)
                {
                    if (StrArray[Index, i] == vValue)
                    {
                        result = i;
                        break;
                    }
                }
                return result;
            }
            public static void RaiseMsg(string sModuleName, String sDescription, Exception ex)
            {
                if (ex.Message == sDescription)
                {
                    throw new Exception(ex.Message);
                }
                else
                {
                    throw new Exception(ex.Message + " " + sDescription);
                }
                //throw new Exception(sModuleName + ":" + ex.Message + ":" + sDescription+":"+ex.StackTrace);
            }
            public static void RaiseMsg(string sModuleName, String sDescription)
            {
                throw new Exception(sDescription);
            }
            /// <summary>
            /// 将数据表转换成二维数组
            /// </summary>
            /// <param name="source"></param>
            /// <returns></returns>
            public static string[,] DataTableToArray(DataTable dt)
            {
                int col = dt.Columns.Count;
                int row = dt.Rows.Count;
                string[,] array = new string[col, row];
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        array[j, i] = Tostring(dt.Rows[i][j]);
                    }
                }
                return array;
            }

            /// <summary>
            /// 把数据表转换为字符串
            /// </summary>
            /// ColFilter=(char)9  RowFilter = (char)7
            /// <param name="ArrList">数组</param>
            /// <returns>字符串</returns>
            public static string DataTableToStrList(DataTable dt)
            {

                if (dt == null) return "";

                int col = dt.Columns.Count;
                int row = dt.Rows.Count;
                if (row == 0) return "";
                string strDate = DateTime.Now.ToString();
                var sb = new StringBuilder();
                for (int i = 0; i < row; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(RowFilterN);
                    }

                    for (int j = 0; j < col; j++)
                    {
                        if (j != 0)
                        {
                            sb.Append(ColFilterN);
                        }

                        object p = dt.Rows[i][j];
                        if (p != null)
                            if (p is DateTime)//判断时间 时分秒是否为0如果为0移除掉
                            {
                                DateTime dst = Convert.ToDateTime(p);
                                if (dst.Hour == 00 && dst.Minute == 00 && dst.Second == 00)
                                {
                                    string time = dst.ToShortDateString();
                                    sb.Append(time);
                                }
                                else
                                    sb.Append(dst.ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                            else if (p is decimal || p is double || p is float || p is Single)
                            {
                                var tt = Convert.ToDecimal(p);
                                sb.Append(tt.ToString("0.####"));
                            }
                            //else if (p is int || p is Int16 || p is Int64)
                            //{
                            //    var tt = Convert.ToInt32(p);
                            //    //if (tt == 0)
                            //    //    sb.Append("");
                            //    //else
                            //        sb.Append(tt.ToString("#"));
                            //}
                            else
                                sb.Append(p.ToString());
                        else
                            sb.Append("");
                    }

                }

                sb = sb.Replace(RowFilter.ToString(), "");
                sb = sb.Replace(ColFilter.ToString(), "");
                sb = sb.Replace(RowFilterN, RowFilter);
                sb = sb.Replace(ColFilterN, ColFilter);

                return sb.ToString();
            }
            public static string GetStringFromRs(DataTable Rs, string colname, string separator)
            {
                if (Rs == null || Rs.Rows.Count == 0)
                    return "";
                string[] tt = new string[Rs.Rows.Count];
                int i = 0;
                foreach (DataRow oR in Rs.Rows)
                {
                    tt[i] = Tostring(oR[colname]);
                    i += 1;
                }
                return string.Join(separator, tt);
            }

            /// <summary>
            /// 将一个数据表转换成一个JSON字符串，在客户端可以直接转换成二维数组。
            /// </summary>
            /// <param name="source">需要转换的表。</param>
            /// <returns></returns>
            //public static string DataTableToJson(DataTable source)
            //{

            //    StringBuilder sb = new StringBuilder("{\"name\":");
            //    sb.Append("\"" + source.TableName + "\",");
            //    sb.Append("\"Columns\":[");
            //    foreach (DataColumn col in source.Columns)
            //    {
            //        sb.Append("{");
            //        sb.AppendFormat(null,"\"name\":\"{0}\",\"type\":\"{1}\"", col.ColumnName, col.DataType.ToString());
            //        sb.Append("},");
            //    }
            //    sb.Remove(sb.Length - 1, 1);
            //    sb.Append("],");
            //    sb.Append("\"Data\":[");
            //    foreach (DataRow row in source.Rows)
            //    {
            //        sb.Append("[");
            //        for (int i = 0; i < source.Columns.Count; i++)
            //        {

            //            string strValue = row[i].ToString().Replace("\"", "");

            //            sb.Append("\"" + strValue + "\",");
            //        }
            //        sb.Remove(sb.Length - 1, 1);
            //        sb.Append("],");
            //    }
            //    sb.Remove(sb.Length - 1, 1);
            //    sb.Append("]");
            //    sb.Append("}");
            //    return sb.ToString();
            //}



            public static bool Tobool(object p)
            {
                try
                {
                    if (p == null || p == System.DBNull.Value) return false;
                    string str1 = p.ToString();
                    if (string.IsNullOrEmpty(str1)) return false;
                    if (str1 == "0") return false;
                    if (str1 == "1") return true;

                    return bool.Parse(str1);
                }
                catch
                {
                    return false;
                }

            }
            public static int Toint(object p)
            {
                try
                {
                    if (p == null || p == System.DBNull.Value)
                    {
                        return 0;
                    }
                    string str1 = p.ToString().Trim();
                    if (string.IsNullOrEmpty(str1))
                    {
                        return 0;
                    }
                    return (int)double.Parse(str1);
                }
                catch
                {
                    return 0;
                }
            }
            public static decimal ToDecimal2(object p)
            {
                try
                {
                    if (p == null || p == System.DBNull.Value)
                    {
                        return 0;
                    }
                    string str1 = p.ToString().Trim();
                    if (string.IsNullOrEmpty(str1))
                    {
                        return 0;
                    }
                    return Convert.ToDecimal(str1);
                }
                catch
                {
                    return 0;
                }
            }
            public static double Todouble(object p)
            {
                try
                {
                    if (p == null || p == System.DBNull.Value) return 0;
                    string str1 = p.ToString().Trim();
                    if (string.IsNullOrEmpty(str1))
                    {
                        return 0;
                    }
                    return double.Parse(str1);
                }
                catch
                {
                    return 0;
                }
            }
            public static long Tolong(object p)
            {
                try
                {
                    if (p == null || p == System.DBNull.Value) return 0;
                    string str1 = p.ToString().Trim();
                    if (string.IsNullOrEmpty(str1))
                    {
                        return 0;
                    }
                    return long.Parse(str1);
                }
                catch
                {
                    return 0;
                }
            }
            private static bool IshaveLetter(string tt)
            {
                foreach (char t in tt)
                {
                    if (Char.IsLetter(t))
                    {
                        return true;
                    }
                }
                return false;
            }

            public static string Tostring(object p)
            {
                try
                {
                    if (p == null || p == System.DBNull.Value) return "";
                    string str1 = p.ToString();

                    return str1;
                }
                catch
                {
                    return "";
                }
            }
            public static DateTime Todatetime(object p)
            {
                try
                {
                    if (p == null || p == System.DBNull.Value)
                    {
                        return DateTime.MinValue;
                    }
                    string str1 = p.ToString();
                    if (string.IsNullOrEmpty(str1))
                    {
                        return DateTime.MinValue;
                    }
                    return DateTime.Parse(str1);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }

            public static DateTime ToDateTime2(object p)
            {
                try
                {
                    if (p == null || p == System.DBNull.Value)
                    {
                        return DateTime.Now;
                    }
                    string str1 = p.ToString();
                    if (string.IsNullOrEmpty(str1))
                    {
                        return DateTime.Now;
                    }
                    return DateTime.Parse(str1);
                }
                catch
                {
                    return DateTime.Now;
                }
            }

            /// <summary>
            /// 是否在wince平台下运行
            /// </summary>
            public static bool IsWinCe
            {
                get
                {
                    return (Environment.OSVersion.Platform == PlatformID.WinCE);
                }

            }

            public static void SaveJsonFile(string fileName, string theFileAsString)
            {

                Encoding encoding = Encoding.GetEncoding("UTF-8");
                StreamWriter writer = new StreamWriter(fileName, false, encoding);
                writer.Write(theFileAsString);
                writer.Close();
            }


            /// <summary>
            /// 通过一个字符串获取三个Key
            /// </summary>
            /// <param name="sCode"></param>
            /// <param name="vCode"></param>
            /// <param name="vSku"></param>
            /// <param name="vXiangHao"></param>
            public static void GetKey(string KeyAll, ref string key1, ref string key2, ref string key3)
            {
                string[] arr = KeyAll.Split(',');
                int t = arr.Length;
                key1 = "";
                key2 = "";
                key3 = "";
                if (t == 0) { key1 = KeyAll; }
                if (t > 0) { key1 = (arr[0]).Trim(); }
                if (t > 1) { key2 = arr[1]; }
                if (t > 2) { key3 = arr[2]; }
            }
            /// <summary>
            /// 通过一个字符串获取四个Key
            /// </summary>
            /// <param name="sCode"></param>
            /// <param name="vCode"></param>
            /// <param name="vSku"></param>
            /// <param name="vXiangHao"></param>
            public static void GetKey(string KeyAll, ref string key1, ref string key2, ref string key3, ref string key4)
            {
                string[] arr = KeyAll.Split(',');
                int t = arr.Length;
                key1 = "";
                key2 = "";
                key3 = "";
                key4 = "";
                if (t == 0) { key1 = KeyAll; }
                if (t > 0) { key1 = (arr[0]).Trim(); }
                if (t > 1) { key2 = arr[1]; }
                if (t > 2) { key3 = arr[2]; }
                if (t > 3) { key4 = arr[3]; }
            }
            /// <summary>
            /// 通过一个字符串获取三个以上的Key
            /// </summary>
            /// <param name="KeyAll"></param>
            /// <param name="key1"></param>
            /// <param name="key2"></param>
            /// <param name="key3"></param>
            public static void GetKey(string KeyAll, ref string key1, ref string key2, ref string key3, ref string key4, ref string key5, ref string key6)
            {
                string[] arr = KeyAll.Split(',');
                int t = arr.Length;
                key1 = "";
                key2 = "";
                key3 = "";
                key4 = "";
                key5 = "";
                key6 = "";
                if (t == 0) { key1 = KeyAll; }
                if (t > 0) { key1 = (arr[0]).Trim(); }
                if (t > 1) { key2 = arr[1]; }
                if (t > 2) { key3 = arr[2]; }
                if (t > 3) { key4 = arr[3]; }
                if (t > 4) { key5 = arr[4]; }
                if (t > 5) { key6 = arr[5]; }
            }




            public static string trans_no(string aa, int strlen)
            {
                int i;
                i = strlen - aa.Length;
                if (i > 0)
                {
                    return toString(i, "0") + aa;
                }
                else
                {
                    return aa.Substring(aa.Length - strlen, strlen);
                }
            }

        public static string toString(int count, string chr)
        {
            string result = string.Empty;
            for (int i = 0; i < count; i++)
            {
                result += chr;
            }
            return result;
        }

        /// <summary>
        /// 校验是否正整数
        /// </summary>
        /// <param name="inputStr">待校验的字符串</param>
        /// <returns></returns>
        public static bool IsNumberByRegex(string inputStr)
            {
                string regStr = @"^\d+$";
                return Regex.IsMatch(inputStr, regStr);
            }

            public static double Round(double money, int dicimalLength)
            {

#if WindowsCE
            return Math.Round(money, dicimalLength);
#else
                return Math.Round(money, dicimalLength, MidpointRounding.AwayFromZero);
#endif

            }

            public static string EnumToString<T>(object itemValue, bool showvalue = true)
            {
                int value = Toint(itemValue);
                try
                {
                    if (Enum.IsDefined(typeof(T), value))
                    {
                        if (showvalue)
                        {
                            return Environment.NewLine + "-->状态[" + value + ":"
                                + Enum.GetName(typeof(T), value) + "]";
                        }
                        else
                        {
                            return Environment.NewLine + "-->状态[" + Enum.GetName(typeof(T), value) + "]";
                        }
                    }
                    else
                    {
                        return Environment.NewLine + "-->状态[" + value + "]";
                    }
                }
                catch
                {
                    return Environment.NewLine + "-->状态[" + value + "]";
                }
                //string r = ((queryOrder)10010).ToString();
                //string x3 = Enum.GetName(typeof(queryOrder), 10010);
                //bool isd = Enum.IsDefined(typeof(queryOrder), 10086); 

                //Enum.Parse(typeof(T), itemValue.ToString()).ToString();
            }

            /**
             * 生成时间戳，标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数
             * @return 时间戳
             */
            /// <summary>
            ///生成时间戳
            /// </summary>
            public static string GenerateTimeStamp()
            {
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return Convert.ToInt64(ts.TotalSeconds).ToString();
            }


            /// <summary>  
            /// 时间戳转为C#格式时间  
            /// </summary>  
            /// <param name="timeStamp">Unix时间戳格式</param>  
            /// <returns>C#格式时间</returns>  
            public static DateTime GetTime(string timeStamp)
            {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(timeStamp + "0000000");
                TimeSpan toNow = new TimeSpan(lTime);
                return dtStart.Add(toNow);
            }
            /// <summary>
            /// 时间戳转为C#格式时间  dev874 2019-04-04
            /// </summary>
            /// <param name="timeStamp">Unix时间戳格式</param>
            /// <param name="byms">是否按毫秒计算</param>
            /// <returns>C#格式时间</returns>
            public static DateTime GetTime(string timeStamp, bool byms = false)//按毫秒
            {
                if (byms)
                {
                    timeStamp = timeStamp + "0000";
                }
                else
                {
                    timeStamp = timeStamp + "0000000";
                }
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(timeStamp);
                TimeSpan toNow = new TimeSpan(lTime);
                return dtStart.Add(toNow);
            }

            /// <summary>
            /// 生成DateTime的时间戳
            /// </summary>
            /// <returns></returns>
            public static DateTime GetTimeStamp()
            {
                return DateTime.Now.ToLocalTime();
            }
            public static bool IsNumeric(string str) //接收一个string类型的参数,保存到str里
            {
                if (str == null || str.Length == 0)    //验证这个参数是否为空
                    return false;                           //是，就返回False
                ASCIIEncoding ascii = new ASCIIEncoding();//new ASCIIEncoding 的实例
                byte[] bytestr = ascii.GetBytes(str);         //把string类型的参数保存到数组里

                foreach (byte c in bytestr)                   //遍历这个数组里的内容
                {
                    if (c < 48 || c > 57)                          //判断是否为数字
                    {
                        return false;                              //不是，就返回False
                    }
                }
                return true;                                        //是，就返回True
            }

            //备注 数字，字母的ASCII码对照表
            /*
            0~9数字对应十进制48－57
            a~z字母对应的十进制97－122十六进制61－7A
            A~Z字母对应的十进制65－90十六进制41－5A
                    /**
                     * 生成随机串，随机串包含字母或数字
                     * @return 随机串
                     */
            /// <summary>
            /// 生成随机数串
            /// </summary>
            public static string GenerateNonceStr()
            {
                return Guid.NewGuid().ToString().Replace("-", "");
            }

            public static int CountStr(string str, string s)
            {
                int count = 0;
                while (str.Contains(s))
                {
                    count += 1;
                    str = str.Substring(str.IndexOf(s) + s.Length);
                }
                return count;
            }


            public static DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)//PageIndex表示第几页，PageSize表示每页的记录数
            {

                if (PageIndex == 0)
                    return dt;//0页代表每页数据，直接返回

                DataTable newdt = dt.Copy();
                newdt.Clear();//copy dt的框架

                int rowbegin = (PageIndex - 1) * PageSize;
                int rowend = PageIndex * PageSize;

                if (rowbegin >= dt.Rows.Count)
                    return newdt;//源数据记录数小于等于要显示的记录，直接返回dt

                if (rowend > dt.Rows.Count)
                    rowend = dt.Rows.Count;
                for (int i = rowbegin; i <= rowend - 1; i++)
                {
                    DataRow newdr = newdt.NewRow();
                    DataRow dr = dt.Rows[i];
                    foreach (DataColumn column in dt.Columns)
                    {
                        newdr[column.ColumnName] = dr[column.ColumnName];
                    }
                    newdt.Rows.Add(newdr);
                }
                return newdt;
            }

            public static bool IsDate(string strDate)
            {
                try
                {
                    DateTime.Parse(strDate);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            /// <summary>
            /// 获取文件的真实后缀名。目前只支持JPG, GIF, PNG, BMP四种图片文件。
            /// </summary>
            /// <param name="fileData">文件字节流</param>
            /// <returns>JPG, GIF, PNG or null</returns>
            public static string GetFileSuffix(byte[] fileData)
            {
                if (fileData == null || fileData.Length < 10)
                {
                    return null;
                }

                if (fileData[0] == 'G' && fileData[1] == 'I' && fileData[2] == 'F')
                {
                    return "GIF";
                }
                else if (fileData[1] == 'P' && fileData[2] == 'N' && fileData[3] == 'G')
                {
                    return "PNG";
                }
                else if (fileData[6] == 'J' && fileData[7] == 'F' && fileData[8] == 'I' && fileData[9] == 'F')
                {
                    return "JPG";
                }
                else if (fileData[0] == 'B' && fileData[1] == 'M')
                {
                    return "BMP";
                }
                else
                {
                    return null;
                }
            }

            /// <summary>
            /// 获取文件的真实媒体类型。目前只支持JPG, GIF, PNG, BMP四种图片文件。
            /// </summary>
            /// <param name="fileData">文件字节流</param>
            /// <returns>媒体类型</returns>
            public static string GetMimeType(byte[] fileData)
            {
                string suffix = GetFileSuffix(fileData);
                string mimeType;

                switch (suffix)
                {
                    case "JPG": mimeType = "image/jpeg"; break;
                    case "GIF": mimeType = "image/gif"; break;
                    case "PNG": mimeType = "image/png"; break;
                    case "BMP": mimeType = "image/bmp"; break;
                    default: mimeType = "application/octet-stream"; break;
                }

                return mimeType;
            }

            /// <summary>
            /// 根据文件后缀名获取文件的媒体类型。
            /// </summary>
            /// <param name="fileName">带后缀的文件名或文件全名</param>
            /// <returns>媒体类型</returns>
            public static string GetMimeType(string fileName)
            {
                string mimeType;
                fileName = fileName.ToLower();

                if (fileName.EndsWith(".bmp", StringComparison.CurrentCulture))
                {
                    mimeType = "image/bmp";
                }
                else if (fileName.EndsWith(".gif", StringComparison.CurrentCulture))
                {
                    mimeType = "image/gif";
                }
                else if (fileName.EndsWith(".jpg", StringComparison.CurrentCulture) || fileName.EndsWith(".jpeg", StringComparison.CurrentCulture))
                {
                    mimeType = "image/jpeg";
                }
                else if (fileName.EndsWith(".png", StringComparison.CurrentCulture))
                {
                    mimeType = "image/png";
                }
                else
                {
                    mimeType = "application/octet-stream";
                }

                return mimeType;
            }


     
        #region As
        /// <summary>
        /// Used to simplify and beautify casting an object to a type. 
        /// </summary>
        /// <typeparam name="T">Type to be casted</typeparam>
        /// <param name="this">object to cast</param>
        /// <returns>Casted object</returns>
        public static T As<T>( object @this) where T : class
        {
            return (T)@this;
        }
        #endregion

        #region AsOrDefault
        /// <summary>
        /// An object extension method that converts the @this to an or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A T.</returns>
        public static T AsOrDefault<T>( object @this)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// An object extension method that converts the @this to an or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A T.</returns>
        public static T AsOrDefault<T>( object @this, T defaultValue)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts the @this to an or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>A T.</returns>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_AsOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void AsOrDefault()
        ///               {
        ///                   // Type
        ///                   object intValue = 1;
        ///                   object invalidValue = &quot;Fizz&quot;;
        /// 
        ///                   // Exemples
        ///                   var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
        ///                   var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
        ///                   int result3 = invalidValue.AsOrDefault(3); // return 3;
        ///                   int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(0, result2);
        ///                   Assert.AreEqual(3, result3);
        ///                   Assert.AreEqual(4, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_AsOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void AsOrDefault()
        ///               {
        ///                   // Type
        ///                   object intValue = 1;
        ///                   object invalidValue = &quot;Fizz&quot;;
        /// 
        ///                   // Exemples
        ///                   var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
        ///                   var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
        ///                   int result3 = invalidValue.AsOrDefault(3); // return 3;
        ///                   int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(0, result2);
        ///                   Assert.AreEqual(3, result3);
        ///                   Assert.AreEqual(4, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///           using Z.ExtensionMethods.Object;
        ///           
        ///           namespace ExtensionMethods.Examples
        ///           {
        ///               [TestClass]
        ///               public class System_Object_AsOrDefault
        ///               {
        ///                   [TestMethod]
        ///                   public void AsOrDefault()
        ///                   {
        ///                       // Type
        ///                       object intValue = 1;
        ///                       object invalidValue = &quot;Fizz&quot;;
        ///           
        ///                       // Exemples
        ///                       var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
        ///                       var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
        ///                       int result3 = invalidValue.AsOrDefault(3); // return 3;
        ///                       int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
        ///           
        ///                       // Unit Test
        ///                       Assert.AreEqual(1, result1);
        ///                       Assert.AreEqual(0, result2);
        ///                       Assert.AreEqual(3, result3);
        ///                       Assert.AreEqual(4, result4);
        ///                   }
        ///               }
        ///           }
        ///     </code>
        /// </example>
        public static T AsOrDefault<T>( object @this, Func<T> defaultValueFactory)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts the @this to an or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>A T.</returns>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_AsOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void AsOrDefault()
        ///               {
        ///                   // Type
        ///                   object intValue = 1;
        ///                   object invalidValue = &quot;Fizz&quot;;
        /// 
        ///                   // Exemples
        ///                   var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
        ///                   var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
        ///                   int result3 = invalidValue.AsOrDefault(3); // return 3;
        ///                   int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(0, result2);
        ///                   Assert.AreEqual(3, result3);
        ///                   Assert.AreEqual(4, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_AsOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void AsOrDefault()
        ///               {
        ///                   // Type
        ///                   object intValue = 1;
        ///                   object invalidValue = &quot;Fizz&quot;;
        /// 
        ///                   // Exemples
        ///                   var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
        ///                   var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
        ///                   int result3 = invalidValue.AsOrDefault(3); // return 3;
        ///                   int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(0, result2);
        ///                   Assert.AreEqual(3, result3);
        ///                   Assert.AreEqual(4, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///           using Z.ExtensionMethods.Object;
        ///           
        ///           namespace ExtensionMethods.Examples
        ///           {
        ///               [TestClass]
        ///               public class System_Object_AsOrDefault
        ///               {
        ///                   [TestMethod]
        ///                   public void AsOrDefault()
        ///                   {
        ///                       // Type
        ///                       object intValue = 1;
        ///                       object invalidValue = &quot;Fizz&quot;;
        ///           
        ///                       // Exemples
        ///                       var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
        ///                       var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
        ///                       int result3 = invalidValue.AsOrDefault(3); // return 3;
        ///                       int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
        ///           
        ///                       // Unit Test
        ///                       Assert.AreEqual(1, result1);
        ///                       Assert.AreEqual(0, result2);
        ///                       Assert.AreEqual(3, result3);
        ///                       Assert.AreEqual(4, result4);
        ///                   }
        ///               }
        ///           }
        ///     </code>
        /// </example>
        public static T AsOrDefault<T>( object @this, Func<object, T> defaultValueFactory)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return defaultValueFactory(@this);
            }
        }
        #endregion

     
        

        #region ToValueType
      

        #region ToLongOrDefault
        /// <summary>
        /// An object extension method that converts this object to a long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToLongOrDefault( object @this)
        {
            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return default(long);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToLongOrDefault( object @this, long defaultValue)
        {
            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToLongOrDefault( object @this, long defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToLongOrDefault( object @this, Func<long> defaultValueFactory)
        {
            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToLongOrDefault( object @this, Func<long> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion


        #region ToDoubleOrDefault
        /// <summary>
        /// An object extension method that converts this object to a double or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a double.</returns>
        public static double ToDoubleOrDefault( object @this)
        {
            try
            {
                return Convert.ToDouble(@this);
            }
            catch (Exception)
            {
                return default(double);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a double or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a double.</returns>
        public static double ToDoubleOrDefault( object @this, double defaultValue)
        {
            try
            {
                return Convert.ToDouble(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a double or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a double.</returns>
        public static double ToDoubleOrDefault( object @this, double defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToDouble(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a double or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a double.</returns>
        public static double ToDoubleOrDefault( object @this, Func<double> defaultValueFactory)
        {
            try
            {
                return Convert.ToDouble(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a double or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a double.</returns>
        public static double ToDoubleOrDefault( object @this, Func<double> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToDouble(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToBooleanOrDefault
        /// <summary>
        /// An object extension method that converts this object to a boolean or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a bool.</returns>
        public static bool ToBooleanOrDefault( object @this)
        {
            try
            {
                return Convert.ToBoolean(@this);
            }
            catch (Exception)
            {
                return default(bool);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a boolean or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">true to default value.</param>
        /// <returns>The given data converted to a bool.</returns>
        public static bool ToBooleanOrDefault( object @this, bool defaultValue)
        {
            try
            {
                return Convert.ToBoolean(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// An object extension method that converts this object to a boolean or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">true to default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a bool.</returns>
        public static bool ToBooleanOrDefault( object @this, bool defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToBoolean(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a boolean or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a bool.</returns>
        public static bool ToBooleanOrDefault( object @this, Func<bool> defaultValueFactory)
        {
            try
            {
                return Convert.ToBoolean(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a boolean or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a bool.</returns>
        public static bool ToBooleanOrDefault( object @this, Func<bool> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToBoolean(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

     

        #region ToDateTimeOrDefault
        /// <summary>
        /// An object extension method that converts this object to a date time or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a DateTime.</returns>
        public static DateTime ToDateTimeOrDefault( object @this)
        {
            try
            {
                return Convert.ToDateTime(@this);
            }
            catch (Exception)
            {
                return default(DateTime);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a date time or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a DateTime.</returns>
        public static DateTime ToDateTimeOrDefault( object @this, DateTime defaultValue)
        {
            try
            {
                return Convert.ToDateTime(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a date time or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a DateTime.</returns>
        public static DateTime ToDateTimeOrDefault( object @this, DateTime defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToDateTime(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a date time or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a DateTime.</returns>
        public static DateTime ToDateTimeOrDefault( object @this, Func<DateTime> defaultValueFactory)
        {
            try
            {
                return Convert.ToDateTime(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a date time or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a DateTime.</returns>
        public static DateTime ToDateTimeOrDefault( object @this, Func<DateTime> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToDateTime(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToDateTimeOffSet
        /// <summary>
        /// An object extension method that converts the @this to a date time off set.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a DateTimeOffset.</returns>
        public static DateTimeOffset ToDateTimeOffSet( object @this)
        {
            return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
        }
        #endregion

        #region ToDateTimeOffSetOrDefault
        /// <summary>
        /// An object extension method that converts this object to a date time off set or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a DateTimeOffset.</returns>
        public static DateTimeOffset ToDateTimeOffSetOrDefault( object @this)
        {
            try
            {
                return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
            }
            catch (Exception)
            {
                return default(DateTimeOffset);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a date time off set or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a DateTimeOffset.</returns>
        public static DateTimeOffset ToDateTimeOffSetOrDefault( object @this, DateTimeOffset defaultValue)
        {
            try
            {
                return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a date time off set or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a DateTimeOffset.</returns>
        public static DateTimeOffset ToDateTimeOffSetOrDefault( object @this, DateTimeOffset defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a date time off set or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a DateTimeOffset.</returns>
        public static DateTimeOffset ToDateTimeOffSetOrDefault( object @this, Func<DateTimeOffset> defaultValueFactory)
        {
            try
            {
                return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a date time off set or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a DateTimeOffset.</returns>
        public static DateTimeOffset ToDateTimeOffSetOrDefault( object @this, Func<DateTimeOffset> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToByte
        /// <summary>
        /// An object extension method that converts the @this to a byte.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a byte.</returns>
        public static byte ToByte( object @this)
        {
            return Convert.ToByte(@this);
        }
        #endregion

        #region ToByteOrDefault
        /// <summary>
        /// An object extension method that converts this object to a byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a byte.</returns>
        public static byte ToByteOrDefault( object @this)
        {
            try
            {
                return Convert.ToByte(@this);
            }
            catch (Exception)
            {
                return default(byte);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a byte.</returns>
        public static byte ToByteOrDefault( object @this, byte defaultValue)
        {
            try
            {
                return Convert.ToByte(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a byte.</returns>
        public static byte ToByteOrDefault( object @this, byte defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToByte(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a byte.</returns>
        public static byte ToByteOrDefault( object @this, Func<byte> defaultValueFactory)
        {
            try
            {
                return Convert.ToByte(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a byte.</returns>
        public static byte ToByteOrDefault( object @this, Func<byte> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToByte(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToChar
        /// <summary>
        /// An object extension method that converts the @this to a character.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a char.</returns>
        public static char ToChar( object @this)
        {
            return Convert.ToChar(@this);
        }
        #endregion

        #region ToCharOrDefault
        /// <summary>
        /// An object extension method that converts this object to a character or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a char.</returns>
        public static char ToCharOrDefault( object @this)
        {
            try
            {
                return Convert.ToChar(@this);
            }
            catch (Exception)
            {
                return default(char);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a character or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a char.</returns>
        public static char ToCharOrDefault( object @this, char defaultValue)
        {
            try
            {
                return Convert.ToChar(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a character or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a char.</returns>
        public static char ToCharOrDefault( object @this, char defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToChar(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a character or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a char.</returns>
        public static char ToCharOrDefault( object @this, Func<char> defaultValueFactory)
        {
            try
            {
                return Convert.ToChar(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a character or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a char.</returns>
        public static char ToCharOrDefault( object @this, Func<char> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToChar(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

   

        #region ToDecimalOrDefault
        /// <summary>
        /// An object extension method that converts this object to a decimal or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a decimal.</returns>
        public static decimal ToDecimalOrDefault( object @this)
        {
            try
            {
                return Convert.ToDecimal(@this);
            }
            catch (Exception)
            {
                return default(decimal);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a decimal or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a decimal.</returns>
        public static decimal ToDecimalOrDefault( object @this, decimal defaultValue)
        {
            try
            {
                return Convert.ToDecimal(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a decimal or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a decimal.</returns>
        public static decimal ToDecimalOrDefault( object @this, decimal defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToDecimal(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a decimal or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a decimal.</returns>
        public static decimal ToDecimalOrDefault( object @this, Func<decimal> defaultValueFactory)
        {
            try
            {
                return Convert.ToDecimal(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a decimal or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a decimal.</returns>
        public static decimal ToDecimalOrDefault( object @this, Func<decimal> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToDecimal(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

      

        #region ToFloatOrDefault
        /// <summary>
        /// An object extension method that converts this object to a float or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToFloatOrDefault( object @this)
        {
            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return default(float);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a float or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToFloatOrDefault( object @this, float defaultValue)
        {
            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a float or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToFloatOrDefault( object @this, float defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a float or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToFloatOrDefault( object @this, Func<float> defaultValueFactory)
        {
            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a float or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToFloatOrDefault( object @this, Func<float> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToGuid
        /// <summary>
        /// An object extension method that converts the @this to a unique identifier.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a GUID.</returns>
        public static Guid ToGuid( object @this)
        {
            return new Guid(@this.ToString());
        }
        #endregion

        #region ToGuidOrDefault
        /// <summary>
        /// An object extension method that converts this object to a unique identifier or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a GUID.</returns>
        public static Guid ToGuidOrDefault( object @this)
        {
            try
            {
                return new Guid(@this.ToString());
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a unique identifier or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a GUID.</returns>
        public static Guid ToGuidOrDefault( object @this, Guid defaultValue)
        {
            try
            {
                return new Guid(@this.ToString());
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a unique identifier or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a GUID.</returns>
        public static Guid ToGuidOrDefault( object @this, Guid defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return new Guid(@this.ToString());
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a unique identifier or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a GUID.</returns>
        public static Guid ToGuidOrDefault( object @this, Func<Guid> defaultValueFactory)
        {
            try
            {
                return new Guid(@this.ToString());
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a unique identifier or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a GUID.</returns>
        public static Guid ToGuidOrDefault( object @this, Func<Guid> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return new Guid(@this.ToString());
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToInt16
        /// <summary>
        /// An object extension method that converts the @this to an int 16.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a short.</returns>
        public static short ToInt16( object @this)
        {
            return Convert.ToInt16(@this);
        }
        #endregion

        #region ToInt16OrDefault
        /// <summary>
        /// An object extension method that converts this object to an int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToInt16OrDefault( object @this)
        {
            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return default(short);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToInt16OrDefault( object @this, short defaultValue)
        {
            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToInt16OrDefault( object @this, short defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToInt16OrDefault( object @this, Func<short> defaultValueFactory)
        {
            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToInt16OrDefault( object @this, Func<short> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToInt32
        /// <summary>
        /// An object extension method that converts the @this to an int 32.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an int.</returns>
        public static int ToInt32( object @this)
        {
            return Convert.ToInt32(@this);
        }
        #endregion

        #region ToInt32OrDefault
        /// <summary>
        /// An object extension method that converts this object to an int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an int.</returns>
        public static int ToInt32OrDefault( object @this)
        {
            try
            {
                return Convert.ToInt32(@this);
            }
            catch (Exception)
            {
                return default(int);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an int.</returns>
        public static int ToInt32OrDefault( object @this, int defaultValue)
        {
            try
            {
                return Convert.ToInt32(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to an int.</returns>
        public static int ToInt32OrDefault( object @this, int defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToInt32(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an int.</returns>
        public static int ToInt32OrDefault( object @this, Func<int> defaultValueFactory)
        {
            try
            {
                return Convert.ToInt32(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to an int.</returns>
        public static int ToInt32OrDefault( object @this, Func<int> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToInt32(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

    

        #region ToInt64OrDefault
        /// <summary>
        /// An object extension method that converts this object to an int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToInt64OrDefault( object @this)
        {
            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return default(long);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToInt64OrDefault( object @this, long defaultValue)
        {
            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToInt64OrDefault( object @this, long defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToInt64OrDefault( object @this, Func<long> defaultValueFactory)
        {
            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToInt64OrDefault( object @this, Func<long> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToSByte
        /// <summary>
        /// An object extension method that converts the @this to the s byte.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a sbyte.</returns>
        public static sbyte ToSByte( object @this)
        {
            return Convert.ToSByte(@this);
        }
        #endregion

        #region ToSByteOrDefault
        /// <summary>
        /// An object extension method that converts this object to the s byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a sbyte.</returns>
        public static sbyte ToSByteOrDefault( object @this)
        {
            try
            {
                return Convert.ToSByte(@this);
            }
            catch (Exception)
            {
                return default(sbyte);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to the s byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a sbyte.</returns>
        public static sbyte ToSByteOrDefault( object @this, sbyte defaultValue)
        {
            try
            {
                return Convert.ToSByte(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to the s byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a sbyte.</returns>
        public static sbyte ToSByteOrDefault( object @this, sbyte defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToSByte(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to the s byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a sbyte.</returns>
        public static sbyte ToSByteOrDefault( object @this, Func<sbyte> defaultValueFactory)
        {
            try
            {
                return Convert.ToSByte(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to the s byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a sbyte.</returns>
        public static sbyte ToSByteOrDefault( object @this, Func<sbyte> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToSByte(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToShort
        /// <summary>
        /// An object extension method that converts the @this to a short.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a short.</returns>
        public static short ToShort( object @this)
        {
            return Convert.ToInt16(@this);
        }
        #endregion

        #region ToShortOrDefault
        /// <summary>
        /// An object extension method that converts this object to a short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToShortOrDefault( object @this)
        {
            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return default(short);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToShortOrDefault( object @this, short defaultValue)
        {
            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToShortOrDefault( object @this, short defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToShortOrDefault( object @this, Func<short> defaultValueFactory)
        {
            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToShortOrDefault( object @this, Func<short> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToSingle
        /// <summary>
        /// An object extension method that converts the @this to a single.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a float.</returns>
        public static float ToSingle( object @this)
        {
            return Convert.ToSingle(@this);
        }
        #endregion

        #region ToSingleOrDefault
        /// <summary>
        /// An object extension method that converts this object to a single or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToSingleOrDefault( object @this)
        {
            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return default(float);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a single or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToSingleOrDefault( object @this, float defaultValue)
        {
            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a single or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToSingleOrDefault( object @this, float defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a single or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToSingleOrDefault( object @this, Func<float> defaultValueFactory)
        {
            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a single or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToSingleOrDefault( object @this, Func<float> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToString
        /// <summary>
        /// An object extension method that convert this object into a string representation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string that represents this object.</returns>
        public static string ToString( object @this)
        {
            return Convert.ToString(@this);
        }
        #endregion

        #region ToStringOrDefault
        /// <summary>
        /// An object extension method that converts this object to a string or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToStringOrDefault( object @this)
        {
            try
            {
                return Convert.ToString(@this);
            }
            catch (Exception)
            {
                return default(string);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a string or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToStringOrDefault( object @this, string defaultValue)
        {
            try
            {
                return Convert.ToString(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a string or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToStringOrDefault( object @this, string defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToString(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a string or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToStringOrDefault( object @this, Func<string> defaultValueFactory)
        {
            try
            {
                return Convert.ToString(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a string or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToStringOrDefault( object @this, Func<string> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToString(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToUInt16
        /// <summary>
        /// An object extension method that converts the @this to an u int 16.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ushort.</returns>
        public static ushort ToUInt16( object @this)
        {
            return Convert.ToUInt16(@this);
        }
        #endregion

        #region ToUInt16OrDefault
        /// <summary>
        /// An object extension method that converts this object to an u int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUInt16OrDefault( object @this)
        {
            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return default(ushort);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUInt16OrDefault( object @this, ushort defaultValue)
        {
            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUInt16OrDefault( object @this, ushort defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUInt16OrDefault( object @this, Func<ushort> defaultValueFactory)
        {
            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUInt16OrDefault( object @this, Func<ushort> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToUInt32
        /// <summary>
        /// An object extension method that converts the @this to an u int 32.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an uint.</returns>
        public static uint ToUInt32( object @this)
        {
            return Convert.ToUInt32(@this);
        }
        #endregion

        #region ToUInt32OrDefault
        /// <summary>
        /// An object extension method that converts this object to an u int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an uint.</returns>
        public static uint ToUInt32OrDefault( object @this)
        {
            try
            {
                return Convert.ToUInt32(@this);
            }
            catch (Exception)
            {
                return default(uint);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an uint.</returns>
        public static uint ToUInt32OrDefault( object @this, uint defaultValue)
        {
            try
            {
                return Convert.ToUInt32(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to an uint.</returns>
        public static uint ToUInt32OrDefault( object @this, uint defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToUInt32(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an uint.</returns>
        public static uint ToUInt32OrDefault( object @this, Func<uint> defaultValueFactory)
        {
            try
            {
                return Convert.ToUInt32(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to an uint.</returns>
        public static uint ToUInt32OrDefault( object @this, Func<uint> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToUInt32(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToUInt64
        /// <summary>
        /// An object extension method that converts the @this to an u int 64.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ulong.</returns>
        public static ulong ToUInt64( object @this)
        {
            return Convert.ToUInt64(@this);
        }
        #endregion

        #region ToUInt64OrDefault
        /// <summary>
        /// An object extension method that converts this object to an u int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToUInt64OrDefault( object @this)
        {
            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return default(ulong);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToUInt64OrDefault( object @this, ulong defaultValue)
        {
            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToUInt64OrDefault( object @this, ulong defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToUInt64OrDefault( object @this, Func<ulong> defaultValueFactory)
        {
            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToUInt64OrDefault( object @this, Func<ulong> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToULong
        /// <summary>
        /// An object extension method that converts the @this to an u long.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ulong.</returns>
        public static ulong ToULong( object @this)
        {
            return Convert.ToUInt64(@this);
        }
        #endregion

        #region ToULongOrDefault
        /// <summary>
        /// An object extension method that converts this object to an u long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToULongOrDefault( object @this)
        {
            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return default(ulong);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToULongOrDefault( object @this, ulong defaultValue)
        {
            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToULongOrDefault( object @this, ulong defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToULongOrDefault( object @this, Func<ulong> defaultValueFactory)
        {
            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToULongOrDefault( object @this, Func<ulong> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToUShort
        /// <summary>
        /// An object extension method that converts the @this to an u short.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ushort.</returns>
        public static ushort ToUShort( object @this)
        {
            return Convert.ToUInt16(@this);
        }
        #endregion

        #region ToUShortOrDefault
        /// <summary>
        /// An object extension method that converts this object to an u short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUShortOrDefault( object @this)
        {
            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return default(ushort);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUShortOrDefault( object @this, ushort defaultValue)
        {
            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUShortOrDefault( object @this, ushort defaultValue, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUShortOrDefault( object @this, Func<ushort> defaultValueFactory)
        {
            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// An object extension method that converts this object to an u short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="useDefaultIfNull">true to use default if null.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUShortOrDefault( object @this, Func<ushort> defaultValueFactory, bool useDefaultIfNull)
        {
            if (useDefaultIfNull && @this == null)
            {
                return defaultValueFactory();
            }

            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableBoolean
        /// <summary>
        /// An object extension method that converts the @this to a nullable boolean.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a bool?</returns>
        public static bool? ToNullableBoolean( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToBoolean(@this);
        }
        #endregion

        #region ToNullableBooleanOrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable boolean or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a bool?</returns>
        public static bool? ToNullableBooleanOrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToBoolean(@this);
            }
            catch (Exception)
            {
                return default(bool);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable boolean or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a bool?</returns>
        public static bool? ToNullableBooleanOrDefault( object @this, bool? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToBoolean(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable boolean or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a bool?</returns>
        public static bool? ToNullableBooleanOrDefault( object @this, Func<bool?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToBoolean(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableByte
        /// <summary>
        /// An object extension method that converts the @this to a nullable byte.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a byte?</returns>
        public static byte? ToNullableByte( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToByte(@this);
        }
        #endregion

        #region ToNullableByteOrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a byte?</returns>
        public static byte? ToNullableByteOrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToByte(@this);
            }
            catch (Exception)
            {
                return default(byte);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a byte?</returns>
        public static byte? ToNullableByteOrDefault( object @this, byte? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToByte(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a byte?</returns>
        public static byte? ToNullableByteOrDefault( object @this, Func<byte?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToByte(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableChar
        /// <summary>
        /// An object extension method that converts the @this to a nullable character.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a char?</returns>
        public static char? ToNullableChar( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToChar(@this);
        }
        #endregion

        #region ToNullableCharOrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable character or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a char?</returns>
        public static char? ToNullableCharOrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToChar(@this);
            }
            catch (Exception)
            {
                return default(char);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable character or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a char?</returns>
        public static char? ToNullableCharOrDefault( object @this, char? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToChar(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable character or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a char?</returns>
        public static char? ToNullableCharOrDefault( object @this, Func<char?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToChar(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableDateTime
        /// <summary>
        /// An object extension method that converts the @this to a nullable date time.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a DateTime?</returns>
        public static DateTime? ToNullableDateTime( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDateTime(@this);
        }
        #endregion

        #region ToNullableDateTimeOrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable date time or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a DateTime?</returns>
        public static DateTime? ToNullableDateTimeOrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDateTime(@this);
            }
            catch (Exception)
            {
                return default(DateTime);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable date time or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a DateTime?</returns>
        public static DateTime? ToNullableDateTimeOrDefault( object @this, DateTime? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDateTime(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable date time or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a DateTime?</returns>
        public static DateTime? ToNullableDateTimeOrDefault( object @this, Func<DateTime?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDateTime(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableDateTimeOffSet
        /// <summary>
        /// An object extension method that converts the @this to a nullable date time off set.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a DateTimeOffset?</returns>
        public static DateTimeOffset? ToNullableDateTimeOffSet( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
        }
        #endregion

        #region ToNullableDateTimeOffSetOrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable date time off set or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a DateTimeOffset?</returns>
        public static DateTimeOffset? ToNullableDateTimeOffSetOrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
            }
            catch (Exception)
            {
                return default(DateTimeOffset);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable date time off set or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a DateTimeOffset?</returns>
        public static DateTimeOffset? ToNullableDateTimeOffSetOrDefault( object @this, DateTimeOffset? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable date time off set or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a DateTimeOffset?</returns>
        public static DateTimeOffset? ToNullableDateTimeOffSetOrDefault( object @this, Func<DateTimeOffset?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableDecimal
        /// <summary>
        /// An object extension method that converts the @this to a nullable decimal.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a decimal?</returns>
        public static decimal? ToNullableDecimal( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDecimal(@this);
        }
        #endregion

        #region ToNullableDecimalOrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable decimal or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a decimal?</returns>
        public static decimal? ToNullableDecimalOrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDecimal(@this);
            }
            catch (Exception)
            {
                return default(decimal);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable decimal or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a decimal?</returns>
        public static decimal? ToNullableDecimalOrDefault( object @this, decimal? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDecimal(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable decimal or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a decimal?</returns>
        public static decimal? ToNullableDecimalOrDefault( object @this, Func<decimal?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDecimal(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableDouble
        /// <summary>
        /// An object extension method that converts the @this to a nullable double.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a double?</returns>
        public static double? ToNullableDouble( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDouble(@this);
        }
        #endregion

        #region ToNullableDoubleOrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable double or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a double?</returns>
        public static double? ToNullableDoubleOrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDouble(@this);
            }
            catch (Exception)
            {
                return default(double);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable double or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a double?</returns>
        public static double? ToNullableDoubleOrDefault( object @this, double? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDouble(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable double or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a double?</returns>
        public static double? ToNullableDoubleOrDefault( object @this, Func<double?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDouble(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableFloat
        /// <summary>
        /// An object extension method that converts the @this to a nullable float.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a float?</returns>
        public static float? ToNullableFloat( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToSingle(@this);
        }
        #endregion

        #region ToNullableFloatOrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable float or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a float?</returns>
        public static float? ToNullableFloatOrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return default(float);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable float or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a float?</returns>
        public static float? ToNullableFloatOrDefault( object @this, float? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable float or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a float?</returns>
        public static float? ToNullableFloatOrDefault( object @this, Func<float?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableGuid
        /// <summary>
        /// An object extension method that converts the @this to a nullable unique identifier.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a Guid?</returns>
        public static Guid? ToNullableGuid( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return new Guid(@this.ToString());
        }
        #endregion

        #region ToNullableGuidOrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable unique identifier or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a Guid?</returns>
        public static Guid? ToNullableGuidOrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return new Guid(@this.ToString());
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable unique identifier or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a Guid?</returns>
        public static Guid? ToNullableGuidOrDefault( object @this, Guid? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return new Guid(@this.ToString());
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable unique identifier or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a Guid?</returns>
        public static Guid? ToNullableGuidOrDefault( object @this, Func<Guid?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return new Guid(@this.ToString());
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableInt16
        /// <summary>
        /// An object extension method that converts the @this to a nullable int 16.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a short?</returns>
        public static short? ToNullableInt16( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt16(@this);
        }
        #endregion

        #region ToNullableInt16OrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a short?</returns>
        public static short? ToNullableInt16OrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return default(short);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a short?</returns>
        public static short? ToNullableInt16OrDefault( object @this, short? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a short?</returns>
        public static short? ToNullableInt16OrDefault( object @this, Func<short?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableInt32
        /// <summary>
        /// An object extension method that converts the @this to a nullable int 32.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an int?</returns>
        public static int? ToNullableInt32( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt32(@this);
        }
        #endregion

        #region ToNullableInt32OrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an int?</returns>
        public static int? ToNullableInt32OrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt32(@this);
            }
            catch (Exception)
            {
                return default(int);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an int?</returns>
        public static int? ToNullableInt32OrDefault( object @this, int? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt32(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an int?</returns>
        public static int? ToNullableInt32OrDefault( object @this, Func<int?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt32(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableInt64
        /// <summary>
        /// An object extension method that converts the @this to a nullable int 64.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a long?</returns>
        public static long? ToNullableInt64( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt64(@this);
        }
        #endregion

        #region ToNullableInt64OrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a long?</returns>
        public static long? ToNullableInt64OrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return default(long);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a long?</returns>
        public static long? ToNullableInt64OrDefault( object @this, long? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a long?</returns>
        public static long? ToNullableInt64OrDefault( object @this, Func<long?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableLong
        /// <summary>
        /// An object extension method that converts the @this to a nullable long.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a long?</returns>
        public static long? ToNullableLong( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt64(@this);
        }
        #endregion

        #region ToNullableLongOrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a long?</returns>
        public static long? ToNullableLongOrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return default(long);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a long?</returns>
        public static long? ToNullableLongOrDefault( object @this, long? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a long?</returns>
        public static long? ToNullableLongOrDefault( object @this, Func<long?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableSByte
        /// <summary>
        /// An object extension method that converts the @this to a nullable s byte.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a sbyte?</returns>
        public static sbyte? ToNullableSByte( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToSByte(@this);
        }
        #endregion

        #region ToNullableSByteOrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable s byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a sbyte?</returns>
        public static sbyte? ToNullableSByteOrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSByte(@this);
            }
            catch (Exception)
            {
                return default(sbyte);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable s byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a sbyte?</returns>
        public static sbyte? ToNullableSByteOrDefault( object @this, sbyte? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSByte(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable s byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a sbyte?</returns>
        public static sbyte? ToNullableSByteOrDefault( object @this, Func<sbyte?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSByte(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableShort
        /// <summary>
        /// An object extension method that converts the @this to a nullable short.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a short?</returns>
        public static short? ToNullableShort( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt16(@this);
        }
        #endregion

        #region ToNullableShortOrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a short?</returns>
        public static short? ToNullableShortOrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return default(short);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a short?</returns>
        public static short? ToNullableShortOrDefault( object @this, short? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a short?</returns>
        public static short? ToNullableShortOrDefault( object @this, Func<short?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableSingle
        /// <summary>
        /// An object extension method that converts the @this to a nullable single.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a float?</returns>
        public static float? ToNullableSingle( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToSingle(@this);
        }
        #endregion

        #region ToNullableSingleOrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable single or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a float?</returns>
        public static float? ToNullableSingleOrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return default(float);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable single or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a float?</returns>
        public static float? ToNullableSingleOrDefault( object @this, float? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable single or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a float?</returns>
        public static float? ToNullableSingleOrDefault( object @this, Func<float?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableUInt16
        /// <summary>
        /// An object extension method that converts the @this to a nullable u int 16.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ushort?</returns>
        public static ushort? ToNullableUInt16( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt16(@this);
        }
        #endregion

        #region ToNullableUInt16OrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable u int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ushort?</returns>
        public static ushort? ToNullableUInt16OrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return default(ushort);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable u int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ushort?</returns>
        public static ushort? ToNullableUInt16OrDefault( object @this, ushort? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable u int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ushort?</returns>
        public static ushort? ToNullableUInt16OrDefault( object @this, Func<ushort?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableUInt32
        /// <summary>
        /// An object extension method that converts the @this to a nullable u int 32.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an uint?</returns>
        public static uint? ToNullableUInt32( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt32(@this);
        }
        #endregion

        #region ToNullableUInt32OrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable u int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an uint?</returns>
        public static uint? ToNullableUInt32OrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt32(@this);
            }
            catch (Exception)
            {
                return default(uint);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable u int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an uint?</returns>
        public static uint? ToNullableUInt32OrDefault( object @this, uint? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt32(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable u int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an uint?</returns>
        public static uint? ToNullableUInt32OrDefault( object @this, Func<uint?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt32(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableUInt64
        /// <summary>
        /// An object extension method that converts the @this to a nullable u int 64.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ulong?</returns>
        public static ulong? ToNullableUInt64( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt64(@this);
        }
        #endregion

        #region ToNullableUInt64OrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable u int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ulong?</returns>
        public static ulong? ToNullableUInt64OrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return default(ulong);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable u int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ulong?</returns>
        public static ulong? ToNullableUInt64OrDefault( object @this, ulong? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable u int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ulong?</returns>
        public static ulong? ToNullableUInt64OrDefault( object @this, Func<ulong?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableULong
        /// <summary>
        /// An object extension method that converts the @this to a nullable u long.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ulong?</returns>
        public static ulong? ToNullableULong( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt64(@this);
        }
        #endregion

        #region ToNullableULongOrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable u long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ulong?</returns>
        public static ulong? ToNullableULongOrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return default(ulong);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable u long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ulong?</returns>
        public static ulong? ToNullableULongOrDefault( object @this, ulong? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable u long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ulong?</returns>
        public static ulong? ToNullableULongOrDefault( object @this, Func<ulong?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion

        #region ToNullableUShort
        /// <summary>
        /// An object extension method that converts the @this to a nullable u short.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ushort?</returns>
        public static ushort? ToNullableUShort( object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt16(@this);
        }
        #endregion

        #region ToNullableUShortOrDefault
        /// <summary>
        /// An object extension method that converts this object to a nullable u short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ushort?</returns>
        public static ushort? ToNullableUShortOrDefault( object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return default(ushort);
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable u short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ushort?</returns>
        public static ushort? ToNullableUShortOrDefault( object @this, ushort? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// An object extension method that converts this object to a nullable u short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ushort?</returns>
        public static ushort? ToNullableUShortOrDefault( object @this, Func<ushort?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        #endregion
        #endregion

        #region IsValidValueType
        #region IsValidBoolean
        /// <summary>
        /// An object extension method that query if '@this' is valid bool.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid bool, false if not.</returns>
        public static bool IsValidBoolean( object @this)
        {
            if (@this == null)
                return false;
            bool result;
            return bool.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidByte
        /// <summary>
        /// An object extension method that query if '@this' is valid byte.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid byte, false if not.</returns>
        public static bool IsValidByte( object @this)
        {
            if (@this == null)
                return false;
            byte result;
            return byte.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidChar
        /// <summary>
        /// An object extension method that query if '@this' is valid char.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid char, false if not.</returns>
        public static bool IsValidChar( object @this)
        {
            if (@this == null)
                return false;
            char result;
            return char.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidDateTime
        /// <summary>
        /// An object extension method that query if '@this' is valid System.DateTime.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid System.DateTime, false if not.</returns>
        public static bool IsValidDateTime( object @this)
        {
            if (@this == null)
                return false;
            DateTime result;
            return DateTime.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidDateTimeOffSet
        /// <summary>
        /// An object extension method that query if '@this' is valid System.DateTimeOffset.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid System.DateTimeOffset, false if not.</returns>
        public static bool IsValidDateTimeOffSet( object @this)
        {
            if (@this == null)
                return false;
            DateTimeOffset result;
            return DateTimeOffset.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidDecimal
        /// <summary>
        /// An object extension method that query if '@this' is valid decimal.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid decimal, false if not.</returns>
        public static bool IsValidDecimal( object @this)
        {
            if (@this == null)
                return false;
            decimal result;
            return decimal.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidDouble
        /// <summary>
        /// An object extension method that query if '@this' is valid double.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid double, false if not.</returns>
        public static bool IsValidDouble( object @this)
        {
            if (@this == null)
                return false;
            double result;
            return double.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidFloat
        /// <summary>
        /// An object extension method that query if '@this' is valid float.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid float, false if not.</returns>
        public static bool IsValidFloat( object @this)
        {
            if (@this == null)
                return false;
            float result;
            return float.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidGuid
        /// <summary>
        /// An object extension method that query if '@this' is valid System.Guid.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid System.Guid, false if not.</returns>
        public static bool IsValidGuid( object @this)
        {
            if (@this == null)
                return false;
            Guid result;
            return Guid.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidInt16
        /// <summary>
        /// An object extension method that query if '@this' is valid short.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid short, false if not.</returns>
        public static bool IsValidInt16( object @this)
        {
            if (@this == null)
                return false;
            short result;
            return short.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidInt32
        /// <summary>
        /// An object extension method that query if '@this' is valid int.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid int, false if not.</returns>
        public static bool IsValidInt32( object @this)
        {
            if (@this == null)
                return false;
            int result;
            return int.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidInt64
        /// <summary>
        /// An object extension method that query if '@this' is valid long.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid long, false if not.</returns>
        public static bool IsValidInt64( object @this)
        {
            if (@this == null)
                return false;
            long result;
            return long.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidLong
        /// <summary>
        /// An object extension method that query if '@this' is valid long.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid long, false if not.</returns>
        public static bool IsValidLong( object @this)
        {
            if (@this == null)
                return false;
            long result;
            return long.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidSByte
        /// <summary>
        /// An object extension method that query if '@this' is valid sbyte.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid sbyte, false if not.</returns>
        public static bool IsValidSByte( object @this)
        {
            if (@this == null)
                return false;
            sbyte result;
            return sbyte.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidShort
        /// <summary>
        /// An object extension method that query if '@this' is valid short.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid short, false if not.</returns>
        public static bool IsValidShort( object @this)
        {
            if (@this == null)
                return false;
            short result;
            return short.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidSingle
        /// <summary>
        /// An object extension method that query if '@this' is valid float.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid float, false if not.</returns>
        public static bool IsValidSingle( object @this)
        {
            if (@this == null)
                return false;
            float result;
            return float.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidString
        /// <summary>
        /// An object extension method that query if '@this' is valid string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid string, false if not.</returns>
        public static bool IsValidString( object @this)
        {
            return true;
        }
        #endregion

        #region IsValidUInt16
        /// <summary>
        /// An object extension method that query if '@this' is valid ushort.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid ushort, false if not.</returns>
        public static bool IsValidUInt16( object @this)
        {
            if (@this == null)
                return false;
            ushort result;
            return ushort.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidUInt32
        /// <summary>
        /// An object extension method that query if '@this' is valid uint.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid uint, false if not.</returns>
        public static bool IsValidUInt32( object @this)
        {
            if (@this == null)
                return false;
            uint result;
            return uint.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidUInt64
        /// <summary>
        /// An object extension method that query if '@this' is valid ulong.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid ulong, false if not.</returns>
        public static bool IsValidUInt64( object @this)
        {
            if (@this == null)
                return false;
            ulong result;
            return ulong.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidULong
        /// <summary>
        /// An object extension method that query if '@this' is valid ulong.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid ulong, false if not.</returns>
        public static bool IsValidULong( object @this)
        {
            if (@this == null)
                return false;
            ulong result;
            return ulong.TryParse(@this.ToString(), out result);
        }
        #endregion

        #region IsValidUShort
        /// <summary>
        /// An object extension method that query if '@this' is valid ushort.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid ushort, false if not.</returns>
        public static bool IsValidUShort( object @this)
        {
            if (@this == null)
                return false;
            ushort result;
            return ushort.TryParse(@this.ToString(), out result);
        }
        #endregion
        #endregion

        #region IsAssignableFrom
        /// <summary>
        /// An object extension method that query if '@this' is assignable from.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if assignable from, false if not.</returns>
        public static bool IsAssignableFrom<T>( object @this)
        {
            Type type = @this.GetType();
            return type.IsAssignableFrom(typeof(T));
        }

        /// <summary>
        /// An object extension method that query if '@this' is assignable from.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns>true if assignable from, false if not.</returns>
        public static bool IsAssignableFrom( object @this, Type targetType)
        {
            Type type = @this.GetType();
            return type.IsAssignableFrom(targetType);
        }
        #endregion

        #region Chain
        /// <summary>
        /// A T extension method that chains actions.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="action">The action.</param>
        /// <returns>The @this acted on.</returns>
        public static T Chain<T>( T @this, Action<T> action)
        {
            action(@this);

            return @this;
        }
        #endregion

       
        /// <summary>
        /// Returns an object of the specified type whose value is equivalent to the specified object.
        /// </summary>
        /// <param name="this">An object that implements the  interface.</param>
        /// <param name="typeCode">The type of object to return.</param>
        /// <returns>
        /// An object whose underlying type is  and whose value is equivalent to .-or-A null reference (Nothing in Visual
        /// Basic), if  is null and  is , , or .
        /// </returns>
        public static object ChangeType( object @this, TypeCode typeCode)
        {
            return Convert.ChangeType(@this, typeCode);
        }

        /// <summary>
        /// Returns an object of the specified type whose value is equivalent to the specified object. A parameter
        /// supplies culture-specific formatting information.
        /// </summary>
        /// <param name="this">An object that implements the  interface.</param>
        /// <param name="typeCode">The type of object to return.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An object whose underlying type is  and whose value is equivalent to .-or- A null reference (Nothing in
        /// Visual Basic), if  is null and  is , , or .
        /// </returns>
        public static object ChangeType( object @this, TypeCode typeCode, IFormatProvider provider)
        {
            return Convert.ChangeType(@this, typeCode, provider);
        }

        /// <summary>
        /// Returns an object of the specified type whose value is equivalent to the specified object. A parameter
        /// supplies culture-specific formatting information.
        /// </summary>
        /// <param name="this">An object that implements the  interface.</param>
        /// <param name="conversionType">The type of object to return.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An object whose type is  and whose value is equivalent to .-or- , if the  of  and  are equal.-or- A null
        /// reference (Nothing in Visual Basic), if  is null and  is not a value type.
        /// </returns>
        public static object ChangeType( object @this, Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(@this, conversionType, provider);
        }

        /// <summary>
        /// Returns an object of the specified type whose value is equivalent to the specified object. A parameter
        /// supplies culture-specific formatting information.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">An object that implements the  interface.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An object whose type is  and whose value is equivalent to .-or- , if the  of  and  are equal.-or- A null
        /// reference (Nothing in Visual Basic), if  is null and  is not a value type.
        /// </returns>
        public static object ChangeType<T>( object @this, IFormatProvider provider)
        {
            return (T)Convert.ChangeType(@this, typeof(T), provider);
        }

       
       
        #region GetTypeCode
        /// <summary>
        /// Returns the  for the specified object.
        /// </summary>
        /// <param name="this">An object that implements the  interface.</param>
        /// <returns>The  for , or  if  is null.</returns>
        public static TypeCode GetTypeCode( object @this)
        {
            return Convert.GetTypeCode(@this);
        }
        #endregion

        #region Coalesce
        /// <summary>
        /// A T extension method that that return the first not null value (including the @this).
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>The first not null value.</returns>
        public static T Coalesce<T>( T @this, params T[] values) where T : class
        {
            if (@this != null)
            {
                return @this;
            }

            foreach (T value in values)
            {
                if (value != null)
                {
                    return value;
                }
            }

            return null;
        }
        #endregion

        #region CoalesceOrDefault
        /// <summary>
        /// A T extension method that that return the first not null value (including the @this) or a default value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>The first not null value or a default value.</returns>
        public static T CoalesceOrDefault<T>( T @this, params T[] values) where T : class
        {
            if (@this != null)
            {
                return @this;
            }

            foreach (T value in values)
            {
                if (value != null)
                {
                    return value;
                }
            }

            return default(T);
        }

        /// <summary>
        /// A T extension method that that return the first not null value (including the @this) or a default value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>The first not null value or a default value.</returns>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_CoalesceOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void CoalesceOrDefault()
        ///               {
        ///                   // Varable
        ///                   object nullObject = null;
        /// 
        ///                   // Type
        ///                   object @thisNull = null;
        ///                   object @thisNotNull = &quot;Fizz&quot;;
        /// 
        ///                   // Exemples
        ///                   object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
        ///                   object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result3 = @thisNull.CoalesceOrDefault((x) =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result2);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result3);
        ///                   Assert.AreEqual(&quot;Fizz&quot;, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_CoalesceOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void CoalesceOrDefault()
        ///               {
        ///                   // Varable
        ///                   object nullObject = null;
        /// 
        ///                   // Type
        ///                   object @thisNull = null;
        ///                   object @thisNotNull = &quot;Fizz&quot;;
        /// 
        ///                   // Exemples
        ///                   object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
        ///                   object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result3 = @thisNull.CoalesceOrDefault(x =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result2);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result3);
        ///                   Assert.AreEqual(&quot;Fizz&quot;, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///           using Z.ExtensionMethods.Object;
        ///           
        ///           namespace ExtensionMethods.Examples
        ///           {
        ///               [TestClass]
        ///               public class System_Object_CoalesceOrDefault
        ///               {
        ///                   [TestMethod]
        ///                   public void CoalesceOrDefault()
        ///                   {
        ///                       // Varable
        ///                       object nullObject = null;
        ///           
        ///                       // Type
        ///                       object @thisNull = null;
        ///                       object @thisNotNull = &quot;Fizz&quot;;
        ///           
        ///                       // Exemples
        ///                       object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
        ///                       object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                       object result3 = @thisNull.CoalesceOrDefault(x =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                       object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
        ///           
        ///                       // Unit Test
        ///                       Assert.AreEqual(&quot;Buzz&quot;, result1);
        ///                       Assert.AreEqual(&quot;Buzz&quot;, result2);
        ///                       Assert.AreEqual(&quot;Buzz&quot;, result3);
        ///                       Assert.AreEqual(&quot;Fizz&quot;, result4);
        ///                   }
        ///               }
        ///           }
        ///     </code>
        /// </example>
        public static T CoalesceOrDefault<T>( T @this, Func<T> defaultValueFactory, params T[] values) where T : class
        {
            if (@this != null)
            {
                return @this;
            }

            foreach (T value in values)
            {
                if (value != null)
                {
                    return value;
                }
            }

            return defaultValueFactory();
        }

        /// <summary>
        /// A T extension method that that return the first not null value (including the @this) or a default value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>The first not null value or a default value.</returns>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_CoalesceOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void CoalesceOrDefault()
        ///               {
        ///                   // Varable
        ///                   object nullObject = null;
        /// 
        ///                   // Type
        ///                   object @thisNull = null;
        ///                   object @thisNotNull = &quot;Fizz&quot;;
        /// 
        ///                   // Exemples
        ///                   object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
        ///                   object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result3 = @thisNull.CoalesceOrDefault((x) =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result2);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result3);
        ///                   Assert.AreEqual(&quot;Fizz&quot;, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_CoalesceOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void CoalesceOrDefault()
        ///               {
        ///                   // Varable
        ///                   object nullObject = null;
        /// 
        ///                   // Type
        ///                   object @thisNull = null;
        ///                   object @thisNotNull = &quot;Fizz&quot;;
        /// 
        ///                   // Exemples
        ///                   object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
        ///                   object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result3 = @thisNull.CoalesceOrDefault(x =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result2);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result3);
        ///                   Assert.AreEqual(&quot;Fizz&quot;, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///           using Z.ExtensionMethods.Object;
        ///           
        ///           namespace ExtensionMethods.Examples
        ///           {
        ///               [TestClass]
        ///               public class System_Object_CoalesceOrDefault
        ///               {
        ///                   [TestMethod]
        ///                   public void CoalesceOrDefault()
        ///                   {
        ///                       // Varable
        ///                       object nullObject = null;
        ///           
        ///                       // Type
        ///                       object @thisNull = null;
        ///                       object @thisNotNull = &quot;Fizz&quot;;
        ///           
        ///                       // Exemples
        ///                       object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
        ///                       object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                       object result3 = @thisNull.CoalesceOrDefault(x =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                       object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
        ///           
        ///                       // Unit Test
        ///                       Assert.AreEqual(&quot;Buzz&quot;, result1);
        ///                       Assert.AreEqual(&quot;Buzz&quot;, result2);
        ///                       Assert.AreEqual(&quot;Buzz&quot;, result3);
        ///                       Assert.AreEqual(&quot;Fizz&quot;, result4);
        ///                   }
        ///               }
        ///           }
        ///     </code>
        /// </example>
        public static T CoalesceOrDefault<T>( T @this, Func<T, T> defaultValueFactory, params T[] values) where T : class
        {
            if (@this != null)
            {
                return @this;
            }

            foreach (T value in values)
            {
                if (value != null)
                {
                    return value;
                }
            }

            return defaultValueFactory(@this);
        }
        #endregion

        #region GetValueOrDefault
        /// <summary>
        /// A T extension method that gets value or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="func">The function.</param>
        /// <returns>The value or default.</returns>
        public static TResult GetValueOrDefault<T, TResult>( T @this, Func<T, TResult> func)
        {
            try
            {
                return func(@this);
            }
            catch (Exception)
            {
                return default(TResult);
            }
        }

        /// <summary>
        /// A T extension method that gets value or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="func">The function.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value or default.</returns>
        public static TResult GetValueOrDefault<T, TResult>( T @this, Func<T, TResult> func, TResult defaultValue)
        {
            try
            {
                return func(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// A T extension method that gets value or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="func">The function.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The value or default.</returns>
        /// <example>
        ///     <code>
        ///       using System.Xml;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_GetValueOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void GetValueOrDefault()
        ///               {
        ///                   // Type
        ///                   var @this = new XmlDocument();
        /// 
        ///                   // Exemples
        ///                   string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
        ///                   string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System.Xml;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_GetValueOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void GetValueOrDefault()
        ///               {
        ///                   // Type
        ///                   var @this = new XmlDocument();
        /// 
        ///                   // Exemples
        ///                   string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
        ///                   string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///           using System.Xml;
        ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///           using Z.ExtensionMethods.Object;
        ///           
        ///           namespace ExtensionMethods.Examples
        ///           {
        ///               [TestClass]
        ///               public class System_Object_GetValueOrDefault
        ///               {
        ///                   [TestMethod]
        ///                   public void GetValueOrDefault()
        ///                   {
        ///                       // Type
        ///                       var @this = new XmlDocument();
        ///           
        ///                       // Exemples
        ///                       string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
        ///                       string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
        ///           
        ///                       // Unit Test
        ///                       Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
        ///                       Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
        ///                   }
        ///               }
        ///           }
        ///     </code>
        /// </example>
        public static TResult GetValueOrDefault<T, TResult>( T @this, Func<T, TResult> func, Func<TResult> defaultValueFactory)
        {
            try
            {
                return func(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// A T extension method that gets value or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="func">The function.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The value or default.</returns>
        /// <example>
        ///     <code>
        ///       using System.Xml;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_GetValueOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void GetValueOrDefault()
        ///               {
        ///                   // Type
        ///                   var @this = new XmlDocument();
        /// 
        ///                   // Exemples
        ///                   string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
        ///                   string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System.Xml;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_GetValueOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void GetValueOrDefault()
        ///               {
        ///                   // Type
        ///                   var @this = new XmlDocument();
        /// 
        ///                   // Exemples
        ///                   string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
        ///                   string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///           using System.Xml;
        ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///           using Z.ExtensionMethods.Object;
        ///           
        ///           namespace ExtensionMethods.Examples
        ///           {
        ///               [TestClass]
        ///               public class System_Object_GetValueOrDefault
        ///               {
        ///                   [TestMethod]
        ///                   public void GetValueOrDefault()
        ///                   {
        ///                       // Type
        ///                       var @this = new XmlDocument();
        ///           
        ///                       // Exemples
        ///                       string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
        ///                       string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
        ///           
        ///                       // Unit Test
        ///                       Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
        ///                       Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
        ///                   }
        ///               }
        ///           }
        ///     </code>
        /// </example>
        public static TResult GetValueOrDefault<T, TResult>( T @this, Func<T, TResult> func, Func<T, TResult> defaultValueFactory)
        {
            try
            {
                return func(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory(@this);
            }
        }
        #endregion

        #region IfNotNull
        /// <summary>
        /// A T extension method that execute an action when the value is not null.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="action">The action.</param>
        public static void IfNotNull<T>( T @this, Action<T> action) where T : class
        {
            if (@this != null)
            {
                action(@this);
            }
        }

        /// <summary>
        /// A T extension method that the function result if not null otherwise default value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="func">The function.</param>
        /// <returns>The function result if @this is not null otherwise default value.</returns>
        public static TResult IfNotNull<T, TResult>( T @this, Func<T, TResult> func) where T : class
        {
            return @this != null ? func(@this) : default(TResult);
        }

        /// <summary>
        /// A T extension method that the function result if not null otherwise default value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="func">The function.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The function result if @this is not null otherwise default value.</returns>
        public static TResult IfNotNull<T, TResult>( T @this, Func<T, TResult> func, TResult defaultValue) where T : class
        {
            return @this != null ? func(@this) : defaultValue;
        }

        /// <summary>
        /// A T extension method that the function result if not null otherwise default value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="func">The function.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The function result if @this is not null otherwise default value.</returns>
        public static TResult IfNotNull<T, TResult>( T @this, Func<T, TResult> func, Func<TResult> defaultValueFactory) where T : class
        {
            return @this != null ? func(@this) : defaultValueFactory();
        }
        #endregion

        #region NullIf
        /// <summary>
        /// A T extension method that null if.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A T.</returns>
        public static T NullIf<T>( T @this, Func<T, bool> predicate) where T : class
        {
            if (predicate(@this))
            {
                return null;
            }
            return @this;
        }
        #endregion

        #region NullIfEquals
        /// <summary>
        /// A T extension method that null if equals.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="value">The value.</param>
        /// <returns>A T.</returns>
        public static T NullIfEquals<T>( T @this, T value) where T : class
        {
            if (@this.Equals(value))
            {
                return null;
            }
            return @this;
        }
        #endregion

        #region NullIfEqualsAny
        /// <summary>
        /// A T extension method that null if equals any.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>A T.</returns>
        public static T NullIfEqualsAny<T>( T @this, params T[] values) where T : class
        {
            if (Array.IndexOf(values, @this) != -1)
            {
                return null;
            }
            return @this;
        }
        #endregion

        #region ToStringSafe
        /// <summary>
        /// An object extension method that converts the @this to string or return an empty string if the value is null.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a string or empty if the value is null.</returns>
        public static string ToStringSafe( object @this)
        {
            return @this == null ? "" : @this.ToString();
        }
        #endregion

        #region Try
        /// <summary>
        /// A TType extension method that tries.
        /// </summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryFunction">The try function.</param>
        /// <returns>A TResult.</returns>
        public static TResult Try<TType, TResult>( TType @this, Func<TType, TResult> tryFunction)
        {
            try
            {
                return tryFunction(@this);
            }
            catch
            {
                return default(TResult);
            }
        }

        /// <summary>
        /// A TType extension method that tries.
        /// </summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryFunction">The try function.</param>
        /// <param name="catchValue">The catch value.</param>
        /// <returns>A TResult.</returns>
        public static TResult Try<TType, TResult>( TType @this, Func<TType, TResult> tryFunction, TResult catchValue)
        {
            try
            {
                return tryFunction(@this);
            }
            catch
            {
                return catchValue;
            }
        }

        /// <summary>
        /// A TType extension method that tries.
        /// </summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryFunction">The try function.</param>
        /// <param name="catchValueFactory">The catch value factory.</param>
        /// <returns>A TResult.</returns>
        public static TResult Try<TType, TResult>( TType @this, Func<TType, TResult> tryFunction, Func<TType, TResult> catchValueFactory)
        {
            try
            {
                return tryFunction(@this);
            }
            catch
            {
                return catchValueFactory(@this);
            }
        }

        /// <summary>
        /// A TType extension method that tries.
        /// </summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryFunction">The try function.</param>
        /// <param name="result">[out] The result.</param>
        /// <returns>A TResult.</returns>
        public static bool Try<TType, TResult>( TType @this, Func<TType, TResult> tryFunction, out TResult result)
        {
            try
            {
                result = tryFunction(@this);
                return true;
            }
            catch
            {
                result = default(TResult);
                return false;
            }
        }

        /// <summary>
        /// A TType extension method that tries.
        /// </summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryFunction">The try function.</param>
        /// <param name="catchValue">The catch value.</param>
        /// <param name="result">[out] The result.</param>
        /// <returns>A TResult.</returns>
        public static bool Try<TType, TResult>( TType @this, Func<TType, TResult> tryFunction, TResult catchValue, out TResult result)
        {
            try
            {
                result = tryFunction(@this);
                return true;
            }
            catch
            {
                result = catchValue;
                return false;
            }
        }

        /// <summary>
        /// A TType extension method that tries.
        /// </summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryFunction">The try function.</param>
        /// <param name="catchValueFactory">The catch value factory.</param>
        /// <param name="result">[out] The result.</param>
        /// <returns>A TResult.</returns>
        public static bool Try<TType, TResult>( TType @this, Func<TType, TResult> tryFunction, Func<TType, TResult> catchValueFactory, out TResult result)
        {
            try
            {
                result = tryFunction(@this);
                return true;
            }
            catch
            {
                result = catchValueFactory(@this);
                return false;
            }
        }

        /// <summary>
        /// A TType extension method that attempts to action from the given data.
        /// </summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryAction">The try action.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool Try<TType>( TType @this, Action<TType> tryAction)
        {
            try
            {
                tryAction(@this);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// A TType extension method that attempts to action from the given data.
        /// </summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryAction">The try action.</param>
        /// <param name="catchAction">The catch action.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool Try<TType>( TType @this, Action<TType> tryAction, Action<TType> catchAction)
        {
            try
            {
                tryAction(@this);
                return true;
            }
            catch
            {
                catchAction(@this);
                return false;
            }
        }
        #endregion

        #region Between
        /// <summary>
        /// A T extension method that check if the value is between (exclusif) the minValue and maxValue.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
        public static bool Between<T>( T @this, T minValue, T maxValue) where T : IComparable<T>
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }
        #endregion

        #region In
        /// <summary>
        /// A T extension method to determines whether the object is equal to any of the provided values.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list contains the object, else false.</returns>
        public static bool In<T>( T @this, params T[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }
        #endregion

        #region InRange
        /// <summary>
        /// A T extension method that check if the value is between inclusively the minValue and maxValue.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
        public static bool InRange<T>( T @this, T minValue, T maxValue) where T : IComparable<T>
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }
        #endregion

        #region IsDBNull
        /// <summary>
        /// Returns an indication whether the specified object is of type .
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">An object.</param>
        /// <returns>true if  is of type ; otherwise, false.</returns>
        public static bool IsDBNull<T>( T @this) where T : class
        {
            return Convert.IsDBNull(@this);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// A T extension method that query if 'source' is the default value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The source to act on.</param>
        /// <returns>true if default, false if not.</returns>
        public static bool IsDefault<T>( T @this)
        {
            return @this.Equals(default(T));
        }
        #endregion

        #region IsNotNull
        /// <summary>
        /// A T extension method that query if '@this' is not null.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if not null, false if not.</returns>
        public static bool IsNotNull<T>( T @this) where T : class
        {
            return @this != null;
        }
        #endregion

        #region IsNull
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="this">object对象</param>
        /// <returns>bool</returns>
        public static bool IsNull( object @this)
        {
            return @this == null || @this == DBNull.Value;
        }

        /// <summary>
        /// 查询'@this'是否为null的T扩展方法。
        /// </summary>
        /// <typeparam name="T">泛型类型参数。</typeparam>
        /// <param name="this">对 @this 起作用。</param>
        /// <returns>如果为null则为true，否则为false。</returns>
        public static bool IsNullT<T>( T @this) where T : class, new()
        {
            return @this == null;
        }
        #endregion

        #region NotIn
        /// <summary>
        /// A T extension method to determines whether the object is not equal to any of the provided values.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list doesn't contains the object, else false.</returns>
        public static bool NotIn<T>( T @this, params T[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
        #endregion

        #region ReferenceEquals
        /// <summary>
        /// Determines whether the specified  instances are the same instance.
        /// </summary>
        /// <param name="this">The first object to compare.</param>
        /// <param name="obj">The second object  to compare.</param>
        /// <returns>true if  is the same instance as  or if both are null; otherwise, false.</returns>
        public new static bool ReferenceEquals( object @this, object obj)
        {
            return object.ReferenceEquals(@this, obj);
        }
        #endregion
      
    }

}