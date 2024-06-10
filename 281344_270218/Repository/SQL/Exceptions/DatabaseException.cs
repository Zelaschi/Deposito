

namespace Repository.SQL.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string mensaje) : base(mensaje) { }
    }
}
