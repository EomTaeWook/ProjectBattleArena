using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BA.Models
{
    public class LogModel
    {
        public string Account { get; set; }

        public long LoggedTime { get; set; }

        public string Log { get; set; }

        public string Path { get; set; }
    }
}
