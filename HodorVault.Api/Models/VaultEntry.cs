namespace HodorVault.Api.Models
{
    public class VaultEntryJson
    {
        public ArtistJson Artist { get; set; }
        public AlbumJson Album { get; set; }
    }

    public class ArtistJson
    {
        public int id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
    }

    public class AlbumJson
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int year { get; set; }
    }
}
