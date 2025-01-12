using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Abstractions
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid().ToString().Substring(0,10);
        }
        public string Id { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
