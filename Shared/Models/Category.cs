using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        
        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public List<Car> Cars { get; set; }



    }

    
}
