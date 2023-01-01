namespace Saas.Core.Utilities.Results
{
    public class Result :IResult
    {
        //this ile cagrialn constracter diger tek parametre alan methodu da build eder
        protected Result(bool success,string message) : this(success)
        {
            Message = message;
            //Success=success;
        }

        protected Result(bool success)
        {
            Success = success;
        }

        protected Result(string message)
        {
            Message = message;
        }

        public bool Success { get; set; }

        public string Message { get; init; }
    }
}
