using System;
using System.Collections.Generic;
using System.Text;
using Collective.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Collective.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Record> Record { get; set; }
        public DbSet<Collection> Collection { get; set; }
        public DbSet<Memory> Memory { get; set; }
        public DbSet<UserFriend> UserFriend { get; set; }
        public DbSet<DiscogsMasterSearch> DiscogsMasterSearch { get; set; }
        
    }
}

