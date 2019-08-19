using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Core.Utility
{
    public static class EncryptUtil
    {
        #region MD5加密

        /// <summary>
        /// MD5加密
        /// </summary>
        public static string Md532(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("未将对象引用设置到对象的实例。");
            }

            var encoding = Encoding.UTF8;
            MD5 md5 = MD5.Create();
            return HashAlgorithmBase(md5, value, encoding);
        }

        /// <summary>
        /// 加权MD5加密
        /// </summary>
        public static string Md532(this string value, string salt)
        {
            return salt == null ? value.Md532() : (value + "『" + salt + "』").Md532();
        }

        #endregion

        #region Base64加密解密 （增加自己算法变种）
        /// <summary>
        /// BASE64 加密（变种）
        /// </summary>
        /// <param name="value">待加密字段</param>
        /// <returns></returns>
        public static string Base64Variant(this string value)
        {
            var btArray = Encoding.UTF8.GetBytes(value);

            var base64Code = Convert.ToBase64String(btArray, 0, btArray.Length);

            var reverse = SBReverse(base64Code);
            var convertCode = ReplaceEncryptStr(reverse);
            return convertCode;
        }

        /// <summary>
        /// BASE64 解密（变种）
        /// </summary>
        /// <param name="value">待解密字段</param>
        /// <returns></returns>
        public static string UnBase64Variant(this string value)
        {
            var decryptStr = string.Empty;
            var convertCode = ReplaceEncryptStr(value);
            var reverse = SBReverse(convertCode);
            try
            {
                var btArray = Convert.FromBase64String(reverse);
                decryptStr = Encoding.UTF8.GetString(btArray);
            }
            catch (Exception)
            {
                //base64 转换为普通字符串失败，传入字符串不合法
                decryptStr = null;
            }

            return decryptStr;
        }
        /// <summary>
        /// 替换字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceEncryptStr(string str)
        {
            var convertCode = string.Empty;
            foreach (var item in str)
            {
                bool isNum = false;
                string _nunStr = string.Empty;
                if (isNum)//当前字符是否是数字 追加转换后的 数字到convertCode
                {
                    switch (item)
                    {
                        case '5':
                            _nunStr = "8";
                            break;
                        case '8':
                            _nunStr = "5";
                            break;
                        case '1':
                            _nunStr = "7";
                            break;
                        case '7':
                            _nunStr = "1";
                            break;
                        default:
                            _nunStr = item.ToString();//不是数字追加 当前字符到convertCode
                            break;
                    }
                    convertCode += _nunStr;
                }
                else
                {
                    string _code = string.Empty;
                    switch (item)
                    {
                        case 'B':
                            _code = "S";
                            break;
                        case 'S':
                            _code = "B";
                            break;
                        case 'Q':
                            _code = "Z";
                            break;
                        case 'Z':
                            _code = "Q";
                            break;
                        default:
                            _code = item.ToString();//不是数字追加 当前字符到convertCode
                            break;
                    }
                    convertCode += _code;
                }
            }
            return convertCode;
        }
        #endregion
        #region BASE64 加密解密

        /// <summary>
        /// BASE64 加密
        /// </summary>
        /// <param name="value">待加密字段</param>
        /// <returns></returns>
        public static string Base64(this string value)
        {
            var btArray = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(btArray, 0, btArray.Length);
        }

        /// <summary>
        /// BASE64 解密
        /// </summary>
        /// <param name="value">待解密字段</param>
        /// <returns></returns>
        public static string UnBase64(this string value)
        {
            var btArray = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(btArray);
        }

        #endregion

        #region Base64加密解密
        /// <summary>
        /// Base64加密 可逆
        /// </summary>
        /// <param name="value">待加密文本</param>
        /// <returns></returns>
        public static string Base64Encrypt(string value)
        {
            return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(value));
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="ciphervalue">密文</param>
        /// <returns></returns>
        public static string Base64Decrypt(string ciphervalue)
        {
            return System.Text.Encoding.Default.GetString(System.Convert.FromBase64String(ciphervalue));
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 翻转字符串
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string SBReverse(string text)
        {
            StringBuilder builder = new StringBuilder(text.Length);
            for (int i = text.Length - 1; i >= 0; i--)
            {
                builder.Append(text[i]);
            }

            return builder.ToString();
        }
        /// <summary>
        /// 转成数组
        /// </summary>
        private static byte[] Str2Bytes(this string source)
        {
            source = source.Replace(" ", "");
            byte[] buffer = new byte[source.Length / 2];
            for (int i = 0; i < source.Length; i += 2) buffer[i / 2] = Convert.ToByte(source.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary>
        /// 转换成字符串
        /// </summary>
        private static string Bytes2Str(this IEnumerable<byte> source, string formatStr = "{0:X2}")
        {
            StringBuilder pwd = new StringBuilder();
            foreach (byte btStr in source) { pwd.AppendFormat(formatStr, btStr); }
            return pwd.ToString();
        }

        private static byte[] FormatByte(this string strVal, Encoding encoding)
        {
            return encoding.GetBytes(strVal.Base64().Substring(0, 16).ToUpper());
        }

        /// <summary>
        /// HashAlgorithm 加密统一方法
        /// </summary>
        private static string HashAlgorithmBase(HashAlgorithm hashAlgorithmObj, string source, Encoding encoding)
        {
            byte[] btStr = encoding.GetBytes(source);
            byte[] hashStr = hashAlgorithmObj.ComputeHash(btStr);
            return hashStr.Bytes2Str();
        }

        #endregion

    }
}
