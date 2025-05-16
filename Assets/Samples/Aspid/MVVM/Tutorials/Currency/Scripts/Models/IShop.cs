namespace Aspid.MVVM.Currency.Models
{
    public interface IShop
    {
        public void Open();

        public void Open(ShopCategory category);
    }
}