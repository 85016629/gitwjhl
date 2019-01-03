namespace fx.Domain.Payment.CommandStatck
{
    using fx.Domain.core;
    /// <summary>
    /// 银行，这里集成值对象。
    /// </summary>
    public class Bank : ValueObject<Bank>
    {
        public string BankCode { get; set; }
        public string BankName { get; set; }

        protected override bool EqualsCore(Bank other)
        {
            throw new System.NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new System.NotImplementedException();
        }
    }
}