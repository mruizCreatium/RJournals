using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JournalModels
{
    public class Subscription
    {
        [Key]
        [Column(Order = 1)]
        public long SubscriberId { get; set; }

        [Key]
        [Column(Order = 2)]
        public long PublisherId { get; set; }
    }
}
