using GymProject.Models;

namespace GymProject.Repository
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetExercises();
        Task<Exercise> GetExercise(int? id);
        Task CreateExercise(Exercise exercise);
        Task UpdateExercise(int id, Exercise exercise);
        Task DeleteExercise(int id);
    }
}
