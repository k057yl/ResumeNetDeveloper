namespace ResumeNetDeveloper.Models
{
    public class EmployerSkill
    {
        public int Id { get; set; }
        public int EmployerId { get; set; }
        public int SkillId { get; set; }


        public Employer Employer { get; set; }
        public Skill Skill { get; set; }
    }
}
