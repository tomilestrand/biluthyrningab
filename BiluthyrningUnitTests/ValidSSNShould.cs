using System;
using Xunit;
using BiluthyrningABdel1;

namespace BiluthyrningUnitTests
{
    public class ValidSSNShould
    {
        [Fact]
        public void ReturnBool()
        {
            var result = Biluthyrning.ValidSSN("12341234-1234");
            Assert.IsType<bool>(result);
        }

        [Fact]
        public void AcceptCorrectSSN()
        {
            var result = Biluthyrning.ValidSSN("12341234-1234");
            Assert.True(result);
        }

        [Fact]
        public void NotAcceptWhitespace()
        {
            var result = Biluthyrning.ValidSSN("             ");
            Assert.False(result);
        }

        [Fact]
        public void NotAcceptEmpty()
        {
            var result = Biluthyrning.ValidSSN("");
            Assert.False(result);
        }

        [Fact]
        public void NotAcceptLetters()
        {
            var result = Biluthyrning.ValidSSN("12341234-abcd");
            Assert.False(result);
        }

        [Fact]
        public void NotAcceptSymbols()
        {
            var result = Biluthyrning.ValidSSN("12341234-%&¤/");
            Assert.False(result);
        }

        [Fact]
        public void NotAcceptShortSSN()
        {
            var result = Biluthyrning.ValidSSN("12341234-123");
            Assert.False(result);
        }

        [Fact]
        public void NotAcceptLongSSN()
        {
            var result = Biluthyrning.ValidSSN("12341234-12345");
            Assert.False(result);
        }

        [Fact]
        public void NotAcceptMissingHyphen()
        {
            var result = Biluthyrning.ValidSSN("1234123412345");
            Assert.False(result);
        }

        [Fact]
        public void NotAcceptMissplacedHyphen()
        {
            var result = Biluthyrning.ValidSSN("1234123412-34");
            Assert.False(result);
        }
    }
}
