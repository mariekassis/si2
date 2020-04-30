using NUnit.Framework;
using si2;
using si2.bll.Classes;
using System;

namespace si2.tests.Services
{
    public class UniversityTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestUniversityContructor()
        {
            si2.bll.Classes.UniversityClass u = new si2.bll.Classes.UniversityClass("ABC");
            Assert.AreEqual("ABC", u.getName());
            //Assert.AreEqual("ABC", u.getName(),true,"Values not equal");
        }

        [Test]
        public void UniversityNameNullCheck()
        {
            Assert.Throws<Exception>(delegate {
                UniversityClass z = new UniversityClass(null);
            });
        }

        [Test]
        public void UniversityNameLengthCheck()
        {
            Assert.Throws<Exception>(delegate { 
                UniversityClass z = new UniversityClass("Universite Saint-Joseph de Beyrouth"); 
            });
        }

    }
}