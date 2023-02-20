using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ForTestIdeas.Domain.Entities
{
    public class Equipment
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        
        public Guid UserId { get; set; }
    }
}
