using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class City
    {
        public Guid CityId { get; set; }

        [Required]
        public virtual State State { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
