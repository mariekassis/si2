using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.dal.Interfaces
{
    public interface IHasConcurrency
    {
        [Timestamp]
        byte[] RowVersion { get; set; }
    }
}
