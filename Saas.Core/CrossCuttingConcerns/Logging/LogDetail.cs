using System.Collections.Generic;

namespace Saas.Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string MethodName { get; set; }
        public List<LogParameter> LogParameters { get; set; }
        public string User { get; set; }


    }
}
