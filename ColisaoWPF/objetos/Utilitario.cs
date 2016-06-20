using System;

namespace Colisao.Core
{
    internal static class Utilitario
    {
        public static double ProximoDouble(this Random random, double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }
}