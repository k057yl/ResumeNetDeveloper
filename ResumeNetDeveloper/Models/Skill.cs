namespace ResumeNetDeveloper.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string SkillName { get; set; }
        public string Description { get; set; }


        public ICollection<EmployerSkill> EmployerSkills { get; set; }
    }
}
