using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace si2.bll.Classes
{
    public class InstitutionClass
    {
        private string code;
        private string name;
        private UniversityClass university;
        private List<InstitutionClass> children;

        public InstitutionClass(string code, string name, UniversityClass u)
        {
            if (code == null || code.Length < 3)
            {
                throw new Exception("Institution code problem");
            }
            if (u == null)
            {
                throw new Exception("Institution should have an owner...");
            }

            this.code = code;
            this.name = name;
            this.university = u;
            children = new List<InstitutionClass>();

        }

        public string GetCode()
        {
            return code;
        }

        public String GetName()
        {
            return name;
        }

        public UniversityClass GetUniversity()
        {
            return university;
        }

        public InstitutionClass createChild(string code, string name)
        {
            if (university.findByCode(code) != null)
            {
                throw new Exception("Duplicate code");
            }
            InstitutionClass i = new InstitutionClass(code, name, university);
            children.Add(i);
            return i;
        }

        public IReadOnlyCollection<InstitutionClass> GetChildren()
        {
            return children.AsReadOnly();
        }

        public InstitutionClass findByCode(string code)
        {
            if (this.code == code)
            {
                return this;
            }
            foreach (InstitutionClass inst in children)
            {
                InstitutionClass k = inst.findByCode(code);
                if (k != null)
                    return k;
            }
            return null;
        }
    }
}
