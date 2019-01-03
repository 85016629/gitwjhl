using fx.Domain.core;

namespace fx.Domain.Payment.CommandStatck
{
    public class BaseWallet : AggregateRoot<int>
    {
        public string OwnerName { get; set; }
        public string WalletNo { get; set; }
    }
}
