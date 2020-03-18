using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Administration
{
    public class CreateRoleDto
    {
        [Required]
        public string RoleName { get; set; }
    }
}
