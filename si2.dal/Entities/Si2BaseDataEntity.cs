using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.dal.Entities
{
    public abstract class Si2BaseDataEntity<TPrimaryKey> : Si2BaseEntity<TPrimaryKey>, IHasConcurrency/*, IHasFullAudit*/
    {
        [Timestamp]
        public byte[] RowVersion { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime? CreatedOn { get; set; }
        //public string LastModifiedBy { get; set; }
        //public DateTime? LastModifiedOn { get; set; }
        //public string DeletedBy { get; set; }
        //public DateTime? DeletedOn { get; set; }
    }
}
