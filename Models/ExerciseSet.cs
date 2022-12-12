namespace GymProject.Models
{
    public class ExerciseSet
    {
        public string ExerciseName { get; set; }
        public List<Set> Sets { get; set; }
    }

    public class Set
    {
        public string Reps { get; set; }
        public string Kg { get; set; }
    }
}
