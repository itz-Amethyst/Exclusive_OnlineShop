namespace _01_ExclusiveQuery.Contracts.Order
{
    public interface IOrderQuery
    {
        public List<CartQueryModel> GetCartItemsBy(List<CartQueryModel> cartItems);
    }
}
