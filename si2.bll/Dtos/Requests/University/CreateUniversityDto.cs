﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.bll.Dtos.Requests.University
{
    public class CreateUniversityDto
    {
        [Required(ErrorMessage="Name can't be null")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        public override bool Equals(Object obj) => Equals(obj as CreateUniversityDto);

        public bool Equals(CreateUniversityDto obj)
        {
            return (string.Equals(this.Name, obj.Name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
