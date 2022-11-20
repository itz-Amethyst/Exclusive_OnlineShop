namespace _0_Framework.Application.Cookie;

public class CartModelWithSummary
{
    //? summary
    public double TotalAmount { get; set; }
    public double TotalDiscountAmount { get; set; }
    public double TotalPayAmount { get; set; }
    public int PaymentMethod { get; set; }
    public List<CookieCartModel> Items { get; set; }

    public string UserNameForZarinPal { get; set; }

    public string EmailForZarinPal { get; set; }

    public CartModelWithSummary()
    {
        Items = new List<CookieCartModel>();
    }

    public void Add(CookieCartModel cartItem)
    {
        Items.Add(cartItem);
        TotalAmount += cartItem.TotalItemPrice;
        TotalDiscountAmount += cartItem.DiscountAmount;
        TotalPayAmount += cartItem.ItemPayAmount;
    }

    public void SetPaymentMethod(int methodId)
    {
        PaymentMethod = methodId;
    }
}