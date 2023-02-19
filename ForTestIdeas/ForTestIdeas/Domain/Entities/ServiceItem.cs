using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForTestIdeas.Domain.Entities
{
    public class ServiceItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescripton { get; set; }

        public ICollection<TaskTiket> TaskTikets { get; set; }
    }
}
