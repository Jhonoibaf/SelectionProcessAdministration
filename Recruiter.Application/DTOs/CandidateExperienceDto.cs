namespace Recruiters.Application.DTOs
{
    public class CandidateExperienceDto
    {
        public string Company { get; set; }
        public string Job { get; set; }
        public decimal Salary { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
