namespace DevTeams.Data
{
    public class DevJob
    {
        private double _difficultyRating;
        public int Id { get; set; }
        public string JobDescription { get; set; } = string.Empty;
        private const double MAX_DIFFICULTY = 5;

        public double DifficultyRating 
        { 
            get
            {
                return _difficultyRating;
            } 
            set
            {
                if(_difficultyRating < MAX_DIFFICULTY)
                {
                    _difficultyRating = value;
                }
                _difficultyRating = 0;
            }
        }
    }
}