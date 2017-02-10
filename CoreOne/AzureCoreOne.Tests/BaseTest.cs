using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AzureCoreOne.Tests
{
    public class BaseTest
    {
        public BaseTest()
        {
        }

        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        public void IndexActionModelIsComplete()
        {
            //var controller = new ProBookController
        }

        [Fact]
        public void FaillingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        public void FirstTheory(int value)
        {
            Assert.True(IsOdd(value));
        }

        private bool IsOdd(int value)
        {
            return value % 2 == 1;
        }

        private int Add(int v1, int v2)
        {
            return v1 + v2;
        }
    }
}
