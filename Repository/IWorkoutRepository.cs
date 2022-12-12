using GymProject.Models;

namespace GymProject.Repository
{
    public interface IWorkoutRepository
    {
        Task<IEnumerable<Workout>> GetWorkouts();
        Task<Workout> GetWorkout(int? id);

        Task<Workout> GetWorkoutByWorkoutId(Guid workoutId);

        Task<Guid> CreateWorkout(Workout workout);

    }
}
