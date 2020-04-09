using System;
using System.Collections.Generic;
using System.Text;

namespace si2.bll.Dtos.Results.Institution
{
    public class InstitutionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid UniversityId { get; set; }
    }
}
