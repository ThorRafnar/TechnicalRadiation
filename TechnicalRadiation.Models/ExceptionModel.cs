using System.Collections.Generic;
using Newtonsoft.Json;

namespace TechnicalRadiation.Models
{
    public class ExceptionModel
    {
        public int StatusCode { get; set; }
        public string ExceptionMessage { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}