using Microsoft.Data.SqlClient;
using PJ.Example.Abstractions.Exceptions;
using System.Net;

namespace PJ.Example.Repository
{
    public abstract class BaseRepository
    {
        protected BaseRepository()
        {
        }

        protected static Exception HandleSqlException(SqlException exception)
        {
            return exception.Number switch
            {
                50000 => new ApiException(HttpStatusCode.NotFound, exception.Message),
                _ => exception
            };
        }
    }
}