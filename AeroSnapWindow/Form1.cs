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
        //Removing standard window frame and title bar
        //https://learn.microsoft.com/ru-ru/windows/win32/winmsg/wm-nccalcsize
        //Overriden methods
        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
              {
                return;
              };
            base.WndProc(ref m);
        }

        //обработчик (event method) изменения размеров формы с методом
        //подгонки отступов (padding) содержания формы к ее границе
        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }
        private void AdjustForm()
        {
            switch (this.WindowState)//в зависимости от состояния формы
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(8, 8, 8, 0); //int left,top,right,bottom
                    break;
                case FormWindowState.Normal:
                    if (this.Padding.Top != borderSize)
                        //окно в нормальнос сост. приводится к нач. значениямм = 2
                    this.Padding = new Padding(borderSize);
                    break;

            }
        }
    }
}
