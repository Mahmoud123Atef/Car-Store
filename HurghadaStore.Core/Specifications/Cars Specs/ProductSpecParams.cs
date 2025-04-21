﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurghadaStore.Core.Specifications.Cars_Specs
{
    public class ProductSpecParams
    {
        private const int maxPageSize = 10;
        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > maxPageSize ? maxPageSize : value ; }
        }

        public int PageIndex { get; set; } = 1;

        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
    }
}
