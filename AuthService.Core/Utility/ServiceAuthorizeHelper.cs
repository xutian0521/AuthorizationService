using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AuthService.Core.Utility
{
    /// <summary>
    /// 系统平台授权 帮助类
    /// </summary>
    public class ServiceAuthorizeHelper
    {



        /// <summary>
        /// 检查平台识别码(机器码)是否合法
        /// </summary>
        /// <param name="mchineCode">机器码</param>
        /// <returns></returns>
        public bool MachineCodeIsValidate(string mchineCode)
        {
            bool IsValidate = false;
            if (mchineCode.Length == 12)
            {
                IsValidate = true;
            }
            return IsValidate;
        }


        public DateTime? GetAuthorizedDeadline(string authorizeCode)
        {
            DateTime? Deadline = null;
            //通过授权码 解码 到机器码
            //通过$分割 机器码和时间。
            var unCode = authorizeCode.UnBase64Variant();
            try
            {
                var onlyCode = unCode.Split('$')[0];//机器码
                var onlyTime = unCode.Split('$')[1];//注册时间
                var onlyTimeSpan = unCode.Split('$')[2];//授权时间长度(间隔)
                                                        //对比 机器码是否相等，时间是否超时。
                Deadline = DateTime.Parse(onlyTime).Add(TimeSpan.Parse(onlyTimeSpan));
            }
            catch (Exception)
            {
                // 分割 解密之后的字符串失败，授权码不合法
                Deadline = null;
            }
            return Deadline;
        }

        ///<summary>
        /// 根据序列号(平台唯一识别码)生成注册码（授权码）
        ///</summary>
        ///<returns></returns>
        public string GetAuthorizeCode(string MCode, TimeSpan span)
        {
            var now = "$" + GetServiceTime().ToString("yyyy-MM-dd") + "$" + span.ToString();
            string strAsciiName = EncryptUtil.Base64Variant(MCode + now);   //注册码

            return strAsciiName;
        }

        public DateTime GetServiceTime()
        {
            //todo: 从时间服务器获取时间
            return DateTime.Now;
        }
    }
}
