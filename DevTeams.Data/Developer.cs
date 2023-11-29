namespace DevTeams.Data
{
    public abstract class Developer
    {
        public Developer(){}

        public Developer(string firstName, string lastName, bool HasPluralsight)
        {
            FirstName = firstName;
            LastName = lastName;
            this.HasPluralsight = HasPluralsight;
        }

        //Unique identifier
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName 
        { 
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public bool HasPluralsight {get;set;}

        public override string ToString()
        {
            string str = $"Id: {Id}\n"+
                         $"Name: {FullName}\n"+
                         $"Has PluralSight: {HasPluralsight}\n"+
                         "======================================\n";

            return  str;
        }

    }
}