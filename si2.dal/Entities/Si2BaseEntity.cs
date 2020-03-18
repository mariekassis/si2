using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.dal.Entities
{
    public abstract class Si2BaseEntity<TPrimaryKey>
    {
        [Key]
        public TPrimaryKey Id { get; set; }
    }
}
