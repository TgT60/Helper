using ForTestIdeas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForTestIdeas.Models
{
    public class TaskTiketViewModel
    {
        public Guid Id { get; set; }

        public ServiceItem ServiceItem { get; set; }

        public User User { get; set; }
    }
}
