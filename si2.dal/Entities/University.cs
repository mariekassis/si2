using si2.dal.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace si2.dal.Entities
{
	[Table("University")]

	public class University : Si2BaseDataEntity<Guid>, IAuditable
	{
		[Required]
		public string Name { get; set; }
	}
}
