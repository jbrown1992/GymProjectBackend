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
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseRepository repository;

        public ExerciseController(IExerciseRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Exercise>>> GetExercises()
        {
            return Ok(await repository.GetExercises());
        }

        [HttpPost]
        public async Task<ActionResult<List<Exercise>>> CreateExercise(Exercise exercise)
        {
            await repository.CreateExercise(exercise);
            return Ok();
        }
    }
}