using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.bll.Dtos.Requests.Institution
{
    public class CreateInstitutionDto
    {
        [Required]
        [StringLength(3, ErrorMessage = "Code cannot be longer than 3 characters.")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
