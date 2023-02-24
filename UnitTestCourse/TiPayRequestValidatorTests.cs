using System;
using NSubstitute;
using NUnit.Framework;
using SystemUnderTest;
using UnitTestCourse.Defaults;

namespace UnitTestCourse
{
    [TestFixture]
    public class TiPayRequestValidatorTests
    {
        private TiPayRequestValidator _tiPayRequestValidator;
        private IMerchantRepository _merchantRepository;
        private TiPayRequest _tiPayRequest;
        private TestDelegate _action;

        [SetUp]
        public void SetUp()
        {
            _merchantRepository = Substitute.For<IMerchantRepository>();
            _tiPayRequestValidator = new TiPayRequestValidator(_merchantRepository);
        }

        // 2. write unit tests for TiPayRequestValidator
        [Test]
        public void Validate_HashedStringIsEqualToSignature_ShouldNotThrowException()
        {
            GivenMerchantKey("1234");
            GivenTiPayRequest(
                amount: 1000,
                merchantCode: "Wee",
                expected: "418f5f26db85d805dce85754b0b875c1");

            WhenValidate();
            ShouldNotThrowException();
        }

        [Test]
        public void Validate_HashedStringIsEqualToSignature_ThrowException()
        {
            GivenMerchantKey("123");
            GivenTiPayRequest(
                amount: 1000,
                merchantCode: "Wee",
                expected: "418f5f26db85d805dce85754b0b875c1");

            WhenValidate();
            ShouldThrowException();
        }

        private void ShouldThrowException()
        {
            Assert.Throws<Exception>(_action);
        }

        private void GivenMerchantKey(string key)
        {
            _merchantRepository.GetMerchantKey(Arg.Any<string>()).Returns(key);
        }

        private void ShouldNotThrowException()
        {
            Assert.DoesNotThrow(_action);
        }

        private TestDelegate WhenValidate()
        {
            return _action = () => _tiPayRequestValidator.Validate(_tiPayRequest);
        }

        private void GivenTiPayRequest(string merchantCode, int amount, string expected)
        {
            _tiPayRequest = TiPayRequestDefault.DefaultTiPayRequest(x =>
            {
                x.MerchantCode = merchantCode;
                x.Amount = amount;
                x.Signature = expected;
            });
        }
    }
}