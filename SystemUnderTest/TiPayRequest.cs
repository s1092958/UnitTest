using System;

namespace SystemUnderTest
{
    public class TiPayRequest
    {
        public string MerchantCode { get; set; }
        public int Amount { get; set; }
        public string Signature { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}