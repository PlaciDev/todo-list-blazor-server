using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListBlazorServer.Models;

namespace ToDoListBlazorServer.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnName("PasswordHash")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255);

            builder.HasMany(x => x.TaskItems)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);





        }
    }
}
