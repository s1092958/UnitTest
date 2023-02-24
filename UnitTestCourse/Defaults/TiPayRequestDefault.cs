using System;
using SystemUnderTest;

namespace UnitTestCourse.Defaults
{
    public static class TiPayRequestDefault
    {
        public static TiPayRequest DefaultTiPayRequest(Action<TiPayRequest> overrides = null)
        {
            var payRequest = new TiPayRequest()
            {
                CreatedOn = DateTime.Now,
                MerchantCode = "Wee",
                Signature = "",
                Amount = 1000
            };
            overrides?.Invoke(payRequest);
            return payRequest;
        }
    }
}