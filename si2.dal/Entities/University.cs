using si2.dal.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace si2.dal.Entities
{
	[Table("University")]

	public class University : Si2BaseDataEntity<Guid>, IAuditable
	{
		[Required]
		public string Name { get; set; }
        private List<Institution> institutions;

        //public University(string code, string name)
        public University(string name)
        {
            /* (code == null || code.Length < 3)
            {
                throw new Exception("University Code can't be null or less than three characters!");
            }
            this.code = code;*/

            if (name == null)
            {
                throw new Exception("University name can't be null");
            }

            this.Name = name;
            institutions = new List<Institution>();
        }

    }
}
