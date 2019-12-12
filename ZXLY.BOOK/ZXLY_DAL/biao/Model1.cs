namespace ZXLY_DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Book_information> Book_information { get; set; }
        public virtual DbSet<Book_type> Book_type { get; set; }
        public virtual DbSet<Borrowing_records> Borrowing_records { get; set; }
        public virtual DbSet<Sales_list> Sales_list { get; set; }
        public virtual DbSet<User_info> User_info { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_information>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Book_information>()
                .HasMany(e => e.Sales_list)
                .WithRequired(e => e.Book_information)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book_type>()
                .Property(e => e.Btype)
                .IsUnicode(false);

            modelBuilder.Entity<User_info>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User_info>()
                .Property(e => e.Pwd)
                .IsUnicode(false);

            modelBuilder.Entity<User_info>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User_info>()
                .Property(e => e.shenfen)
                .IsUnicode(false);

            modelBuilder.Entity<User_info>()
                .Property(e => e.addre)
                .IsUnicode(false);

            modelBuilder.Entity<User_info>()
                .HasMany(e => e.Borrowing_records)
                .WithRequired(e => e.User_info)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User_info>()
                .HasMany(e => e.Sales_list)
                .WithRequired(e => e.User_info)
                .WillCascadeOnDelete(false);
        }
    }
}
