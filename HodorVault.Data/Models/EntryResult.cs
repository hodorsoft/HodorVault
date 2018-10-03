namespace HodorVault.Data.Models
{
    public class EntryResult
    {
        /// <summary>
        /// Artists that were inserted or updated
        /// </summary>
        public MergeResult Artists { get; set; }

        /// <summary>
        /// Albums that were inserted or updated
        /// </summary>
        public MergeResult Albums { get; set; }
    }
}
