using Microsoft.EntityFrameworkCore;

namespace URLShort.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public UserContext(DbContextOptions<UserContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    string adminRoleName = "admin";
        //    string userRoleName = "user";


        //    string adminLogin = "admin";
        //    string adminPassword = "123456";
        //    string userLogin = "user";
        //    string userPassword = "12345";

        //    Role adminRole = new Role { Id = 1, Name = adminRoleName };
        //    Role userRole = new Role { Id=2, Name = userRoleName };

        //    User adminUser = new User { Id = 1, Login=adminLogin, Passwood = adminPassword , RoleId = adminRole.Id};
        //    User user = new User { Id = 2, Login=userLogin, Passwood=userPassword, RoleId = userRole.Id};

        //    modelBuilder.Entity<Role>().HasData(adminRole, userRole);
        //    modelBuilder.Entity<User>().HasData(adminUser, user);

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
