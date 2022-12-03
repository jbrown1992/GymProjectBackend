using Dapper;
using GymProject.Models;

namespace GymProject.Repository
{
    public class ExerciseRepository : IExerciseRepository
    {

        private readonly DapperContext context;

        public ExerciseRepository(DapperContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Exercise>> GetExercises()
        {
            using (var connection = context.CreateConnection())
            {
                var sql = "select * from Exercise";
                var exercises = await connection.QueryAsync<Exercise>(sql);
                return exercises.ToList();
            }
        }

        public async Task CreateExercise(Exercise exercise)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync("insert into Exercise (name) values (@Name)", exercise);
            }
        }

        public Task DeleteExercise(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Exercise> GetExercise(int? id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateExercise(int id, Exercise exercise)
        {
            throw new NotImplementedException();
        }
    }
}
