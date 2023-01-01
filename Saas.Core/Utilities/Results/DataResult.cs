namespace Saas.Core.Utilities.Results
{
    public class DataResult<T> :Result, IDataResult<T>
    {
        // base ile result class ina success ve message gonderiyor.
        public DataResult(T data,bool success,string message) : base(success,message)
        {
            Data = data;
        }
        public DataResult(T data,bool success) : base(success)
        {
            Data = data;
        }
        public DataResult(string message) : base(message)
        {
            Message = message;
        }
        public T Data { get; }
    }
}
