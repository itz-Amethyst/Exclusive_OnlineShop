﻿using _0_Framework.Application.Cookie;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        int PlaceOrder(CartModelWithSummary cartModelWithSummary , HttpContext httpContext);

        string PaymentSucceeded(int orderId , int refId);

        double GetAmountBy(int id);
    }
}