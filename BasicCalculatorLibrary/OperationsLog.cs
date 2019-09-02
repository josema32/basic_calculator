using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCalculatorLibrary
{
    public class OperationsLog
    {
        public string FinishedOperation { get; set; }

        public string DisplayText
        {
            
            get
            {
                return $"{ FinishedOperation }";
            }
        }
    }
}
