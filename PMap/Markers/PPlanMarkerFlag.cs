using System.Drawing;
using PMap.Properties;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace PMap.Markers
{
    using System.Drawing;
using PMap.DB;
    using PMap.BO;
    public class PPlanMarkerFlag : PPlanMarker
    {

        public float? Bearing;
        private Bitmap m_bmp;


        public PPlanMarkerFlag(PointLatLng p, boPlanTourPoint p_TourPoint)
            : base(p, Color.Transparent, p_TourPoint)
        {
            Init();

        }

        static readonly Point[] Arrow = new Point[] { new Point(-5, 5), new Point(0, -20), new Point(5, 5), new Point(0, 2) };

        public override void OnRender(Graphics g)
        {


            if (Bearing.HasValue)
            {
                g.TranslateTransform(ToolTipPosition.X, ToolTipPosition.Y);
                g.RotateTransform(Bearing.Value);

                g.FillPolygon(Brushes.Green, Arrow);

                g.ResetTransform();
            }

            g.DrawImageUnscaled(m_bmp, LocalPosition.X, LocalPosition.Y);
        }

        private void Init()
        {
            m_bmp = new Bitmap(Resources.Flag3);
            Size = m_bmp.Size;
            Offset = new System.Drawing.Point(0, -m_bmp.Height+1);
        }
    }
}
