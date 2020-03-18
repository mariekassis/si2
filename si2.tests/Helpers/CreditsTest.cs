using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using si2.bll.Helpers.Credits;

namespace si2.tests.Helpers
{
    [TestFixture]
    class CreditsTest
    {
        [Test]
        public void CreditsCreation()
        {
            Credits c = new Credits(5);
            Assert.AreEqual(5, c.Value);
            Assert.Throws<NegativeCreditsException>(delegate {
                new Credits(-1);
            });
            c = new Credits(0);
            Assert.AreEqual(0, c.Value);
        }

        [Test]
        public void CreditsAddOperator()
        {
            Credits a = new Credits(5);
            Credits b = new Credits(7);
            Credits c = a + b;
            Assert.AreEqual(12, c.Value);
        }

        [Test]
        public void CreditsSubstractOperator()
        {
            Credits a = new Credits(5);
            Credits b = new Credits(7);
            Credits c = b-a;
            Assert.AreEqual(2, c.Value);
            Assert.Throws<NegativeCreditsException>(delegate {
                Credits d = a - b;
            });
        }
    }
}
