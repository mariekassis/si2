using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.bll.Dtos.Requests.Institution
{
    public class UpdateInstitutionDto
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid universityId { get; set; }
    }
}
