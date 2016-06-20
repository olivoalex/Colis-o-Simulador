namespace Colisao.Core
{
    public abstract class Bola
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Raio { get; }

        public double Massa { get; set; }

        public double XVelocidade { get; set; }

        public double YVelocidade { get; set; }

        protected Bola(PontoD posicao, double raio, double xVelocidade, double yVelocidade)
        {
            this.X = posicao.X;
            this.Y = posicao.Y;
            this.Raio = raio;
            this.Massa = raio*2.5;
            this.XVelocidade = xVelocidade;
            this.YVelocidade = yVelocidade;
        }
    }
}