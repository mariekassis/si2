using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace si2.bll.Classes
{
    public class UniversityClass
    {
        //private string code;
        private string name;
        private List<InstitutionClass> institutions;

        public UniversityClass(string name)
        {
            if (name == null)
            {
                throw new Exception("University name can't be null");
            }

            if(name.Length > 10)
            {
                throw new Exception("University name can't be more than 10 characters!");
            }

            this.name = name;
            institutions = new List<InstitutionClass>();
        }

        public string getName()
        {
            return name;
        }

        public InstitutionClass createInstitution(string code, string name)
        {
            InstitutionClass f = findByCode(code);
            if (f != null)
            {
                throw new Exception("Duplicate code");
            }
            InstitutionClass i = new InstitutionClass(code, name, this);
            institutions.Add(i);
            return i;
        }

        public InstitutionClass findByCode(string code)
        {
            foreach (InstitutionClass i in institutions)
            {
                InstitutionClass k = i.findByCode(code);
                if (k != null)
                {
                    return k;
                }
            }
            return null;
        }

        public IEnumerable<InstitutionClass> getInstitutions()
        {
            return institutions;
        }

        public bool TestTestCase(int value)
        {
            if (value < 2)
                return false;
            throw new NotImplementedException("Please create a test first.");
        }
    }
}
