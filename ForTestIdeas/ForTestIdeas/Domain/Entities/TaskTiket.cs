using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForTestIdeas.Domain.Entities
{
    public class TaskTiket
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ServiceItemId { get; set; }
    }
}
