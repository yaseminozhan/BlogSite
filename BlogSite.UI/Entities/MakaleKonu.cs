namespace BlogSite.UI.Entities
{
    public class MakaleKonu
    {
        //many-to-many için join table
        public int MakaleId { get; set; }
        public Makale? Makale { get; set; }

        public int KonuId { get; set; }
        public Konu? Konu { get; set; }
    }

}
