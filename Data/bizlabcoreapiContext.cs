using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using bizlabcoreapi.Models;

namespace bizlabcoreapi.Data
{
    public class bizlabcoreapiContext : DbContext
    {
        public bizlabcoreapiContext (DbContextOptions<bizlabcoreapiContext> options)
            : base(options)
        {
        }

        public DbSet<bizlabcoreapi.Models.staff_users> StaffModel { get; set; } = default!;
    }
}
