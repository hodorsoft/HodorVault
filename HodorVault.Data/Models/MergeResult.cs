namespace HodorVault.Data.Models
{
    public class MergeResult
    {
        public MergeResult(string action, int id)
        {
            Inserted = new[] { action == "INSERT" ? id : 0 };
            Updated = new[] { action == "UPDATE" ? id : 0 };
            Error = action.StartsWith("ERROR") ? action : null;
        }

        /// <summary>
        /// Ids for rows that were inserted
        /// </summary>
        public int[] Inserted { get; set; }

        /// <summary>
        /// Ids for rows that were updated
        /// </summary>
        public int[] Updated { get; set; }

        public string Error { get; set; }
    }
}
