using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace TeachingTool.Models
{
    public class TeachingToolDBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Question> Questions { get; set; }
        public TeachingToolDBContext(DbContextOptions<TeachingToolDBContext> options) : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //}
    }
}