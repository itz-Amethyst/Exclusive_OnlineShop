namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public class ColleagueDiscountViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int DiscountRate { get; set; }

        public string CreationDate { get; set; }
    }
}
