using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelperAPI.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Name { get; set; }

        public string SureName { get; set; }

        public string PersonImg { get; set; }

        public ICollection<Equipment> EquipmentId { get; set; }

        public ICollection<TaskTiket> TaskTikets { get; set; }

    }
}
