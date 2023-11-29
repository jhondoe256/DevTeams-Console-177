using DevTeams.Data;

namespace DevTeams.Repository
{
    public class DevRepository
    {
        //Fake database : Collection List<T>
        private readonly List<Developer> _devDbContext;
        private int _idCount;
        public DevRepository()
        {
            _devDbContext = new List<Developer>();
            Seed();
        }

        private void Seed()
        {
            Developer daymon = new FrontEndDeveloper("Daymon", "Wayans", false);
            Developer george = new BackendDeveloper("George", "Carlin", true);
            Developer burr = new FullStackDeveloper("Bill", "Burr", false);

            AddDeveloper(daymon);
            AddDeveloper(george);
            AddDeveloper(burr);
        }

        //C.R.U.D
        public bool AddDeveloper(Developer developer)
        {
            if (developer is null)
            {
                return false;
            }
            else
            {

                return AddToDatabase(developer);
            }

            // return (developer is null)? false : AddToDatabase(developer);
        }

        //helper method
        private bool AddToDatabase(Developer developer)
        {
            IncrementId(developer);
            _devDbContext.Add(developer);
            return true;
        }

        //helper method
        private void IncrementId(Developer developer)
        {
            _idCount++;
            developer.Id = _idCount;
        }

        public List<Developer> GetDevelopers()
        {
            return _devDbContext;
        }

        public Developer GetDeveloper(int devId)
        {
            return _devDbContext.SingleOrDefault(d => d.Id == devId)!;
        }

        //update
        public bool UpdateDeveloper(int devId, Developer newDevData)
        {
            Developer devInDatabase = GetDeveloper(devId);
            //def coding
            //dev is in the data base...
            if (devInDatabase != null)
            {
                devInDatabase.FirstName = newDevData.FirstName;
                devInDatabase.LastName = newDevData.LastName;
                devInDatabase.HasPluralsight = newDevData.HasPluralsight;
                return true;
            }
            return false;
        }

        //delete
        public bool DeleteDeveloper(int devId)
        {
            Developer devInDatabase = GetDeveloper(devId);
            return _devDbContext.Remove(devInDatabase);
        }

        //delete
        public bool DeleteDeveloper(Developer dev)
        {
            return _devDbContext.Remove(dev);
        }

        //* Challenge -> list of devs w/o Ps
        public List<Developer> GetDevelopersWithoutPs()
        {
            //start w/ empty list
            List<Developer> developersWoPs = new List<Developer>();

            //loop through database
            foreach (Developer dev in _devDbContext)
            {
                if (dev.HasPluralsight is false)
                {
                    //add to the empty list 
                    developersWoPs.Add(dev);
                }
            }
            return developersWoPs;
        }
    }
}