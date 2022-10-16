using JournalModels;
using Microsoft.EntityFrameworkCore;

namespace JournalsApi.Models
{
    public class JournalContext : DbContext
    {
        public JournalContext(DbContextOptions<JournalContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>()
                .HasKey(s => new { s.SubscriberId, s.PublisherId });

            Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Researcher>().HasData(
                new Researcher
                {
                    Id = 1,
                    Institution = "CREATIUM Research Intitute",
                    Name = "Manuel Ruiz",
                    ResearchArea = "Computer Science",
                    PictureUrl = "researchers/picture_1.jpg"
                },
                new Researcher
                {
                    Id = 2,
                    Institution = "Institut d'Optique (Paris-Saclay University)",
                    Name = "PhD Alain Aspect",
                    ResearchArea = "Quantum Theory",
                    PictureUrl = "researchers/picture_2.jpg"
                },
                new Researcher
                {
                    Id = 3,
                    Institution = "The Jane Goodall Institute",
                    Name = "Jane Goodall",
                    ResearchArea = "Primatology",
                    PictureUrl = "researchers/picture_3.jpg"
                });

            modelBuilder.Entity<Journal>().HasData(new Journal
            {
                Id = 1,
                FileUrl = "journals/post_0.pdf",
                PostDate = DateTime.Now.AddDays(-10),
                ResearcherId = 1,
                Title = "Security in the cloud."
            },
            new Journal
            {
                Id = 2,
                FileUrl = "journals/post_1.pdf",
                PostDate = DateTime.Now.AddDays(-10),
                ResearcherId = 2,
                Title = "How rare events bring atoms to rest."
            },
            new Journal
            {
                Id = 3,
                FileUrl = "journals/post_2.pdf",
                PostDate = DateTime.Now.AddDays(-31),
                ResearcherId = 2,
                Title = "Speakable and unspeakable in quantum mechanics."
            },
            new Journal
            {
                Id = 4,
                FileUrl = "journals/post_3.pdf",
                PostDate = DateTime.Now.AddDays(-71),
                ResearcherId = 2,
                Title = "Introduction to quantum optics."
            },
            new Journal
            {
                Id = 5,
                FileUrl = "journals/post_4.pdf",
                PostDate = DateTime.Now.AddDays(-41),
                ResearcherId = 3,
                Title = "My Friends the Wild Chimpanzees"
            },
            new Journal
            {
                Id = 6,
                FileUrl = "journals/post_5.pdf",
                PostDate = DateTime.Now.AddDays(-91),
                ResearcherId = 3,
                Title = "The Chimpanzees of Gombe: Patterns of Behavior"
            });

            var s = modelBuilder.Entity<Subscription>().HasData(new Subscription
            {
                PublisherId = 2,
                SubscriberId = 1
            });

        }

        public DbSet<Journal> Journals { get; set; } = null!;

        public DbSet<Researcher> Researchers { get; set; } = null!;

        public DbSet<Subscription> Subscriptions { get; set; } = null!;
    }
}