namespace WPF_Starter.Services.Exceptions
{
    public abstract class SqlCustomException : Exception
    {
        public int SqlErrorCode { get; }

        protected SqlCustomException(string message, int sqlErrorCode) : base(message)
        {
            SqlErrorCode = sqlErrorCode;
        }
    }

    public class DataBaseNotFoundException : SqlCustomException
    {
        public DataBaseNotFoundException(string message, int sqlErrorCode) : base(message, sqlErrorCode)
        {
        }
    }

    public class TableNotFoundException : SqlCustomException
    {
        public TableNotFoundException(string message, int sqlErrorCode) : base(message, sqlErrorCode)
        {
        }
    }

    public class SqlServerConnectionException : SqlCustomException
    {
        public SqlServerConnectionException(string message, int sqlErrorCode) : base(message, sqlErrorCode)
        {
        }
    }

}
