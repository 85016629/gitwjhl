namespace fx.Domain.Payment.CommandStatck
{
    using fx.Domain.core;
    using System;
    public class DealResult : AggregateRoot<Guid>
    {
        public ResultFlag ResultFlag { get; set; }
        public string ResultMsg { get; set; }
        public int OprId { get; set; }
        public string OprName { get; set; }
        public DateTime OprTime { get; set; }
    }


    public enum ResultFlag : byte
    {
        /// <summary>
        /// 同意
        /// </summary>
        Agree = 0,
        Refurse = 1
    }
}
