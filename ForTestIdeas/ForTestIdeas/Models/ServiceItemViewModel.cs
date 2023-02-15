using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForTestIdeas.Models
{
    public class ServiceItemViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescripton { get; set; }
    }
}
