using System.Data;

namespace GymProject.Repository
{
    public interface IDbContext
    {

        IDbConnection CreateConnection();


    }
}
