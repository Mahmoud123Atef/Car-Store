using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurghadaStore.Core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; } // Front-end will send the Id as a GUID.
        public List<BasketItem> Items { get; set; }
        public CustomerBasket(string id)
        {
            Id = id;
            Items = new List<BasketItem>();
        }
    }
}
