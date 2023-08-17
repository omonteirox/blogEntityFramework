using blogEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blogEntityFramework.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(x => x.Title).IsRequired().HasColumnName("Title").HasColumnType("NVARCHAR").HasMaxLength(80);
            builder.Property(x => x.Summary).IsRequired().HasColumnName("Summary").HasColumnType("NVARCHAR").HasMaxLength(80);
            builder.Property(x => x.Body).IsRequired().HasColumnName("Body").HasColumnType("NVARCHAR").HasMaxLength(80);
            builder.Property(x => x.Slug).IsRequired().HasColumnName("Slug").HasColumnType("VARCHAR").HasMaxLength(80);
            builder.Property(x => x.CreateDate).IsRequired().HasColumnName("CreateDate").HasColumnType("SMALLDATETIME").HasDefaultValue(DateTime.Now.ToUniversalTime());
            builder.Property(x => x.LastUpdateDate).IsRequired().HasColumnName("LastUpdateDate").HasColumnType("SMALLDATETIME").HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.HasIndex(x => x.Slug).HasName("IX_Post_Slug").IsUnique();

            builder.HasOne(x => x.Author).WithMany(x => x.Posts).HasConstraintName("FK_Post_Author").OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Category).WithMany(x => x.Posts).HasConstraintName("FK_Post_Category").OnDelete(DeleteBehavior.Restrict);
            // gera uma terceira tabela
            builder.HasMany(x => x.Tags).WithMany(x => x.Posts)
                .UsingEntity<Dictionary<string, object>>("PostTag",
                post => post.HasOne<Tag>().WithMany().HasForeignKey("PostId").HasConstraintName("FK_PostTag_PostId").OnDelete(DeleteBehavior.Restrict),
                tag => tag.HasOne<Post>().WithMany().HasForeignKey("TagId").HasConstraintName("FK_PostTag_TagId").OnDelete(DeleteBehavior.Restrict));
        }
    }
}

//public int Id { get; set; }
//public int AuthorId { get; set; }
//public int CategoryId { get; set; }
//public string Title { get; set; }
//public string Summary { get; set; }
//public string Body { get; set; }
//public string Slug { get; set; }
//public DateTime CreateDate { get; set; }
//public DateTime LastUpdateDate { get; set; }