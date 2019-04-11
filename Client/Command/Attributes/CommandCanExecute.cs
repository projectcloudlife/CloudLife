using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Command.Attributes
{
    public class CommandCanExecute : Attribute
    {
        public CommandCanExecute() {}
        public CommandCanExecute(string method)
        {
            Method = method;
        }
        public string Method { get; set; }
    }
}
