using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ForumQuestions.Models;

namespace ForumQuestions.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<ForumQuestions.Models.Question> Question { get; set; }
        public DbSet<ForumQuestions.Models.Reply> Reply { get; set; }



    }
}
