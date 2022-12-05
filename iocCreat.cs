using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Tool.Interface;
using Lead.Tool.Resources;

namespace Lead.Tool.IOC0640
{
    public class IOCCreat : ICreat
    {
        public ITool GetInstance(string Name, string Path)
        {
            return new IOCTool(Name, Path);
        }

        public System.Drawing.Image Icon
        {
            get
            {
                return (Image)ImageManager.GetImage("IOC0640");
            }
        }

        public string Name
        {
            get
            {
                return "IOC0640";
            }
        }
    }
}
