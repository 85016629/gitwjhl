namespace fx.Domain.Payment.CommandStatck
{
    using fx.Domain.core;
    using System;
    using System.Threading.Tasks;
    public class BankCardCommandHandler : 
        ICommandHandler<PutBankCardIntoWalletCommand>
    {
        public Task HandleAsync(PutBankCardIntoWalletCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
