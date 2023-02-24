using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SystemUnderTest
{
    /* TitanPay 是鈦坦科技的支付, Pchome和鈦坦簽約後，鈦坦提供Pchome一組MerchantCode與MerchantKey
     *
     * PChome在對TitanPay的Server API發出請求時，要將Signature填入
     *
     * Md5("{MerchantCode}{Amount}{MerchantKey}")
     *
     * 其中Amount每三個零要包含一個逗點, e.g. 1000 => 1,000
     *
     * md5: https://www.md5hashgenerator.com/
     */
    // 這個是 Interfaces  
    public interface IMerchantKeyReader
    {
        string Get();
    }

    // 這個 TxtMerchantKeyReader 的 class Implements IMerchantKeyReader 的 Interfaces
    public class TxtMerchantKeyReader : IMerchantKeyReader
    {
        public string Get()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                @"Data\key.txt");
            var merchantKey = File.ReadLines(path).First();
            return merchantKey;
        }
    }

    public interface IGetDate
    {
        DateTime GetDate();
    }

    public class GetDate : IGetDate
    {
        DateTime IGetDate.GetDate()
        {
            return DateTime.Now;
        }
    }

    public class TitanPayRequest
    {
        private readonly IMerchantKeyReader _txtMerchantKeyReader;
        private readonly IGetDate _txtGetDate;
        

        public TitanPayRequest(IMerchantKeyReader txtMerchantKeyReader, IGetDate getDate)
        {
            _txtMerchantKeyReader = txtMerchantKeyReader;
            _txtGetDate = getDate;
        }

        private string MerchantCode => "pchome";
        public int Amount { get; set; }
        public string Signature { get; private set; }
        public DateTime CreatedOn { get; private set; }

        // 這個是一個方法 （method or function）
        
        public void Sign()
        {
            const string merchantKey = "asdf1234";
            var beforeHash = $"{MerchantCode}{Amount:n0}{merchantKey}";

            Signature = new Md5Helper().Hash(beforeHash);
        }

        
        public void Sign2()
        {
            var merchantKey = _txtMerchantKeyReader.Get();
            var beforeHash = $"{MerchantCode}{Amount:n0}{merchantKey}";

            Signature = new Md5Helper().Hash(beforeHash);
        }


        public void Sign3()
        {
            CreatedOn = _txtGetDate.GetDate();

            const string merchantKey = "asdf1234";
            var beforeHash = $"{MerchantCode}{Amount:n0}{merchantKey}{CreatedOn:yyyy-MM-ddTHH:mm:ss}";

            Signature = new Md5Helper().Hash(beforeHash);
        }
    }
}