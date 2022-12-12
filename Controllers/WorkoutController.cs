using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GymProject.Models;
using GymProject.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;

namespace GymProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutRepository repository;

        public WorkoutController(IWorkoutRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Workout>>> GetWorkouts()
        {
            return Ok(await repository.GetWorkouts());
        }

        [HttpGet("{workoutId}")]
        public async Task<ActionResult<Workout>> GetWorkoutByWorkoutId(Guid workoutId)
        {
            return Ok(await repository.GetWorkoutByWorkoutId(workoutId));
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<List<Workout>>> GetWorkout(int id)
        //{
        //    return Ok(await repository.GetWorkout(id));
        //}

        [HttpPost]
        public async Task<ActionResult<List<Workout>>> CreateWorkout(Workout Workout)
        {
            return Ok(await repository.CreateWorkout(Workout));
        }
    }
}