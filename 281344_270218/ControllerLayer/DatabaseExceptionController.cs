namespace ControllerLayer
{
    public class DatabaseExceptionController : Exception
    {
        public DatabaseExceptionController(string mensaje) : base(mensaje) { }
    }
}
