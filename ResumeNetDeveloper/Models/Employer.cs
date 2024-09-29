namespace ResumeNetDeveloper.Models
{
    public class Employer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }


        public ICollection<EmployerSkill> EmployerSkills { get; set; }
    }
}
