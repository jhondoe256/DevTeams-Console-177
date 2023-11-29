using DevTeams.Data;

namespace DevTeams.Repository
{
    public class DevJobRepository
    {
        private readonly DevRepository _devRepo;
        private readonly List<DevJob> _devJobRepo = new List<DevJob>();

        public DevJobRepository(DevRepository devRepo)
        {
            _devRepo = devRepo;
        } 
    }
}