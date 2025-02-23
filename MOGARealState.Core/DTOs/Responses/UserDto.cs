using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.DTOs.Responses
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;

        public string Role { get; set; }
    }
}
