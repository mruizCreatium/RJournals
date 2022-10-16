using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JournalModels
{
    public class Researcher
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string ResearchArea { get; set; }

        public string Institution { get; set; }

        public string PictureUrl { get; set; }

        [NotMapped]
        public string MediaServer { get; set; } = "https://rjournalsapi.creatium.mx";

        [NotMapped]
        public Uri PictureFullUrl
        {
            get
            {
                return new Uri($"{MediaServer}/api/medias/{PictureUrl}");
            }
        }

        [NotMapped]
        public bool Subscribed { get; set; }

        public Researcher SetSubcriberFlag(bool subscribed, string mediaServer)
        {
            this.MediaServer = mediaServer;
            this.Subscribed = subscribed;
            return this;
        }
    }
}
