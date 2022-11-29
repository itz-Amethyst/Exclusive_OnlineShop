using System.Globalization;
using _0_Framework.Application;
using _0_Framework.Application.Cookie;
using _0_Framework.Application.ZarinPal;
using _01_ExclusiveQuery.Contracts.Order;
using _01_ExclusiveQuery.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        private readonly ISerializeCookie _serializeCookie;
        private readonly IOrderQuery _orderQuery;
        private readonly IProductQuery _productQuery;
        private readonly IOrderApplication _orderApplication;
        private readonly IZarinPalFactory _zarinPalFactory;

        [TempData] public bool EmptyBasket { get; set; }

        public string OrderDescription { get; set; }

        public List<CookieCartModel> CartItems;

        public CartModelWithSummary TotalCartSummaryModel;


        public const string CookieName = "cart-items";


        public CheckoutModel(ISerializeCookie serializeCookie, IOrderQuery orderQuery, IProductQuery productQuery, IOrderApplication orderApplication, IZarinPalFactory zarinPalFactory)
        {
            _serializeCookie = serializeCookie;
            _orderQuery = orderQuery;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _zarinPalFactory = zarinPalFactory;
        }

        public void OnGet()
        {
            if (Request.Cookies["cart-items"] != null)
            {
                if (!_serializeCookie.CheckSerialize(CartItems, HttpContext))
                {
                    //?Just in Case
                    _serializeCookie.DeleteCookie(HttpContext);
                    return;
                }

                CartItems = _serializeCookie.Serialize(CartItems, HttpContext);

                if (CartItems.Count <= 0)
                {
                    EmptyBasket = true;
                    _serializeCookie.DeleteCookie(HttpContext);
                    return;
                }

                EmptyBasket = false;


                CartItems = _orderQuery.GetCartItemsBy(CartItems);

                TotalCartSummaryModel = _orderQuery.GetSummary(CartItems);

            }
        }

        public IActionResult OnPostPay(int paymentMethod)
        {
            if (Request.Cookies["cart-items"] != null)
            {
                if (!_serializeCookie.CheckSerialize(CartItems, HttpContext))
                {
                    //?Just in Case
                    _serializeCookie.DeleteCookie(HttpContext);
                    return RedirectToPage("/Index");
                }

                CartItems = _serializeCookie.Serialize(CartItems, HttpContext);

                if (CartItems.Count <= 0)
                {
                    EmptyBasket = true;
                    _serializeCookie.DeleteCookie(HttpContext);
                    return RedirectToPage("/Index");
                }

                EmptyBasket = false;


                CartItems = _orderQuery.GetCartItemsBy(CartItems);

                TotalCartSummaryModel = _orderQuery.GetSummary(CartItems);

                TotalCartSummaryModel.SetPaymentMethod(paymentMethod);

                var result = _productQuery.CheckInventoryStatus(TotalCartSummaryModel.Items); //CartItems Mishe

                if (result.Any(x => !x.IsInStock))
                {
                    return RedirectToPage("/Cart");
                }

                var orderId = _orderApplication.PlaceOrder(TotalCartSummaryModel);

                if (paymentMethod == 1 || paymentMethod == 3)
                {
                    OrderDescription = $"پرداخت فاکتور :‌ {orderId}";
                }
                else
                {
                    OrderDescription = $"فاکتور حضوری : {orderId}‌";
                }

                var paymentResponse = _zarinPalFactory.CreatePaymentRequest(
                    TotalCartSummaryModel.TotalPayAmount.ToString(CultureInfo.InvariantCulture), TotalCartSummaryModel.UserNameForZarinPal,
                    TotalCartSummaryModel.EmailForZarinPal, OrderDescription, orderId);

                var paymentResult = new PaymentResult();

                if (paymentMethod == 1)
                {
                    return Redirect(
                        $"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
                }
                else if (paymentMethod == 2)
                {
                    var orderAmount = _orderApplication.GetAmountBy(orderId);

                    var verificationResponse = _zarinPalFactory.CreateVerificationRequest(paymentResponse.Authority, orderAmount.ToString(CultureInfo.InvariantCulture));
                    var issueTrackingNo = _orderApplication.PaymentSucceeded(orderId, verificationResponse.RefID);
                    return RedirectToPage("/PaymentResult", paymentResult.Succeeded("سفارش شما با موفقیت ثبت شد و پس از تماس همکاران و درب منزل پرداخت خواهد شد", issueTrackingNo));
                }

                paymentResult = paymentResult.Failed("مشکلی پیش امده لطفا بعد از چند دقیقه دوباره تلاش کنید .");
                _serializeCookie.DeleteCookie(HttpContext);
                return RedirectToPage("/PaymentResult", paymentResult);

            }

            return RedirectToPage("/Index");
        }

        public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status, [FromQuery] int oId)
        {
            var orderAmount = _orderApplication.GetAmountBy(oId);

            var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, orderAmount.ToString(CultureInfo.InvariantCulture));

            var result = new PaymentResult();

            if (status == "OK" && verificationResponse.Status >= 100)
            {
                var issueTrackingNo = _orderApplication.PaymentSucceeded(oId, verificationResponse.RefID);
                _serializeCookie.DeleteCookie(HttpContext);

                result = result.Succeeded("پرداخت با موفقیت انجام شد", issueTrackingNo);

                return RedirectToPage("/PaymentResult", result);
            }
            else
            {
                result = result.Failed("پرداخت به مشکل خورد در صورت کسر وجه از حساب مبلغ تا 24 ساعت دیگه به حساب شما بازگردانده خواهد شد");
                return RedirectToPage("/PaymentResult", result);
            }
        }

        //public IActionResult OnPostUseDiscount(int orderId, string couponCode)
        //{
        //if (Request.Cookies["cart-items"] != null)
        //{
        //    if (!_serializeCookie.CheckSerialize(CartItems, HttpContext))
        //    {
        //        //?Just in Case
        //        _serializeCookie.DeleteCookie(HttpContext);
        //        return RedirectToPage("/Index");
        //    }

        //    CartItems = _serializeCookie.Serialize(CartItems, HttpContext);

        //    if (CartItems.Count <= 0)
        //    {
        //        EmptyBasket = true;
        //        _serializeCookie.DeleteCookie(HttpContext);
        //        return RedirectToPage("/Index");
        //    }

        //    EmptyBasket = false;


        //    CartItems = _orderQuery.GetCartItemsBy(CartItems);

        //    TotalCartSummaryModel = _orderQuery.GetSummary(CartItems);

        //    DiscountUseType type = _orderQuery.ApplyCouponDiscount(CartItems, couponCode);

        //    if (type == DiscountUseType.ExpireDate)
        //    {

        //    }
        //    else if (type == DiscountUseType.Finished)
        //    {

        //    }
        //    else if (type == DiscountUseType.UserUsed)
        //    {

        //    }
        //    else if (type == DiscountUseType.Success)
        //    {

        //    }
        //    else if (type == DiscountUseType.NotFound)
        //    {

        //    }
        //}

        //? This worked but it refreshed the page so all things goes wrong 
    }
}
