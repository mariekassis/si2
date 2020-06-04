using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace si2.dal.Entities
{
    [Table("Category")]
    public class Category : Si2BaseEntity<Guid>
    {
        public string CategoryName { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}