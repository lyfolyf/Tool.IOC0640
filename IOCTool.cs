using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lead.Tool.Interface;
using Lead.Tool.XML;

namespace Lead.Tool.IOC0640
{

    public class IOCTool : ITool
    {
        public Config _Config = null;

        private ConfigUI _ConfigControl = null;
        private DebugUI _DebugControl = null;
        private IToolState _State = IToolState.ToolMin;
        public string _ConfigPath = "";

        private IOCTool()
        {

        }

        public IOCTool(string Name, string Path)
        {
            _ConfigPath = Path;
            if (File.Exists(Path))
            {
                _Config = (Config)XmlSerializerHelper.ReadXML(Path, typeof(Config));
            }
            else
            {
                _Config = new Config();
            }
            _ConfigControl = new ConfigUI(this);
            _DebugControl = new DebugUI(this);
        }

        public Control ConfigUI
        {
            get
            {
                return _ConfigControl;
            }
        }

        public Control DebugUI
        {
            get
            {
                return _DebugControl;
            }
        }

        public IToolState State
        {
            get
            {
                return _State;
            }
        }

        bool ioCardInited = false;
        public void Init()
        {
            if (ioCardInited)
            {
                return ;
            }

            int ret = IOC0640.ioc_board_init();
            if (ret == 1)
            {
                ioCardInited = true;

                return ;
            }
            
            throw new Exception("IO卡初始化失败") ;
        }

        public void Start()
        {
            return;
        }

        public void Terminate()
        {
            return;
        }

        public void WriteOut(string ID,bool value)
        {
            int index = 0;
            bool IsFinded = false;
            for (; index<_Config.IOInfo.Count; index++)
            {
                if (ID == _Config.IOInfo[index].OUT)
                {
                    IsFinded = true;
                    break;
                }
            }

            if (IsFinded)
            {
                var Va = value ? 0 : 1;
                IOC0640.ioc_write_outbit(0, (ushort)index, Va);
            }
            else
            {
                throw new Exception("未找到:" + ID);
            }
             
        }

        public bool ReadIn(string ID)
        {
            int index = 0;
            bool IsFinded = false;
            for (; index < _Config.IOInfo.Count; index++)
            {
                if (ID == _Config.IOInfo[index].IN)
                {
                    IsFinded = true;
                    break;
                }
            }

            if (IsFinded)
            {
                var Va =  IOC0640.ioc_read_inbit(0, (ushort)index);

                if (Va == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                throw new Exception("未找到:" + ID);
            }

        }

        public bool ReadOut(string ID)
        {
            int index = 0;
            bool IsFinded = false;
            for (; index < _Config.IOInfo.Count; index++)
            {
                if (ID == _Config.IOInfo[index].OUT)
                {
                    IsFinded = true;
                    break;
                }
            }

            if (IsFinded)
            {
                var Va = IOC0640.ioc_read_outbit(0, (ushort)index);

                if (Va == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                throw new Exception("未找到:" + ID);
            }

        }
    }
}
