using System.ComponentModel.DataAnnotations;

namespace SelectionProcessAdministration.Models.ViewModels
{
    public class CanditateViewModel
    {
        public int IdCandidate { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime Birthdate { get; set; }
        public string Email { get; set; } = null!;
    }
}
