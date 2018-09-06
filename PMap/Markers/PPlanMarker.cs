using System.Drawing;
using PMapCore.Properties;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace PMapCore.Markers
{
      using System.Drawing;
    using PMapCore.DB;
    using GMap.NET.WindowsForms.Markers;
    using PMapCore.BO;
    public class PPlanMarker : GMapMarker
{


      public float? Bearing;
      public boPlanTourPoint TourPoint { get; private set; }

      private Color m_color;
      private Bitmap m_bmp;


      public PPlanMarker(PointLatLng p, Color p_color, boPlanTourPoint p_TourPoint)
          : base(p)
      {
          m_color = p_color;
          TourPoint = p_TourPoint;
          Init();
      }

      

     static readonly Point[] Arrow = new Point[] { new Point(-5, 5), new Point(0, -20), new Point(5, 5), new Point(0, 2) };

       public Color Color
       {
        get { return m_color;}
           set { m_color = value; 
               Init(); }
       }

      public override void OnRender(Graphics g)
      {

         //g.DrawImageUnscaled(Resources.shadow50, LocalPosition.X, LocalPosition.Y);

         if(Bearing.HasValue)
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
          m_bmp = new Bitmap(Resources.bullet_black);
          for (int x = 0; x < m_bmp.Width; x++)
          {
              for (int y = 0; y < m_bmp.Height; y++)
              {
                  //                  if (x == Resources.bullet_blue.Width / 2 && y == Resources.bullet_blue.Height / 2)
                  //                    System.Console.WriteLine(m_bmp.GetPixel(x, y).ToString());

                  if (m_bmp.GetPixel(x, y) == Color.FromArgb(255, 0, 0, 0))
                  {
                      m_bmp.SetPixel(x, y, m_color);
                  }
              }
          }


          Size = m_bmp.Size;
          Offset = new System.Drawing.Point(-m_bmp.Width / 2, -m_bmp.Height/2);
      }
   }
}
