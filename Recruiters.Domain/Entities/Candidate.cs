namespace Recruiters.Domain.Entities
{
    public class Candidate
    {
        public int IdCandidate { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public ICollection<CandidateExperience> CandidateExperiences { get; set; } = null!;

        public void Validate()
        {
            if (CalculateAge(Birthdate) < 18)
            {
                throw new InvalidOperationException("Candidate must be at least 18 years old.");
            }
        }

        private int CalculateAge(DateTime birthdate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;
            if (birthdate.Date > today.AddYears(-age)) age--;
            return age;
        }

    }
}
