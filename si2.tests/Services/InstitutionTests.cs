using NUnit.Framework;
using si2.bll.Classes;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace si2.tests.Services
{
    class InstitutionTests
    {
        UniversityClass u;
        [SetUp]
        public void setup()
        {
            u = new UniversityClass("AAA");
        }

        [Test]
        public void TestInstitutionCreation()
        {
            InstitutionClass inst = new InstitutionClass("ABC", "B",u);
            Assert.AreEqual("ABC", inst.GetCode());
            Assert.AreEqual("B", inst.GetName());
            Assert.AreEqual(u, inst.GetUniversity());
        }

        [Test]
        public void TestInstitutionNullCode()
        {
            Assert.Throws<Exception>(delegate { new InstitutionClass(null, "",u); });
        }
        [Test]
        public void TestInstitutionCodeLength()
        {
            Assert.Throws<Exception>(delegate { new InstitutionClass("AB", "", u); });
            Assert.Throws<Exception>(delegate { new InstitutionClass("A", "", u); });
        }

        [Test]
        public void TestInstitutionCodeLenLT3()
        {
            Assert.Throws<Exception>(delegate {
                InstitutionClass i = new InstitutionClass("A", "B",u);
            });
            Assert.Throws<Exception>(delegate {
                InstitutionClass i = new InstitutionClass("AB", "B",u);
            });
        }

        [Test]
        public void TestInstitutionOwnerNull()
        {
            Assert.Throws<Exception>(delegate { new InstitutionClass("ABC", "", null); });
        }
        [Test]
        public void TestChildren()
        {
            List<string> code = new List<string>();
            List<string> name = new List<string>();
            InstitutionClass inst = u.createInstitution("COD", "INST");
            for(int i = 0; i < 10; ++i)
            {
                string c = "Code" + i;
                string n = "Name" + i;
                InstitutionClass k = inst.createChild(c, n);
                code.Add(c);
                name.Add(n);
                Assert.AreEqual(c, k.GetCode());
                Assert.AreEqual(n, k.GetName());
            }
            IReadOnlyCollection<InstitutionClass> insts = inst.GetChildren();
            Assert.AreEqual(10, insts.Count);
            foreach(InstitutionClass t in insts){
                int idx = code.IndexOf(t.GetCode());
                Assert.GreaterOrEqual(idx, 0);
                code[idx] = "-----";
            }
        }

        [Test]
        public void TestUniqueCode()
        {
            InstitutionClass i1 = u.createInstitution("COD1", "NAME1");
            InstitutionClass i2 = u.createInstitution("COD2", "NAME2");
            Assert.Throws<Exception>(delegate {
                i1.createChild("COD1", "NAME3");
            });
            Assert.Throws<Exception>(delegate {
                i2.createChild("COD1", "NAME3");
            });
        }
    }
}
