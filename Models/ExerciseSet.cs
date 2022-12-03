namespace GymProject.Models
{
    public class ExerciseSet
    {
        public string Id { get; set; }
        public string ExerciseId { get; set; }
        public List<int> SetsAndReps { get; set; }
    }
}
