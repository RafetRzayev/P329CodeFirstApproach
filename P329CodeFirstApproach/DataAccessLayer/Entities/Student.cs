using System.ComponentModel.DataAnnotations.Schema;

namespace P329CodeFirstApproach.DataAccessLayer.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
