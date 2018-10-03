using System.ComponentModel.DataAnnotations.Schema;

namespace HodorVault.Data.Models
{
    [Table("Albums")]
    public class Album : DbItem
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }

        public string Comment { get; set; }

        /// <summary>
        /// Discogs related data in JSON format
        /// </summary>
        public string DiscogsData { get; set; }
    }
}
