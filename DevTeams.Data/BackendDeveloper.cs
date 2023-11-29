using DevTeams.Data.Interfaces;

namespace DevTeams.Data
{
    public class BackendDeveloper : Developer , IBackEnd
    {
        public BackendDeveloper()
        {
            
        }
        public BackendDeveloper(string firstName, string lastName, bool hasPluralsight)
        :base(firstName,lastName,hasPluralsight)
        {
            
        }
        public List<string> BackEndLanguages { get; set; } = new List<string>();
        public List<string> BackEndFrameworks { get; set; } = new List<string>();
    }
}