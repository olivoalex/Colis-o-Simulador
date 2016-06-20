using System.Windows.Media;
using System.Windows.Shapes;
using Colisao.Core;

namespace ColisaoWPF
{
    internal class BolaWPF : Bola
    {
        public Ellipse WpfShape { get; }

        public BolaWPF(PontoD position, double xVelocity, double yVelocity, double radius, Brush fill) : base(position, radius, xVelocity, yVelocity)
        {
            this.WpfShape = new Ellipse
            {
                Width = radius * 2,
                Height = radius * 2,
                Fill = fill
            };
        }
    }
}