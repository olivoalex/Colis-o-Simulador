using System.Windows.Controls;
using Colisao.Core;

namespace ColisaoWPF
{
    internal class DesenhaAreaCanvas : DesenhaArea
    {
        public override double Height => this.CanvasArea.ActualHeight;

        public override double Width => this.CanvasArea.ActualWidth;

        public System.Windows.Controls.Canvas CanvasArea { get; set; }

        public DesenhaAreaCanvas(System.Windows.Controls.Canvas canvas)
        {
            this.CanvasArea = canvas;
        }

        public override void UpdatePosicao(Bola ball)
        {
            BolaWPF castedBall = (BolaWPF) ball;
            System.Windows.Controls.Canvas.SetLeft(castedBall.WpfShape, castedBall.X - castedBall.Raio);
            System.Windows.Controls.Canvas.SetTop(castedBall.WpfShape, castedBall.Y - castedBall.Raio);
        }
    }
}