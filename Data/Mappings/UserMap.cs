using blogEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blogEntityFramework.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasColumnName("Name").HasColumnType("NVARCHAR").HasMaxLength(80);
            builder.Property(x => x.Email).IsRequired().HasColumnName("Email").HasColumnType("NVARCHAR").HasMaxLength(80);
            builder.Property(x => x.PasswordHash).IsRequired().HasColumnName("PasswordHash").HasColumnType("NVARCHAR").HasMaxLength(120);
            builder.Property(x => x.Bio).IsRequired().HasColumnName("Bio").HasColumnType("NVARCHAR").HasMaxLength(80);
            builder.Property(x => x.Image).IsRequired().HasColumnName("Image").HasColumnType("NVARCHAR").HasMaxLength(90);
            builder.Property(x => x.Slug).IsRequired().HasColumnName("Slug").HasColumnType("VARCHAR").HasMaxLength(80);

            builder.HasIndex(x => x.Slug).HasName("IX_User_Slug").IsUnique();

            builder.HasMany(x => x.Roles).WithMany(x => x.Users)
                .UsingEntity<Dictionary<string, object>>("UserRole",
                role => role.HasOne<Role>().WithMany().HasForeignKey("RoleId").HasConstraintName("FK_UserRole_RoleId").OnDelete(DeleteBehavior.Restrict),
                user => user.HasOne<User>().WithMany().HasForeignKey("UserId").HasConstraintName("FK_UserRole_UserId").OnDelete(DeleteBehavior.Restrict)
                );
        }
    }
}

//public int Id { get; set; }
//public string Name { get; set; }
//public string Email { get; set; }
//public string PasswordHash { get; set; }
//public string Bio { get; set; }
//public string Image { get; set; }
//public string Slug { get; set; }