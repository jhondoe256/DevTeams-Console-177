using DevTeams.Data.Interfaces;

namespace DevTeams.Data
{
    public class FrontEndDeveloper : Developer, IFrontEnd
    {
        public FrontEndDeveloper(){}
        public FrontEndDeveloper(string firstName, string lastName, bool hasPluralsight)
        :base(firstName,lastName,hasPluralsight)
        {
            
        }
        public List<string> FrontEndLanguages { get; set;} = new List<string>();
        public List<string> FrontEndFrameworks { get;set; } = new List<string>();

    }
}