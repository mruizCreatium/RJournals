using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JournalModels
{
    public partial class Journal
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long ResearcherId { get; set; }

        [NotMapped]
        public Researcher Researcher { get; set; }

        public string Title { get; set; }

        public string FileUrl { get; set; }

        public DateTime PostDate { get; set; }

        public Journal SetResearcherInfo(Researcher researcher, string mediaServer)
        {
            researcher.MediaServer = mediaServer;
            this.Researcher = researcher;
            return this;
        }

    }
}
