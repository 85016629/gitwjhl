using fx.Domain.core;
using System;

namespace fx.Domain.Payment.CommandStatck
{
    public class TransactionFlow : AggregateRoot<string>
    {
        //public new Guid UUId { get; set; }
        /// <summary>
        /// 原支付交易流水号
        /// </summary>
        public string OriTransactionFlowId { get; set; }
        /// <summary>
        /// 原支付交易流水单
        /// </summary>
        public virtual TransactionFlow OriTransactionFlow { get; set; }
        public string TranId { get; set; }
        public string PayerWalletNo { get; set; }
        /// <summary>
        /// 付方
        /// </summary>
        public string PayerName { get; set; }
        /// <summary>
        /// 收方
        /// </summary>
        public string ReceiverWalletNo { get; set; }
        public string ReceiverName { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? DealTime { get; set; }
        /// <summary>
        /// 交易金额.
        /// </summary>
        public decimal TransAmount { get; set; }
        /// <summary>
        /// 本次交易手续费
        /// </summary>
        public decimal FeeAmt { get; set; }
        public string ResultMsg { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public PaymentMode PaymentMode { get; set; }
        public string Channel { get; set; }
        public TransStatus Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Rem { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public string TranType { get; set; }
        public void Deal(string dealResult, TransStatus status)
        {
            Status = status;
            DealTime = DateTime.Now;
            ResultMsg = dealResult;
        }
    }

    /// <summary>
    /// 支付方式。
    /// </summary>
    public enum PaymentMode : byte
    {
        /// <summary>
        /// 银行卡。
        /// </summary>
        BankCard = 0,
        /// <summary>
        /// 支付宝。
        /// </summary>
        Alipay = 1,
        /// <summary>
        /// 微信
        /// </summary>
        WechatPay = 2,
        /// <summary>
        /// 余额
        /// </summary>
        Balance = 3
    }

    /// <summary>
    /// 交易状态
    /// </summary>
    public  enum TransStatus : byte
    {
        /// <summary>
        /// 处理中
        /// </summary>
        Process = 0,
        /// <summary>
        /// 处理成功。
        /// </summary>
        Success = 1, 
        /// <summary>
        /// 处理失败
        /// </summary>
        Fail = 2
    }
}
