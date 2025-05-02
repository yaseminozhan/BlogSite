namespace BlogSite.UI.Entities
{
    public class TakipEdilenYazar
    {
        public int Id { get; set; }
        public string TakipEdenId { get; set; } = null!;
        public string TakipEdilenId { get; set; } = null!;
        public ApplicationUser? TakipEden { get; set; }
        public ApplicationUser? TakipEdilen { get; set; }
    }
}
