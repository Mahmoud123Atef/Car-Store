using HurghadaStore.Core.Entities;
using HurghadaStore.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurghadaStore.Repository
{
    internal static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec) 
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }


            // Accumalation
            query = spec.Includes.Aggregate(query, (currentQuery, IncludeExpression) 
                                                => currentQuery.Include(IncludeExpression));

            query = query.Skip(spec.Skip).Take(spec.Take);

            return query;
        }
    }
}
