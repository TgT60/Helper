using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForTestIdeas.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Name { get; set; }

        public string SureName { get; set; }

        public string PersonImg { get; set; }

    }
}
