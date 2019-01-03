namespace fx.Domain.Payment.CommandStatck
{
    using fx.Domain.core;
    using System;
    public class BankCard : AggregateRoot<Guid>
    {
        public string CardNo { get; set; }
        public string CardName { get; set; }
        public Bank Bank
        {
            get
            {
                return null;
                //if (string.IsNullOrEmpty(BankJsonString))
                //    return null;
                //return JsonConvert.DeserializeObject<Bank>(BankJsonString);
            }
        }
        public string BankJsonString { get; set; }
        /// <summary>
        /// 状态。
        /// </summary>
        public BankCardStatus Status { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCardNo { get; set; }
        /// <summary>
        /// 银行预留手机号。
        /// </summary>
        public string Mobile { get; set; }
    }

    public enum BankCardStatus : byte
    {
        Normal = 0,
        Removded = 1,
        Temporary = 9
    }
}
