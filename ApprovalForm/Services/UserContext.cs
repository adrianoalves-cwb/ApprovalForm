using System;
using ApprovalForm.Modal;
using Microsoft.EntityFrameworkCore;

namespace ApprovalForm.Services
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();

        }

        public DbSet<User> Users { get; set; }
    }
}

