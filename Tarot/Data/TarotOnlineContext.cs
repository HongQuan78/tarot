using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tarot.Data;

public partial class TarotOnlineContext : DbContext
{
    public TarotOnlineContext()
    {
    }

    public TarotOnlineContext(DbContextOptions<TarotOnlineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Deck> Decks { get; set; }

    public virtual DbSet<Meaning> Meanings { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    public virtual DbSet<ReadingHistory> ReadingHistories { get; set; }

    public virtual DbSet<TarotCard> TarotCards { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WorkingHour> WorkingHours { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-5DIAVQ3\\SQLEXPRESS; database=TarotOnline; uid=sa;pwd=00000000;TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Deck>(entity =>
        {
            entity.HasKey(e => e.DeckId).HasName("PK__Decks__23DA38D0618D38E4");

            entity.Property(e => e.DeckId).HasColumnName("deck_id");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");

            entity.HasMany(d => d.Cards).WithMany(p => p.Decks)
                .UsingEntity<Dictionary<string, object>>(
                    "DeckCard",
                    r => r.HasOne<TarotCard>().WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DeckCards__card___4E88ABD4"),
                    l => l.HasOne<Deck>().WithMany()
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DeckCards__deck___4D94879B"),
                    j =>
                    {
                        j.HasKey("DeckId", "CardId").HasName("PK__DeckCard__E80518CD0A39BC8F");
                        j.ToTable("DeckCards");
                        j.IndexerProperty<int>("DeckId").HasColumnName("deck_id");
                        j.IndexerProperty<int>("CardId").HasColumnName("card_id");
                    });
        });

        modelBuilder.Entity<Meaning>(entity =>
        {
            entity.HasKey(e => e.MeaningId).HasName("PK__Meanings__F5D174D1BA9655B1");

            entity.Property(e => e.MeaningId).HasColumnName("meaning_id");
            entity.Property(e => e.Meaning1).HasColumnName("meaning");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.HasKey(e => e.ReaderId).HasName("PK__Readers__40E382884BCC5262");

            entity.Property(e => e.ReaderId)
                .ValueGeneratedNever()
                .HasColumnName("reader_id");
            entity.Property(e => e.MeetingLink).HasColumnName("meeting_link");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Rating)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("rating");
            entity.Property(e => e.Schedule).HasColumnName("schedule");

            entity.HasOne(d => d.ReaderNavigation).WithOne(p => p.Reader)
                .HasForeignKey<Reader>(d => d.ReaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Readers__reader___3C69FB99");
        });

        modelBuilder.Entity<ReadingHistory>(entity =>
        {
            entity.HasKey(e => e.ReadingId).HasName("PK__ReadingH__8091F95A0E1B1BF9");

            entity.ToTable("ReadingHistory");

            entity.Property(e => e.ReadingId).HasColumnName("reading_id");
            entity.Property(e => e.DateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_time");
            entity.Property(e => e.DeckId).HasColumnName("deck_id");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.ReaderId).HasColumnName("reader_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Deck).WithMany(p => p.ReadingHistories)
                .HasForeignKey(d => d.DeckId)
                .HasConstraintName("FK__ReadingHi__deck___5441852A");

            entity.HasOne(d => d.Reader).WithMany(p => p.ReadingHistories)
                .HasForeignKey(d => d.ReaderId)
                .HasConstraintName("FK__ReadingHi__reade__52593CB8");

            entity.HasOne(d => d.User).WithMany(p => p.ReadingHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ReadingHi__user___5165187F");
        });

        modelBuilder.Entity<TarotCard>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__TarotCar__BDF201DD1E873AA2");

            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.Arcana)
                .HasMaxLength(255)
                .HasColumnName("arcana");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasMany(d => d.Meanings).WithMany(p => p.Cards)
                .UsingEntity<Dictionary<string, object>>(
                    "CardMeaning",
                    r => r.HasOne<Meaning>().WithMany()
                        .HasForeignKey("MeaningId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CardMeani__meani__48CFD27E"),
                    l => l.HasOne<TarotCard>().WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CardMeani__card___47DBAE45"),
                    j =>
                    {
                        j.HasKey("CardId", "MeaningId").HasName("PK__CardMean__B2AF16900D805656");
                        j.ToTable("CardMeanings");
                        j.IndexerProperty<int>("CardId").HasColumnName("card_id");
                        j.IndexerProperty<int>("MeaningId").HasColumnName("meaning_id");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F0B89E8F8");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E61648C8170BC").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC572A7F391B5").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Birthday)
                .HasColumnType("date")
                .HasColumnName("birthday");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .HasColumnName("gender");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<WorkingHour>(entity =>
        {
            entity.HasKey(e => e.HourId).HasName("PK__WorkingH__21EED6C8023DAA68");

            entity.Property(e => e.HourId).HasColumnName("hour_id");
            entity.Property(e => e.DayOfWeek)
                .HasMaxLength(50)
                .HasColumnName("day_of_week");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.ReaderId).HasColumnName("reader_id");
            entity.Property(e => e.StartTime).HasColumnName("start_time");

            entity.HasOne(d => d.Reader).WithMany(p => p.WorkingHours)
                .HasForeignKey(d => d.ReaderId)
                .HasConstraintName("FK__WorkingHo__reade__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
