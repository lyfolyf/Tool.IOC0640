using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lead.Tool.IOC0640
{
    public partial class DebugUI : UserControl
    {
        IOCTool _Proxy = null;
        public DebugUI(IOCTool proxy)
        {
            InitializeComponent();
            _Proxy = proxy;
        }
        private Button GetButton(GroupBox groupBox, int index)
        {

            foreach (Button _button in groupBox.Controls.OfType<Button>())
            {
                if (Convert.ToInt32(_button.Text.Trim()) == index)
                {
                    return _button;
                }
            }
            return null;
        }

        private void SetButton(Button _button, bool status)
        {
            if (_button != null)
            {
                if (status)
                {
                    _button.BackColor = Color.White;
                }
                else
                {
                    _button.BackColor = Color.Green;
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // IO - IN
            for (int i = 0; i < _Proxy._Config.IOInfo.Count; i++)
            {
                var value = _Proxy.ReadIn(_Proxy._Config.IOInfo[i].IN);
                Button _button = GetButton(groupBoxIn, i);
                if (_button != null)
                {
                    SetButton(_button, value);
                }
            }

            //IO - OUT
            for (int i = 0; i < _Proxy._Config.IOInfo.Count; i++)
            {
                var value = _Proxy.ReadOut(_Proxy._Config.IOInfo[i].OUT);
                Button _button = GetButton(groupBoxOut, i);
                if (_button != null)
                {
                    SetButton(_button, value);
                }
            }
        }
    }
}
