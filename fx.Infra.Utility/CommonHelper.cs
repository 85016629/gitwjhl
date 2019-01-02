using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace fx.Infra.Utility
{
    public class CommonHelper
    {
        /// <summary>
        /// 屏蔽手机号中间4位，13111111111 转换后位131****1111
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static string MaskMobile(string mobile)
        {
            if (mobile.Length != 11)
                throw new ArgumentException("手机号号码位数不对.");

            mobile = Regex.Replace(mobile, "(\\d{3})\\d{4}(\\d{4})", "$1****$2");
            return mobile;
        }

        /// <summary>
        /// 屏蔽名字的后面几个字，只保留第一个字
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string MaskUserName(string userName)
        {
            var iLen = userName.Length;
            var maskUserName = userName.Substring(0, 1);

            maskUserName = maskUserName.PadRight(iLen, '*');
            return maskUserName;
        }
    }
}
