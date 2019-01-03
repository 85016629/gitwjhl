using fx.Domain.core;
using System;
using System.Collections.Generic;

namespace fx.Domain.Payment.CommandStatck
{
    public interface IBankCardRepository : IRepository<BankCard, Guid>
    {
        ICollection<BankCard> QueryBankCards(int pageIndex, int pageSize);
    }
}
