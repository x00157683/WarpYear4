using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Car
    {

        [Key]
        public int CarId { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public double Range { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public double PricePerUnit { get; set; }
        [Required]
        public bool isLocked { get; set; }

        public int Rating { get; set; }

        public string Location { get; set; }
        public int Reviews { get; set; }

        public double Lat {get; set;}

        public double Lng { get; set; }

        public double RangeLeft { get; set; }

        public Category? Category { get; set; }

        public bool Active { get; set; }

        private int _overallRating;
        public int OverallRating
        {
            get
            {
                if(Rating > 0 && Reviews > 0) 
                {
                    return Rating / Reviews;
                }
                else
                {
                    return 0;
                }
                
            }
            set
            {
                _overallRating = value;
            }
        }



    }
}
