using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class Address
    {
        public Guid AddressId { get; set; }

        [Required]
        [MaxLength(50)]
        public string AddressLine1 { get; set; }

        [MaxLength(50)]
        public string AddressLine2 { get; set; }

        [NotMapped]
        public Country Country
        {
            get
            {
                return State.Country;
            }
        }

        [NotMapped]
        public State State
        {
            get
            {
                return City.State;
            }
        }

        [NotMapped]
        public City City
        {
            get
            {
                return Suburb.City;
            }
        }

        [Required]
        public virtual Suburb Suburb { get; set; }

        [Required]
        public string PostalCode { get; set; }
    }
}
