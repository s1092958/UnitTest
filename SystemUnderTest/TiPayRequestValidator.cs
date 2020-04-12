using System;

namespace SystemUnderTest
{
    public class TiPayRequestValidator
    {
        private readonly IMerchantRepository _merchantRepository;

        public TiPayRequestValidator(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public void Validate(TiPayRequest request)
        {
            var merchantKey = _merchantRepository.GetMerchantKey(request.MerchantCode);
            var beforeHash = $"{request.MerchantCode}{request.Amount}{merchantKey}";

            var hash = new Md5Helper().Hash(beforeHash);

            if (hash != request.Signature)
            {
                throw new Exception("Signature mismatch");
            }
        }
    }
}