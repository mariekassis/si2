using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace si2.dal.Entities
{
    [Table("BookCategory")]
    public class BookCategory
    {
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        public Book Book { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}