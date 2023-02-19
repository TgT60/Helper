using Microsoft.AspNetCore.Mvc.Rendering;
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

        public string Name { get; set; }

        public string SureName { get; set; }


    }
}
