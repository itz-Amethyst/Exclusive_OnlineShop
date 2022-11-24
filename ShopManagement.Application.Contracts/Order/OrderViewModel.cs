namespace ShopManagement.Application.Contracts.Order;

public class OrderViewModel
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string AccountName { get; set; }

    public int PaymentMethodId { get; set; }

    public string PaymentMethod { get; set; }

    public double TotalAmount { get; set; }

    public double DiscountAmount { get; set; }

    public double PayAmount { get; set; }

    public bool IsPaid { get; set; }

    public bool IsCanceled { get; set; }

    //? Just need to add this fucking question to make it null and shut the mouth of sql
    public string? IssueTrackingNo { get; set; }

    public int RefId { get; set; }

    public string CreationDate { get; set; }
}