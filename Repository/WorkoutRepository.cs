using Dapper;
using GymProject.Models;
using System.Collections.Specialized;

namespace GymProject.Repository
{
    public class WorkoutRepository : IWorkoutRepository
    {

        private readonly DapperContext context;

        public WorkoutRepository(DapperContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Workout>> GetWorkouts()
        {
            using (var connection = context.CreateConnection())
            {
                var sql = "select * from Workout";
                var Workouts = await connection.QueryAsync<Workout>(sql);
                return Workouts.ToList();
            }
        }

        public async Task<Guid> CreateWorkout(Workout workout)
        {
            workout.Id = Guid.NewGuid(); ;
            //TODO should this be inside the 'using'
            workout.Exercises.ForEach(exercise =>
            {
                exercise.Sets.ForEach(async set =>
                {
                    using (var connection = context.CreateConnection())
                    {
                        var date = workout.DateTime.ToString();
                        var sql = $"insert into Workout (WorkoutId, Name, ExerciseName, Reps, KG, DateTime) " +
                            $"values ('{workout.Id}', '{workout.Name}', '{exercise.ExerciseName}', '{set.Reps}', '{set.Kg}', '{date}')";

                        await connection.ExecuteAsync(sql);
                    }
                });
                


            });

            return workout.Id;
            

       
        }

        public Task DeleteWorkout(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Workout> GetWorkout(int? id)
        {
            using (var connection = context.CreateConnection())
            {
                var sql = "select * from Workout where Id = @Id";
                var Workout = await connection.QueryFirstAsync<Workout>(sql, new { Id = id });
                return Workout;
            }
        }

        public Task UpdateWorkout(int id, Workout workout)
        {
            throw new NotImplementedException();
        }

        public async Task<Workout> GetWorkoutByWorkoutId(Guid workoutId)
        {
            //57a475a0-8253-4903-ba97-224ad1fba1f2
            using (var connection = context.CreateConnection())
            {
                var sql = "select * from Workout where workoutId = @WorkoutId";
                var Workouts = await connection.QueryAsync(sql, new { WorkoutId = workoutId });

                var key = Workouts.Select(c => ((IDictionary<string, object>)c).Keys).ToList()[0];
                var valuesList = Workouts.Select(c => ((IDictionary<string, object>)c).Values).ToList();

                var dicList = new List<Dictionary<string, object>>();

                foreach (var values in valuesList)
                {
                    var dic = key.Zip(values, (k, v) => new { k, v })
                                .ToDictionary(x => x.k, x => x.v);

                    dicList.Add(dic);

                }

                ////get all exerciseNames from dic
                var exerciseNameSet = new HashSet<string> { };



                dicList.ForEach(x => {
                    string? item = x["ExerciseName"].ToString();
                    exerciseNameSet.Add(item);
                });
                Workout workout = new Workout();
                workout.Id = workoutId;
                workout.Name = dicList[0]["Name"].ToString();
                workout.DateTime = DateTime.Parse(dicList[0]["DateTime"].ToString());
                workout.Exercises = new List<ExerciseSet> { };

                //loop through and create exercises
                foreach (var exerciseName in exerciseNameSet)
                {
                    ExerciseSet exerciseSet = new ExerciseSet();
                    exerciseSet.ExerciseName = exerciseName;
                    exerciseSet.Sets = new List<Set> { };

                    var currentExercises = dicList.FindAll(s => s.Values.Contains(exerciseName));
                    
                    foreach (var currentExercise in currentExercises)
                    {
                        var kg = currentExercise["KG"].ToString();
                        var reps = currentExercise["Reps"].ToString();
                        exerciseSet.Sets.Add(new Set { Kg =kg, Reps = reps });

                    }

                    workout.Exercises.Add(exerciseSet);
                }

                

                return workout;
            }
        }
    }
}
