namespace ShopManagement.Application.Contracts.Order
{
    public class PaymentMethod
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        private PaymentMethod(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public static List<PaymentMethod> GetList()
        {
            return new List<PaymentMethod>
            {
                new PaymentMethod(1 , "پرداخت اینترنتی" ,
                    "در مدل شما به درگاه پرداخت اینترنتی هدایت شده و درلحظه پرداخت وجه را انجام خواهید داد."),
                new PaymentMethod(2, "پرداخت نقدی",
                    "در این مدل، ابتدا سفارش ثبت شده و سپس وجه به صورت نقدی در زمان تحویل کالا، دریافت خواهد شد."),
                new PaymentMethod(3, "پرداخت از کیف پول",
                    "در این مدل، هزینه از کیف پول شخصی شما حساب میشود.")
            };
        }

        public PaymentMethod GetBy(int id)
        {
            return GetList().First(x => x.Id == id);
        }
    }
}
