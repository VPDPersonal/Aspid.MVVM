using System;

namespace Aspid.MVVM.Currency.Models
{
    public sealed class DebugShop : IShop
    {
        private readonly Wallet _wallet;

        public DebugShop(Wallet wallet)
        {
            _wallet = wallet;
        }

        public void Open() =>
            Open(ShopCategory.None);
    
        public void Open(ShopCategory category)
        {
            switch (category)
            {
                case ShopCategory.None: 
                    PutSoft();
                    PutHard();
                    break;
                
                case ShopCategory.Soft: 
                    PutSoft();
                    break;
                
                case ShopCategory.Hard: 
                    PutHard(); 
                    break;
                
                default: throw new ArgumentException("...");
            }
        }
        
        private void PutSoft() =>
            _wallet.PutSoft((int)(_wallet.Soft / 100f * 10));
        
        private void PutHard() =>
            _wallet.PutHard((int)(_wallet.Hard / 100f * 10));
    }
}