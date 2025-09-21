using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class RoleDTO
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public string RoleDescription { get; set; } = null!;
    }

    public class ChangeRoleDTO
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
