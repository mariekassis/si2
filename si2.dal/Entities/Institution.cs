using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace si2.dal.Entities
{
    [Table("Institution")]

    public class Institution : Si2BaseDataEntity<Guid>, IAuditable
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid UniversityId { get; set; }

        /*private string Name { get; set; }
        private string Code { get; set; }
        private University university { get; set; }
        private List<Institution> children;

        public Institution(string code, string name, University u)
        {
            if (code == null || code.Length < 3)
            {
                throw new Exception("Institution code can't be null or less than 3 characters");
            }
            if (u == null)
            {
                throw new Exception("Institution should be realted to an owner/university");
            }
            this.Code = code;
            this.Name = name;
            this.university = u;
            children = new List<Institution>();
        }

        public Institution createChild(string code, string name)
        {
            if (this.findByCode(code) != null)
            {
                throw new Exception("Duplicate code");
            }
            Institution i = new Institution(code, name, university);
            children.Add(i);
            return i;
        }

        public IReadOnlyCollection<Institution> GetChildren()
        {
            return children.AsReadOnly();
        }

        public Institution findByCode(string code)
        {
            if (this.Code == code)
            {
                return this;
            }
            foreach (Institution inst in children)
            {
                Institution k = inst.findByCode(code);
                if (k != null)
                    return k;
            }
            return null;
        }*/
    }

}
    

