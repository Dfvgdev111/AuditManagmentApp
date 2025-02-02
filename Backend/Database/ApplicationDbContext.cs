using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;
using Backend.Models;
using Backend.test;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Database
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        
        public ApplicationDbContext(DbContextOptions dbContext): base(dbContext){
        }

        public DbSet<Project> Projects {get;set;}
       public DbSet<ProjectPortfolio> ProjectPortfolios {get;set;}

       public DbSet<Audit> Audit {get;set;}

       public DbSet<AuditCategory> AuditCategories {get;set;}

       public DbSet<AuditQuestion> AuditQuestions {get;set;}

       public DbSet<UserRequests> UserRequests {get;set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            builder.Entity<ProjectPortfolio>(x=> x.HasKey(p=> new {p.AppUserId,p.ProjectId}));

            builder.Entity<ProjectPortfolio>()
            .HasOne(u=> u.AppUser)
            .WithMany(u=> u.ProjectPortfolios)
            .HasForeignKey(u=> u.AppUserId);

            builder.Entity<ProjectPortfolio>()
            .HasOne(u=> u.Project)
            .WithMany(u=> u.ProjectPortfolios)
            .HasForeignKey(u=> u.ProjectId);

            

            builder.Entity<AuditCategory>()
            .HasOne(u=> u.Audit)
            .WithMany(u=> u.AuditCategories)
            .HasForeignKey(u=> u.AuditId);

            builder.Entity<Audit>()
            .HasOne(u=> u.Project)
            .WithMany(u=>u.Audit)
            .HasForeignKey(u=>u.ProjectId);

            
            builder.Entity<UserRequests>(entity => {

                entity.HasOne(ur => ur.InvitedUser)
                .WithMany()
                .HasForeignKey(x=> x.InvitedUserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ur=> ur.InviterUser)
                .WithMany()
                .HasForeignKey(x=> x.InviterUserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Project)
                .WithMany()
                .HasForeignKey(x=> x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(ur => new {ur.InvitedUserId,ur.ProjectId})
                .IsUnique();
            });
       

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
                    optionsBuilder.UseLazyLoadingProxies(false);

        }
    }
}