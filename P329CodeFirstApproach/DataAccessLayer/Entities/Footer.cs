namespace P329CodeFirstApproach.DataAccessLayer.Entities
{
    public class Footer
    {
        public int Id { get; set; } 
        public string CreatedBy { get; set; }
        public string CardImageUrl { get; set; }

        public ICollection<SocialNetwork> SocialNetworks { get; set; }
    }
}
