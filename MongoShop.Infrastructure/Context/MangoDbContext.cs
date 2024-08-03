using MangoShop.Domain.Models;
using Microsoft.EntityFrameworkCore;



namespace MangoShop.Infraestructure.Context;



/// <summary>
/// Class responsible for managing the connection to the database
/// </summary>
public class MangoDbContext : DbContext
{


    public MangoDbContext(DbContextOptions options) : base(options) { }


    /// <summary>
    /// Property for represent the WhatsApp messages table
    /// </summary>
    public DbSet<WhatsAppMessage> WhatsAppMessages { get; set; }


    /// <summary>
    /// override the default behavior of the OnModelCreating method
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<WhatsAppMessage>(entity =>
        {


            entity.ToTable("WhatsAppMessages", schema: "dbo");


            entity.HasKey(e => e.Id);


            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();  

            entity.Property(e => e.Oui);

            entity.Property(e => e.PhoneTo)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.TemplanteNameUsed)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.MessageBody)
                .HasMaxLength(4000);

            entity.Property(e => e.MessageId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PhoneFrom)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.PhoneId)
                .HasMaxLength(50);

            entity.Property(e => e.SendingAt)
                .HasDefaultValue(false);

            entity.Property(e => e.SendingDate);


            entity.Property(e => e.DeliveredAt)
                .HasDefaultValue(false);


            entity.Property(e => e.DeliveredDateConfirm);


            entity.Property(e => e.DeliveredDateRegister);


            entity.Property(e => e.ReadedAt)
                .HasDefaultValue(false);


            entity.Property(e => e.ReadedDate);


            entity.Property(e => e.ReadedDateRegister);


            entity.Property(e => e.FailedAt)
                .HasDefaultValue(false);


            entity.Property(e => e.FailedDate);


            entity.Property(e => e.FailedDateRegister);


        });

        base.OnModelCreating(modelBuilder);


    }


}
