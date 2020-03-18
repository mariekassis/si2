using NUnit.Framework;
using si2.bll.Helpers.MultiLingualSupport;
using System.Collections.Generic;

namespace si2.tests.MultiLingualSupport
{
    [TestFixture]
    class MLStringTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestLanguages()
        {
            Languages languages = Languages.getInstance();
            ICollection<ILanguage> supported = languages.SupportedLanguages();
            Assert.AreEqual(3, supported.Count);
            foreach(ILanguage l in supported)
            {
                Assert.AreEqual(l, languages.get(l.Code));
            }
        }

        [Test]
        public void TestMLString()
        {
            foreach (ILanguage language in Languages.getInstance().SupportedLanguages())
            {
                MLString s = new MLString(language, "Value");
                Assert.AreEqual("Value", s.Value(language));
                string serialized = s.Serialize();
                MLString s1 = new MLString(serialized);
                Assert.AreEqual("Value", s1.Value(language));
                foreach(ILanguage alt in Languages.getInstance().SupportedLanguages())
                {
                    if (alt != language)
                    {
                        Assert.Throws<UndefinedValueForLanguageException>(delegate {
                            s.Value(alt);
                        });
                    }
                    s.Set(alt, "Value for " + alt.Name);
                }
                MLString s2 = new MLString(s.Serialize());
                foreach (ILanguage alt in Languages.getInstance().SupportedLanguages())
                {
                    Assert.AreEqual("Value for " + alt.Name,s.Value(alt));
                    Assert.AreEqual(s.Value(alt), s2.Value(alt));
                }
            }
        }
    }
}
