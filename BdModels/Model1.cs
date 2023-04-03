using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MpdaTest.BdModels
{
    public partial class Model1 : DbContext
    {
        public Model1()

          //  : base("data source=31.31.196.234;initial catalog=u1723122_TestSis;persist security info=True;user id=u1723122_u1723122;password=CVhXN2Mun60yLT1N;MultipleActiveResultSets=True;App=EntityFramework")
           : base("data source=localhost;initial catalog=u1723122_TestSis;persist security info=True;user id=u1723122_u1723122;password=CVhXN2Mun60yLT1N;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }

        public virtual DbSet<AnswerOpenTest> AnswerOpenTest { get; set; }
        public virtual DbSet<AnswerT> AnswerT { get; set; }
        public virtual DbSet<AnswerTheme> AnswerTheme { get; set; }
        public virtual DbSet<OpisTheme> OpisTheme { get; set; }
        public virtual DbSet<question> question { get; set; }
        public virtual DbSet<TableTest> TableTest { get; set; }
        public virtual DbSet<TestAnswer> TestAnswer { get; set; }
        public virtual DbSet<TestOpen> TestOpen { get; set; }
        public virtual DbSet<TestSistem> TestSistem { get; set; }
        public virtual DbSet<TestSort> TestSort { get; set; }
        public virtual DbSet<Theme> Theme { get; set; }
        public virtual DbSet<ThemeTest> ThemeTest { get; set; }
        public virtual DbSet<UserSelectTest> UserSelectTest { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnswerOpenTest>()
                .Property(e => e.Answer)
                .IsUnicode(false);

            modelBuilder.Entity<AnswerT>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<AnswerT>()
                .Property(e => e.TextUser)
                .IsUnicode(false);

            modelBuilder.Entity<AnswerTheme>()
                .Property(e => e.AnswerText)
                .IsUnicode(false);

            modelBuilder.Entity<OpisTheme>()
                .Property(e => e.TypeImage)
                .IsUnicode(false);

            modelBuilder.Entity<OpisTheme>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<question>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<question>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<TableTest>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TableTest>()
                .Property(e => e.Desp)
                .IsUnicode(false);

            modelBuilder.Entity<TestAnswer>()
                .Property(e => e.Question)
                .IsUnicode(false);

            modelBuilder.Entity<TestOpen>()
                .Property(e => e.Question)
                .IsUnicode(false);

            modelBuilder.Entity<TestSistem>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TestSistem>()
                .Property(e => e.DateOpen)
                .IsUnicode(false);

            modelBuilder.Entity<TestSistem>()
                .Property(e => e.DateClose)
                .IsUnicode(false);

            modelBuilder.Entity<TestSort>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Theme>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<ThemeTest>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UserSelectTest>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<UserSelectTest>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
