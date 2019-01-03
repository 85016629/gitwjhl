using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace fx.Infra.Utility
{
    /// <summary>
    /// ID构造器。
    /// 可以使用此类构造一个有意义的，可以简单识别的唯一Id
    /// </summary>
    public class IDConstructor
    {
        public static string CreateOuterTranId()
        {
            var strCurrentTime = DateTime.Now.ToString("yyyyMMddHHMMss");
            //生成四位随机数
            var random = new Random(int.Parse(strCurrentTime.Substring(strCurrentTime.Length - 4, 4)));
            var randomNum = random.Next(9999).ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');
            var strGuid = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();
            return "O" + strCurrentTime + randomNum + strGuid;
        }


        public static string CreateNewSn()
        {
            var strCurrentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            //生成四位随机数
            var random = new Random(int.Parse(strCurrentTime.Substring(strCurrentTime.Length - 4, 4)));
            var randomNum = random.Next(9999).ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');
            var strGuid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToLower();
            return strCurrentTime + randomNum + strGuid;
        }

        /// <summary>
        /// 生成新的交易ID。生成规则：YYYYMMDDHHMMSS+交易类型+四位随机数
        /// </summary>
        /// <param name="tranType"></param>
        /// <returns></returns>
        public static string CreateNewTranId(string tranType)
        {
            var strCurrentTime = DateTime.Now.ToString("yyyyMMddHHMMss");
            //生成四位随机数
            var random = new Random(int.Parse(strCurrentTime.Substring(strCurrentTime.Length - 4, 4)));
            var randomNum = random.Next(9999).ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');
            var strGuid = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();
            return strCurrentTime + tranType + randomNum + strGuid;
        }
    }
}
