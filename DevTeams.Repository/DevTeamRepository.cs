using DevTeams.Data;

namespace DevTeams.Repository
{
    public class DevTeamRepository
    {
        //globally scoped 'private variable' 
        //private 'backing field'
        private DevRepository _devRepo;

        //create a dependency
        public DevTeamRepository(DevRepository devRepo)
        {
            _devRepo = devRepo;
            Seed();
        }

        private readonly List<DevTeam> _devTeamDb = new List<DevTeam>();
        
        private int _count = 0;

        //C.R.U.D

        public bool AddDevTeam(DevTeam devTeam)
        {
            if (devTeam is null)
            {
                return false;
            }
            else
            {
                _count++;
                devTeam.Id = _count;
                _devTeamDb.Add(devTeam);
                return true;
            }
        }

        public List<DevTeam> GetDevTeams()
        {
            return _devTeamDb;
        }

        public DevTeam GetDevTeam(int devTeamId)
        {
            return _devTeamDb.FirstOrDefault(dT => dT.Id == devTeamId)!;
        }

        public bool UpdateDevTeam(int devTeamId, DevTeam newDevTeamData)
        {
            DevTeam teamDataInDb = GetDevTeam(devTeamId);

            if (teamDataInDb is not null)
            {
                teamDataInDb.Name = newDevTeamData.Name;
                if (newDevTeamData.DevsOnTeam.Count() > 0)
                {
                    teamDataInDb.DevsOnTeam = newDevTeamData.DevsOnTeam;
                }
                return true;
            }

            return false;
        }

        public bool DeleteDevTeam(int devTeamId)
        {
            DevTeam teamDataInDb = GetDevTeam(devTeamId);
            return _devTeamDb.Remove(teamDataInDb);
        }

        // Add Developer to DevTeam....
        public bool AddDevToTeam(int devId, int teamId)
        {
            //todo... we can't access the developer stuff from here!!!!!
            Developer dev = _devRepo.GetDeveloper(devId);
            if(dev != null)
            {
                DevTeam team = GetDevTeam(teamId);
                if(team != null)
                {
                    //adding goes here!
                    team.DevsOnTeam.Add(dev);
                    return true;
                }
            }
            return false;
        }

        //Add multi devs 
        public bool AddMulitDevs(int teamId, List<int> devIds)
        {
            var team =GetDevTeam(teamId);
            if(team != null)
            {
                foreach (var id in devIds)
                {
                    Developer dev = _devRepo.GetDeveloper(id);
                    if(dev is not null)
                    {
                        team.DevsOnTeam.Add(dev);
                    }
                }
                return true;
            }
            return false;
        }
    
        //seed
        private void Seed()
        {
            Developer dev1 = _devRepo.GetDeveloper(1);
            Developer dev2 = _devRepo.GetDeveloper(2);

            DevTeam team = new DevTeam("Fire",new List<Developer>{dev1,dev2});
            
            //Adding to the database via the 'AddDevTeam' method
            AddDevTeam(team);
        }
    }
}