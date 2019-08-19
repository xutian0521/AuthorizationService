
using AuthService.Core.Model.Authorize;
using AuthService.Core.Utility;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace AuthService.Core.Utility
{
    /// <summary>
    /// 授权相关方法
    /// </summary>
    public class HDTAuthorizeHelper
    {


  
         public int[] intCode = new int[127];    //存储密钥
         public char[] charCode = new char[25];  //存储ASCII码
         public int[] intNumber = new int[25];   //存储ASCII码值
  
         //初始化密钥
         public void SetIntCode()
         {
             for (int i = 1; i<intCode.Length; i++)
             {
                 intCode[i] = i % 9;
             }
         }
  
         ///<summary>
         /// 根据序列号生成注册码
         ///</summary>
         ///<returns></returns>
         public string GetRNum(string strMNum)
         {
             SetIntCode();
             //string strMNum = GetMNum();
             for (int i = 1; i<charCode.Length; i++)   //存储机器码
             {
                 charCode[i] = Convert.ToChar(strMNum.Substring(i - 1, 1));
             }
             for (int j = 1; j<intNumber.Length; j++)  //改变ASCII码值
             {
                 intNumber[j] = Convert.ToInt32(charCode[j]) + intCode[Convert.ToInt32(charCode[j])];
             }
             string strAsciiName = "";   //注册码
             for (int k = 1; k<intNumber.Length; k++)  //生成注册码
             {
  
                 if ((intNumber[k] >= 48 && intNumber[k] <= 57) || (intNumber[k] >= 65 && intNumber[k]
                     <= 90) || (intNumber[k] >= 97 && intNumber[k] <= 122))  //判断如果在0-9、A-Z、a-z之间
                 {
                     strAsciiName += Convert.ToChar(intNumber[k]).ToString();
                 }
                 else if (intNumber[k] > 122)  //判断如果大于z
                 {
                     strAsciiName += Convert.ToChar(intNumber[k] - 10).ToString();
                 }
                 else
                 {
                     strAsciiName += Convert.ToChar(intNumber[k] - 9).ToString();
                 }
             }
             return strAsciiName;
         }





        public void GetClientMaxConnectNum()
        {
            RegistryKey retkey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true).CreateSubKey("YiuHDT_Platform").CreateSubKey("MaxConnectNum");
            var ciphertext = retkey.GetValue("Num").ToString();
            EncryptUtil.Base64Decrypt(ciphertext);
        }

        /// <summary>
        /// 获取注册码尾部 加密的 注册次数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int GetTailFlagToInt(string num)
        {
            var _num=  this.SplitStringDecryptToInt(num, 24, 4);
            return _num;
        }

        /// <summary>
        /// 获取注册码尾部 加密的 最大连接数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int GetTailConnNumToInt(string num)
        {
            var _num = this.SplitStringDecryptToInt(num, 28, 4);
            return _num;
        }

        /// <summary>
        /// 根据 指定截取的字符串，解密为数字
        /// </summary>
        /// <param name="num"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private int SplitStringDecryptToInt(string num,int startIndex,int length)
        {
            int _time = 0;
            if (num.Length > 24)
            {
                var _temp = num.Substring(startIndex, length);
                var decrypt = EncryptUtil.Base64Decrypt(_temp);

                int.TryParse(decrypt, out _time);
            }
            else
            {
                _time = 1;
            }
            return _time;
        }
    }

}
