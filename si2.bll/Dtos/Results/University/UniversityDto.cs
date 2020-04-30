using System;
using System.Collections.Generic;
using System.Text;
using static si2.common.Enums;

namespace si2.bll.Dtos.Results.University
{
    public class UniversityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(Object obj) => Equals(obj as UniversityDto);

        public bool Equals(UniversityDto obj)
        {
            return (this.Id == obj.Id
                //&& string.Equals(this.Title, obj.Title, StringComparison.OrdinalIgnoreCase)
                && string.Equals(this.Name, obj.Name, StringComparison.OrdinalIgnoreCase));
        }

    }
}
