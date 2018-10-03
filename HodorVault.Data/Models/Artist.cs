using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HodorVault.Data.Models
{   
    [Table("Artists")]
    public class Artist : DbItem
    {
        public string Name { get; set; }
        public string Country { get; set; }

        /// <summary>
        /// Discogs related data in JSON format
        /// </summary>
        public string DiscogsData { get; set; }

        public virtual IList<Album> Albums { get; set; }
    }
}
