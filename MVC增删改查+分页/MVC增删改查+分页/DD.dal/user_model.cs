namespace DD.dal
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class user_model : DbContext
    {
        public user_model()
            : base("name=user_model")
        {
        }

        public virtual DbSet<Login_Info> Login_Info { get; set; }
        public virtual DbSet<Login_role> Login_role { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login_Info>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Login_Info>()
                .Property(e => e.Useraddress)
                .IsUnicode(false);

            modelBuilder.Entity<Login_Info>()
                .Property(e => e.UserEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Login_role>()
                .Property(e => e.Rolename)
                .IsUnicode(false);

            modelBuilder.Entity<Login_role>()
                .Property(e => e.Roleaction)
                .IsUnicode(false);
        }
    }
}
