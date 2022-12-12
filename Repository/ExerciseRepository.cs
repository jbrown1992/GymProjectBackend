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

        public async Task DeleteExercise(int id)
        {
            using (var connection = context.CreateConnection())
            {

                var sql = "delete from Exercise where Id = @Id";
                var exercises = await connection.QueryAsync<Exercise>(sql, new { Id = id });
            }
        }

        public async Task<Exercise> GetExercise(int? id)
        {
            using (var connection = context.CreateConnection())
            {
                var sql = "select * from Exercise where Id = @Id";
                var exercise = await connection.QueryFirstAsync<Exercise>(sql, new { Id = id });
                return exercise;
            }
        }

        public Task UpdateExercise(int id, Exercise exercise)
        {
            throw new NotImplementedException();
        }
    }
}
