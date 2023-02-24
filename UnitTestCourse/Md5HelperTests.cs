using NUnit.Framework;
using SystemUnderTest;

namespace UnitTestCourse
{
    [TestFixture]
    public class Md5HelperTests
    {
        private Md5Helper _md5Helper;
        private string _input;
        private string _actual;
        private string _expected;

        [SetUp]
        public void SetUp()
        {
            _md5Helper = new Md5Helper();
        }

        // 1. write a test for Md5Helper
        // online md5 hash generator: https://www.md5hashgenerator.com/
        [Test]
        public void Hash_InputString_ReturnsMD5HashedString()
        {
            GivenInputString("Wee");
            WhenDoMD5Hash();
            StringSHouldBe("0b77f4cf729ef04c4bb333bcd7935408");
        }

        private void StringSHouldBe(string expected)
        {
            Assert.AreEqual(expected, _actual);
        }

        private void WhenDoMD5Hash()
        {
            _actual = _md5Helper.Hash(_input);
        }

        private void GivenInputString(string input)
        {
            _input = input;
        }
    }
}