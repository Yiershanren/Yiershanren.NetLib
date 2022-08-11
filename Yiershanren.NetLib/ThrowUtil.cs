using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Junior.Utility.Utils
{
    public enum ExceptionType : int
    {
        [Description("Exception")]
        Exception = 0,
        [Description("ArgumentNullException")]
        ArgumentNullException = 1,

        [Description("ArgumentException")]
        ArgumentException = 2,

        [Description("InvalidOperationException")]
        InvalidOperationException = 3,
        [Description("ArgumentOutOfRangeException")]
        ArgumentOutOfRangeException = 4,
        [Description("AccessViolationException")]
        AccessViolationException = 5,
        
        [Description("KeyNotFoundException")]
        KeyNotFoundException = 7,
        [Description("IndexOutOfRangeException")]
        IndexOutOfRangeException = 8,

        [Description("InvalidCastException")]
        InvalidCastException = 9,
        
        [Description("InvalidProgramException")]
        InvalidProgramException = 11,

        [Description("IOException")]
        IOException = 12,

        [Description("NotImplementedException")]
        NotImplementedException = 13,

        [Description("NullReferenceException")]
        NullReferenceException = 14,

        [Description("OutOfMemoryException")]
        OutOfMemoryException = 15,

        [Description("StackOverflowException")]
        StackOverflowException = 16,
        
        [Description("DivideByZeroException")]
        DivideByZeroException = 19,
        
    }
    //public enum NULLType : int
    //{
    //    [Description("Null")]
    //    Null = 0,
    //    [Description("Empty")]
    //    Empty = 1,
    //    [Description("DBNull")]
    //    DBNull =2,
    //    [Description("NullOrEmpty")]
    //    NullOrEmpty = 3,
    //    [Description("NullOrEmptyOrDBNull")]
    //    NullOrEmptyOrDBNull = 4
    //}
    /// <summary>
    /// 抛出异常 工具类
    /// </summary>
    public static class ThrowUtil
    {

        #region ThrowIfNull
        /// <summary>
        /// 对象 null   判断
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument"></param>
        public static void ThrowIfNull<T>(T argument) where T : class
        {
            if (argument == null)
                throw new Exception(nameof(argument));
        }

        /// <summary>
        ///     对象 null   判断
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void ThrowIfNull<T>(T argument, string name) where T : class
        {
            if (argument == null)
                throw new Exception(name);
           if (object.Equals(null, argument))
                throw new Exception(name);
        }
        /// <summary>
        ///     对象 null   判断
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void ThrowIfNull<T>(T? argument, string name) where T : struct
        {
            if (!argument.HasValue)
                throw new Exception(name);
        }
        /// <summary>
        /// 对象 null   判断
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void ThrowIfNull<T>(ExceptionType et, T argument, string name) where T : class
        {
            if (argument == null)
            {
                ThrowNew(et, name, "",null);
                throw new Exception(name);
            }
        }

        /// <summary>
        ///     字符串对象 null,empty   判断
        ///     ArgumentNullException
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void ThrowIfNull(string argument, string name)
        {
            if (string.IsNullOrEmpty(argument))
                throw new Exception(name);
        }
        /// <summary>
        /// 字符串对象 null 判断
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void ThrowIfNull2(string argument, string name)
        {
                if (argument == null)
                    throw new Exception(name);
        }

        /// <summary>
        ///     时间对象 MinValue  判断
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void ThrowIfNull(DateTime argument, string name)
        {
            if (argument == DateTime.MinValue)
                throw new Exception(name);
        }
        #endregion

        #region Throw

        /// <summary>
        /// 抛出参数异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void Throw(string message)
        {
            throw new Exception(message);
        }

        /// <summary>
        /// 抛出异常
        /// </summary>
        /// <param name="et"></param>
        /// <param name="message"></param>
        /// <param name="paramName"></param>
        public static void Throw(ExceptionType et, string message, string paramName)
        {
            ThrowNew(et, message, paramName,null);

            throw new Exception(message);
        }

        private static void ThrowNew(ExceptionType et, string message, string paramName, Exception innerException)
        {
            innerException = innerException ?? new Exception();


            //在试图读写受保护内存时引发的异常。
            if (et == ExceptionType.AccessViolationException)
                throw new AccessViolationException(message, innerException);

            //在向方法提供的其中一个参数无效时引发的异常。
            if (et == ExceptionType.ArgumentException)
                throw new ArgumentException(message, paramName);

            //指定用于访问集合中元素的键与集合中的任何键都不匹配时所引发的异常。
            if (et == ExceptionType.KeyNotFoundException)
                throw new System.Collections.Generic.KeyNotFoundException(message, innerException);

            //访问数组时，因元素索引超出数组边界而引发的异常。
            if (et == ExceptionType.IndexOutOfRangeException)
                throw new System.IndexOutOfRangeException(message, innerException);

            //因无效类型转换或显示转换引发的异常。
            if (et == ExceptionType.InvalidCastException)
                throw new System.InvalidCastException(message, innerException);

            //当方法调用对于对象的当前状态无效时引发的异常。
            if (et == ExceptionType.InvalidOperationException)
                throw new InvalidOperationException(message, innerException);

            //当程序包含无效Microsoft中间语言（MSIL）或元数据时引发的异常，这通常表示生成程序的编译器中有bug。
            if (et == ExceptionType.InvalidProgramException)
                throw new InvalidProgramException(message, innerException);


            //发生I/O错误时引发的异常。
            if (et == ExceptionType.IOException)
                throw new System.IO.IOException(message, innerException);

            //在无法实现请求的方法或操作时引发的异常。
            if (et == ExceptionType.NotImplementedException)
                throw new NotImplementedException(message, innerException);

            //尝试对空对象引用进行操作时引发的异常。
            if (et == ExceptionType.NullReferenceException)
                throw new NullReferenceException(message, innerException);

            //没有足够的内存继续执行程序时引发的异常。
            if (et == ExceptionType.OutOfMemoryException)
                throw new OutOfMemoryException(message, innerException);

            //挂起的方法调用过多而导致执行堆栈溢出时引发的异常。
            if (et == ExceptionType.StackOverflowException)
                throw new StackOverflowException(message, innerException);

            // 当将空引用传递给不接受它作为有效参数的方法时引发的异常。
            if (et == ExceptionType.ArgumentNullException)
                throw new ArgumentNullException(paramName, message);

            //当参数值超出调用的方法所定义的允许取值范围时引发的异常。
            if (et == ExceptionType.ArgumentOutOfRangeException)
                throw new ArgumentOutOfRangeException(paramName, message);

            //试图用零除整数值或十进制数值时引发的异常。
            if (et == ExceptionType.DivideByZeroException)
                throw new DivideByZeroException(message, innerException);

            //当参数值超出调用的方法所定义的允许取值范围时引发的异常。
            if (et == ExceptionType.ArgumentOutOfRangeException)
                throw new ArgumentOutOfRangeException(paramName, message);
        }

        #endregion

        #region 逻辑判断

        /// <summary>
        ///     逻辑对象 false   判断
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void ThrowIfFalse(bool argument, string name)
        {
            if (!argument)
                throw new Exception(name);
        }

        /// <summary>
        ///     逻辑对象 true  判断
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void ThrowIfTrue(bool argument, string name)
        {
            if (argument)
                throw new Exception(name);
        }
        /// <summary>
        ///  逻辑对象 true  判断
        /// </summary>
        /// <param name="et"></param>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void ThrowIfTrue(ExceptionType et, bool argument, string name)
        {
            if (argument)
            {
                ThrowNew(et, name, "",null);
                throw new Exception(name);
            }
        }

        #endregion

        #region 数值判断

        /// <summary>
        ///    int 小于0   判断
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void ThrowIfNegative(int argument, string name)
        {
            if (argument < 0)
                throw new ArgumentOutOfRangeException(name);
        }
        /// <summary>
        ///     小于0   判断
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void ThrowIfNegative(long argument, string name)
        {
            if (argument < 0L)
                throw new ArgumentOutOfRangeException(name);
        }
        /// <summary>
        ///     小于特定数值   判断
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void ThrowIfNegative(long argument,long Neg, string name)
        {
            if (argument < Neg)
                throw new ArgumentOutOfRangeException(name);
        }
        /// <summary>
        ///     小于某个值   判断
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void ThrowIfNegative(int argument,int Neg, string name)
        {
            if (argument < Neg)
                throw new ArgumentOutOfRangeException(name);
        }

        /// <summary>
        ///     小于等于0   判断
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void ThrowIfNonPositive(int argument, string name)
        {
            if (argument <= 0)
                throw new ArgumentOutOfRangeException(name);
        }

        /// <summary>
        ///     小于等于0   判断
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public static void ThrowIfNonPositive(long argument, string name)
        {
            if (argument <= 0L)
                throw new ArgumentOutOfRangeException(name);
        }
        #endregion

        #region 越界判断
        /// <summary>
        ///     超界    判断
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="lowerBound">下界(不包含)</param>
        /// <param name="upperBound">上界(不包含)</param>
        /// <param name="name"></param>
        public static void ThrowIfOutOfRange(int argument, int lowerBound, int upperBound, string name)
        {
            if (argument < lowerBound || argument > upperBound)
                throw new ArgumentOutOfRangeException(name, string.Format("x<{0} or x>{1}", lowerBound, upperBound));
        }

        /// <summary>
        ///     超界    判断
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="boundArray">需要匹配的枚举</param>
        /// <param name="name"></param>
        public static void ThrowIfOutOfRange(int argument, int[] boundArray, string name)
        {
            if (!boundArray.Any(x => x == argument))
                throw new ArgumentOutOfRangeException(name,
                    string.Format("[{0}]", string.Join("],[", Array.ConvertAll(boundArray, x => x.ToString()))));
        }

        /// <summary>
        ///     超界    判断
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="boundArray">需要匹配的枚举</param>
        /// <param name="name"></param>
        public static void ThrowIfOutOfRange(string argument, string[] boundArray, string name)
        {
            if (!boundArray.Any(x => x == argument))
                throw new ArgumentOutOfRangeException(name, string.Format("[{0}]", string.Join("],[", boundArray)));
        }
        #endregion

    }
}
