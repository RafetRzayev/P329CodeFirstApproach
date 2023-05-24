namespace P329CodeFirstApproach.DataAccessLayer.Entities
{
    public class SocialNetwork
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FooterId { get; set; }
        public Footer Footer { get; set; }
    }
}
