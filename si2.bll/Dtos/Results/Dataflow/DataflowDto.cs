using System;
using System.Collections.Generic;
using System.Text;
using static si2.common.Enums;

namespace si2.bll.Dtos.Results.Dataflow
{
    public class DataflowDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }    
        public string Status { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
