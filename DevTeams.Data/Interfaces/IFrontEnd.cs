namespace DevTeams.Data.Interfaces
{
    public interface IFrontEnd
    {
        public List<string> FrontEndLanguages { get; set; }
        public List<string> FrontEndFrameworks { get; set; }
    }
}