using IdentitySample.Server.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentitySample.Server
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("SYS_USER");
            builder.Entity<IdentityRole>().ToTable("SYS_ROLE");
            builder.Entity<IdentityUserRole<string>>().ToTable("SYS_USER_ROLE");
            builder.Entity<IdentityUserLogin<string>>().ToTable("SYS_USER_LOGIN");
            builder.Entity<IdentityUserClaim<string>>().ToTable("SYS_USER_CLAIM");
            builder.Entity<IdentityUserToken<string>>().ToTable("SYS_USER_TOKEN");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("SYS_ROLE_CLAIM");

            builder.Entity<User>().Property(x => x.DisplayName).HasMaxLength(64).IsRequired().IsUnicode();

            string userId = Guid.NewGuid().ToString();
            string roleId = Guid.NewGuid().ToString();
            builder.Entity<User>().HasData(new User
            {
                Id = userId,
                UserName = "admin",
                NormalizedEmail = "ADMIN",
                PasswordHash = "AQAAAAEAACcQAAAAEChYqJukWs6yIODbphrtn5xOgqscDwFEHIG7VBS5/l4H6ejXlh09QiBtWNvbZCAXyw==",
                SecurityStamp = "WDPMF543UJABCB5COV335ULZQPTCFM7M",
                ConcurrencyStamp = "28bac77e-5135-4bd7-8afd-523c767fe576",
                DisplayName = "管理员"
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole("管理员") { Id = roleId });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = userId, RoleId = roleId });
        }
    }
}
