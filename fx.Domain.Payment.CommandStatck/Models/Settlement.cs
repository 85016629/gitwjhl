namespace fx.Domain.Payment.CommandStatck
{
    using fx.Domain.core;
    using System;
    public class Settlement : AggregateRoot<Guid>
    {
        /// <summary>
        /// 结算Id。
        /// </summary>
        public string SettlementId { get; set; }
    }
}
