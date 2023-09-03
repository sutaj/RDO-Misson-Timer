using RDO_mission_timer.Properties;
using System.Drawing;

namespace RDO_mission_timer
{
    internal class bmp
    {
        Bitmap _bmp;

        public bmp()
        {
            _bmp = new Bitmap(Resources.selection_boxes);
        }

        internal Bitmap chkBxEmpty()
        {
            Rectangle r = new Rectangle(0, 0, 50, _bmp.Height);
            Bitmap bmp = _bmp.Clone(r, _bmp.PixelFormat);
            return bmp;
        }

        internal Bitmap chkBxFill()
        {
            Rectangle r = new Rectangle(52, 0, 50, _bmp.Height);
            Bitmap bmp = _bmp.Clone(r, _bmp.PixelFormat);
            return bmp;
        }
    }
}
