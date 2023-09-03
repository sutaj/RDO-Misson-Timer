using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace RDO_mission_timer
{
    public partial class frmMain : Form
    {
        Stopwatch ticker;
        Timer timer;
        bool working = false, b;
        bmp chkBoxCls = new bmp();
        Image _c, _u;
        Font _normal, _strike, _bold;
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Beep(uint dwFreq, uint dwDuration);

        public frmMain()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Tick += Tick;
            timer.Interval = 450;
            ticker = new Stopwatch();

            int HKEY_ID = 1;
            int HKEY_CODE = (int)Keys.NumLock;
            Boolean HKEYREGISTER = RegisterHotKey(
                this.Handle, HKEY_ID, 0x0000, HKEY_CODE
            );

            _c = chkBoxCls.chkBxFill();
            _u = chkBoxCls.chkBxEmpty();
            _normal = new Font(lblGoal1.Font, FontStyle.Regular);
            _bold = new Font(lblGoal2.Font, FontStyle.Bold);
            _strike = new Font(lblGoal1.Font, FontStyle.Strikeout | FontStyle.Bold);
            Reset();
        }


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();
                if (id == 1)
                {
                    Clicked();
                }
            }

            base.WndProc(ref m);
        }

        void sound(uint freq = 2100, uint duration = 100)
        {
            Beep(freq, duration);
        }

        private void Tick(object sender, EventArgs e)
        {
            if (working)
            {
                lblTime.ForeColor = Color.DarkRed;
                lblTime.Text = $"{ticker.Elapsed.Minutes:00} : {ticker.Elapsed.Seconds:00}";
                
                // up to 1 hour
                if(ticker.Elapsed.Minutes >= 59 && ticker.Elapsed.Seconds >= 59 || ticker.Elapsed.Hours > 0)
                {
                    ticker.Stop();
                    Reset();
                }

                switch (ticker.Elapsed.Minutes)
                {
                    case 3:
                        imgChk1.Image = _c;
                        lblGoal1.Font = _bold;
                        if (ticker.Elapsed.Seconds < 1 && ticker.Elapsed.Milliseconds < 500)
                            sound();
                        break;
                    case 6:
                        imgChk2.Image = _c;
                        lblGoal2.Font = _bold;
                        lblGoal1.Font = _strike;
                        lblGoal1.Enabled = false;
                        if (ticker.Elapsed.Seconds < 1 && ticker.Elapsed.Milliseconds < 500)
                            sound();
                        break;
                    case 9:
                        imgChk3.Image = _c;
                        lblGoal3.Font = _bold;
                        lblGoal2.Font = _strike;
                        lblGoal2.Enabled = false;
                        if (ticker.Elapsed.Seconds < 1 && ticker.Elapsed.Milliseconds < 500)
                            sound();
                        break;
                    case 12:
                        imgChk4.Image = _c;
                        lblGoal4.Font = _bold;
                        lblGoal3.Font = _strike;
                        lblGoal3.Enabled = false;
                        if (ticker.Elapsed.Seconds < 1 && ticker.Elapsed.Milliseconds < 500)
                            sound();
                        break;
                    case 15:
                        imgChk5.Image = _c;
                        lblGoal5.Font = _bold;
                        lblGoal4.Font = _strike;
                        lblGoal4.Enabled = false;
                        if (ticker.Elapsed.Seconds < 1 && ticker.Elapsed.Milliseconds < 500)
                            sound();
                        break;
                    case 20:
                        imgChk6.Image = _c;
                        lblGoal6.Font = _bold;
                        lblGoal5.Font = _strike;
                        lblGoal5.Enabled = false;
                        if (ticker.Elapsed.Seconds < 1 && ticker.Elapsed.Milliseconds < 500)
                            sound();
                        break;
                    case 25:
                        imgChk7.Image = _c;
                        lblGoal7.Font = _bold;
                        lblGoal6.Font = _strike;
                        lblGoal6.Enabled = false;
                        if (ticker.Elapsed.Seconds < 1 && ticker.Elapsed.Milliseconds < 500)
                            sound();
                        break;
                    case 30:
                        imgChk8.Image = _c;
                        lblGoal8.ForeColor = Color.DarkRed;
                        lblGoal8.Font = _bold;
                        lblGoal7.Font = _strike;
                        lblGoal7.Enabled = false;
                        if (ticker.Elapsed.Seconds < 1 && ticker.Elapsed.Milliseconds < 500)
                            sound(1000, 750);
                        break;
                    default:
                        break;
                }
            }
        }

        private void Reset()
        {
            timer.Stop();
            timer.Start();
            lblGoal8.ForeColor = SystemColors.ControlText;
            lblTime.ForeColor = SystemColors.ControlText;

            // quick fill ALL! pictureboxes
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                item.Image = _u;
            }
            
            foreach (Label item in Controls.OfType<Label>())
            {
                item.Enabled = true;
                if (item.Name != "lblTime")
                    item.Font = _normal;
            }
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            Clicked();
        }

        private void Clicked()
        {
            working = !working;

            if (working)
            {
                btnAction.Text = "Stop timer";
                ticker.Reset();
                Reset();
                ticker.Start();
            }
            else
            {
                btnAction.Text = "Start timer\r\n[Numlock]";
                ticker.Stop();
                lblTime.ForeColor = SystemColors.ControlText;
                working = false;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (!isAdmin())
            {
                //MessageBox.Show("Application is not running as administrator. \r\nIf hotkey is not working, try to run application as administrator.");
            }
            else
            {
                this.Text += " RUNNING AS ADMIN";
            }
        }

        public static bool isAdmin()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
