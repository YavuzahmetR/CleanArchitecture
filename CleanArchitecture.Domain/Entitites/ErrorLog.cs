using CleanArchitecture.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entitites
{
    public sealed class ErrorLog : Entity
    {
        public string RequestPath { get; set; } = null!;
        public string RequestMethod { get; set; } = null!;
        public string ErrorMessage { get; set; } = null!;
        public string StackTrace { get; set; } = null!;
        public DateTime TimeStamp { get; set; } 
    }
}
