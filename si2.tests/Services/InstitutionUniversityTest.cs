using NUnit.Framework;
using si2;
using si2.bll.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace si2.tests.Services
{
    [TestFixture]
    class InstitutionUniversityTest
    {
        private UniversityClass u;
        [SetUp]
        public void setup()
        {
            u = new UniversityClass("ABC");
        }

        [Test]
        public void TestUniversityCreation()
        {
            foreach(InstitutionClass i in u.getInstitutions())
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestFirstInstitutionCreation()
        {
            InstitutionClass i=u.createInstitution("AVX", "ABC");
            Assert.IsNotNull(i);
            Assert.AreEqual("AVX", i.GetCode());
            Assert.AreEqual("ABC", i.GetName());
            int c = 0;
            foreach(InstitutionClass x in u.getInstitutions())
            {
                Assert.AreEqual(x, i);
                c = c + 1;
            }
            Assert.AreEqual(1, c);
        }
        [Test]
        public void TestInstitutionsCreation()
        {
            List<string> code = new List<string>();
            List<string> name = new List<string>();
            for (int i = 0; i < 10; ++i)
            {
                code.Add("INST" + i);
                name.Add("NAME" + i);
                InstitutionClass inst=u.createInstitution(code[i], name[i]);
                Assert.IsNotNull(inst);
                Assert.AreEqual(inst.GetName(), name[i]);
                Assert.AreEqual(inst.GetCode(), code[i]);
            }
            int c = 0;
            foreach(InstitutionClass inst in u.getInstitutions())
            {
                int idx = code.IndexOf(inst.GetCode());
                Assert.AreEqual(idx >= 0, true);
                code[idx] = "----";
                c = c + 1;
            }
            Assert.AreEqual(10, c);
        }

        [Test]
        public void TestDuplicateInstitutionCode()
        {
            u.createInstitution("ABC", "ABC");
            Assert.Throws<Exception>(delegate
            {
                u.createInstitution("ABC", "ABX");
            });
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
        {
            //var result = _primeService.IsPrime(value);
            var result = u.TestTestCase(value);

            Assert.IsFalse(result, $"{value} should not be prime");
        }
    }
}
