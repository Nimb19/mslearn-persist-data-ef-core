using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ContosoPizza.Models;

namespace ContosoPizza.Data
{
    public partial class PromotionsContext : DbContext
    {
        public PromotionsContext(DbContextOptions<PromotionsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Coupon> Coupons => Set<Coupon>();
    }
}
