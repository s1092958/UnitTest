using System;
using NSubstitute;
using NUnit.Framework;
using SystemUnderTest;

namespace UnitTestCourse
{
    [TestFixture]
    public class TitanPayRequestTests
    {
        // 宣告
        private IMerchantKeyReader _iMerchantKeyReader;
        private TitanPayRequest _titanPayRequest;
        private IGetDate _textGetDate;

        [SetUp]
        public void SetUp()
        {
            // 注入
            _iMerchantKeyReader = Substitute.For<IMerchantKeyReader>();
            _textGetDate = Substitute.For<IGetDate>();
            _titanPayRequest = new TitanPayRequest(_iMerchantKeyReader, _textGetDate);

            _titanPayRequest.Amount = 100;
        }

        // 3. write tests for TitanPayRequest.Sign()
        [Test]
        public void validate_signature_with_fixed_merchantKey()
        {
            _titanPayRequest.Sign();
            Assert.AreEqual(_titanPayRequest.Signature, "fd98262a120ec2f1c7612f7fa0a5cb29");
        }

        // 4. write unit test for Sign2
        [Test]
        public void calculate_signature_with_key_from_file()
        {
            _iMerchantKeyReader.Get().Returns("VV");

            _titanPayRequest.Sign2();
            Assert.AreEqual(_titanPayRequest.Signature, "293e4188082b2a8ca6672e6ecfc38fd9");
        }

        // 5. write unit test for Sign3
        [Test]
        public void calculate_signature_with_created_on()
        {
            _textGetDate.GetDate().Returns(new DateTime(2019, 05, 09, 09, 15, 00));
            _titanPayRequest.Sign3();
            Assert.AreEqual(_titanPayRequest.Signature, "375502e895578a784d1416011e2dfa48");
        }
    }
}