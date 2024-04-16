namespace SuperHeroAPI.Models.ViewModel
{
    public class PersonVM
    {
        public int Code { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string IdNumber { get; set; } = null!;
    }
}
