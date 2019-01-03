namespace fx.Domain.Payment.CommandStatck
{
    using fx.Domain.core;
    using System;
    /// <summary>
    /// 表示一个退款订单。
    /// </summary>
    public class RefundOrder : AggregateRoot<Guid>
    {
        public decimal RefundAmount { get; set; }
        public string TransactionFlowId { get; set; }
        public TransactionFlow TransactionFlow { get; set; }
    }
}
