using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurghadaStore.Core.Entities
{
    public class CarBrand : BaseEntity
    {
        public string Name { get; set; }

        // public ICollection<Car> Cars { get; set; } = new HashSet<Car>(); // Using [HashSet] to avoid non reference Exceptions.
    }
}
