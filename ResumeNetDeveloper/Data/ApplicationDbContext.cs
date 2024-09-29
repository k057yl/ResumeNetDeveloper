using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResumeNetDeveloper.Models;

namespace ResumeNetDeveloper.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employer> Employers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<EmployerSkill> EmployerSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployerSkill>()
                .HasKey(es => new { es.EmployerId, es.SkillId });

            modelBuilder.Entity<EmployerSkill>()
                .HasOne(es => es.Employer)
                .WithMany(e => e.EmployerSkills)
                .HasForeignKey(es => es.EmployerId);

            modelBuilder.Entity<EmployerSkill>()
                .HasOne(es => es.Skill)
                .WithMany(s => s.EmployerSkills)
                .HasForeignKey(es => es.SkillId);
        }
    }
}
