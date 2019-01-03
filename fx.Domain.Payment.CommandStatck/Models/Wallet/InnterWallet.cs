using System.Collections.Generic;

namespace fx.Domain.Payment.CommandStatck
{
    public class InnterWallet : BaseWallet
    {
        public decimal ActualBalance { get; set; }
        public decimal FreezeBalance { get; set; }
        public decimal AvaiableBalance { get; set; }

        /// <summary>
        /// 放入银行卡
        /// </summary>
        /// <param name="bankCard"></param>
        public void PutInBankCard(BankCard bankCard)
        {

        }
        /// <summary>
        /// 移除指定的银行卡
        /// </summary>
        /// <param name="bankCard"></param>
        public void RemoveBankCard(BankCard bankCard)
        {

        }
        public ICollection<BankCard> BankCards { get; set; }
    }
}
