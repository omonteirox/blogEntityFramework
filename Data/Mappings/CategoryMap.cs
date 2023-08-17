using blogEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blogEntityFramework.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // criação da table
            builder.ToTable("Category");

            // Chave primária
            builder.HasKey(x => x.Id);

            // PRIMARY KEY Identity 1,1
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            // propiedades

            builder.Property(x => x.Name).IsRequired().HasColumnName("Name").HasColumnType("NVARCHAR").HasMaxLength(80);
            builder.Property(x => x.Slug).IsRequired().HasColumnName("Slug").HasColumnType("VARCHAR").HasMaxLength(80);

            // Índices
            builder.HasIndex(x => x.Slug).HasName("IX_Category_Slug").IsUnique();
        }
    }
}