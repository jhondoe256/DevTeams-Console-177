using DevTeams.Data.Interfaces;

namespace DevTeams.Data
{
    public class FullStackDeveloper : Developer, IFrontEnd, IBackEnd
    {
        public FullStackDeveloper() { }

        public FullStackDeveloper(string firstName, string lastName, bool hasPluralsight)
       : base(firstName, lastName, hasPluralsight)
        {

        }
        public List<string> BackEndLanguages { get; set; } = new List<string>();
        public List<string> BackEndFrameworks { get; set; } = new List<string>();

        public List<string> FrontEndLanguages { get; set; } = new List<string>();
        public List<string> FrontEndFrameworks { get; set; } = new List<string>();
    }
}