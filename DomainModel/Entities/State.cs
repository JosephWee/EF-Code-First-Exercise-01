using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class State
    {
        public Guid StateId { get; set; }

        [Required]
        public virtual Country Country { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
