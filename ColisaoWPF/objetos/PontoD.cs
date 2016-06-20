using System.Drawing;

#pragma warning disable 1591

namespace Colisao.Core
{
    public struct PontoD
    {
        public double X;
        public double Y;

        public PontoD(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point ToPonto()
        {
            return new Point((int) this.X, (int) this.Y);
        }

        public override bool Equals(object obj)
        {
            return obj is PontoD && this == (PontoD) obj;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode();
        }

        public static bool operator ==(PontoD a, PontoD b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(PontoD a, PontoD b)
        {
            return !(a == b);
        }
    }
}
