namespace DevTeams.Data
{
    public class DevTeam
    {
        public DevTeam(){}
        
        public DevTeam(string name, List<Developer> devsOnTeam)
        {
            Name = name;
            DevsOnTeam = devsOnTeam;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Developer> DevsOnTeam { get; set; } = new List<Developer>();

        public override string ToString()
        {
            string str = $"Id: {Id}\n"+
                         $"Name: {Name}\n"+
                         "============================\n";

            if(DevsOnTeam.Count() > 0)
            {
                foreach (Developer dev in this.DevsOnTeam)
                {
                    str += $"{dev}\n"+
                           "=========================\n";
                }
            }
            else
            {
                str += "Sorry No Devs Available.";
            }

            return str;
        }
    }
}