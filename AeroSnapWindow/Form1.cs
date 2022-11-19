using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices; // for external libraries
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroSnapWindow
{
    public partial class Form1 : Form
    {
        //fields
        //переменная границ формы
        private int borderSize = 2;
        
        public Form1()
        {
            InitializeComponent();
            this.Padding = new Padding(borderSize); //border size
            this.BackColor = Color.FromArgb(98, 102, 244);//border color            
        }
        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd,
            int wMsg, int wParam, int lParam);

        //handler for dragging by title bar
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        { //calling external methods from libraries
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        //Removing standard window frame
        //https://learn.microsoft.com/ru-ru/windows/win32/winmsg/wm-nccalcsize
        //Overriden methods
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }

    }
}
