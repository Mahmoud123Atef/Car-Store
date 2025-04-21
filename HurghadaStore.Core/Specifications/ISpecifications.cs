using HurghadaStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HurghadaStore.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        // Property Signitures
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }

        public bool IsPaginationEnabled { get; set; }

    }
}
