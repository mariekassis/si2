using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace si2.dal.Entities
{
    [Table("Book")]
    public class Book : Si2BaseEntity<Guid>
    {
        public string Title { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }
    }
}