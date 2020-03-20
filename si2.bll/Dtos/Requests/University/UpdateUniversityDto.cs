using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.bll.Dtos.Requests.University
{
    public class UpdateUniversityDto
    {
        [Required]
        public string Name { get; set; }

    }
}
