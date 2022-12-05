using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Tool.IOC0640
{
    public class IO
    {
        public int Index { get; set; }

        public string IN { get; set; }

        public string OUT { get; set; }
    }

    public  class Config
    {
        public string Name { get; set; }

        public BindingList<IO> IOInfo { get; set; }
    }
}
