namespace GymProject.Models
{
    public class Workout
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<ExerciseSet> Exercises { get; set; }
    }
}
