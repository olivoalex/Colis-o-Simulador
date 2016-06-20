using System;
using System.Collections.Generic;

namespace Colisao.Core
{
    internal class Colisao
    {
        private readonly double _desenhaAreaWidth;
        private readonly double _desenhaAreaHeight;

        public Colisao(double desenhaAreaWidth, double desenhaAreaHeight)
        {
            this._desenhaAreaWidth = desenhaAreaWidth;
            this._desenhaAreaHeight = desenhaAreaHeight;
        }

        public void ResultaColisao(Bola a, Bola b)
        {
            double xDist = a.X - b.X;
            double yDist = a.Y - b.Y;
            double distSquared = Math.Pow(xDist, 2) + Math.Pow(yDist, 2);

            //Verifica a distâncias evitando a raiz.
            if (distSquared <= (a.Raio + b.Raio) * (a.Raio + b.Raio))
            {
                double xVelocity = b.XVelocidade - a.XVelocidade;
                double yVelocity = b.YVelocidade - a.YVelocidade;
                double dotProduct = xDist * xVelocity + yDist * yVelocity;

                //Verificação de um objeto colidindo no outro
                if (dotProduct > 0)
                {
                    double collisionScale = dotProduct / distSquared;
                    double xCollision = xDist * collisionScale;
                    double yCollision = yDist * collisionScale;


                    //O que eu falei pra Vitória de explicar os vetores e resultados das direções.
                    double combinedMass = a.Massa + b.Massa;
                    double collisionWeightA = 2 * b.Massa / combinedMass;
                    double collisionWeightB = 2 * a.Massa / combinedMass;
                    a.XVelocidade += collisionWeightA * xCollision;
                    a.YVelocidade += collisionWeightA * yCollision;
                    b.XVelocidade -= collisionWeightB * xCollision;
                    b.YVelocidade -= collisionWeightB * yCollision;
                }
            }
        }

        public bool PosicaoOcupada(Bola b)
        {
            return false;
        }

        public void ResolveColisaoBorda(Bola b)
        {
            if (b.X - b.Raio <= 0 ||
                b.X + b.Raio - this._desenhaAreaWidth > 0)
            {
                b.XVelocidade *= -1;
            }
            if (b.Y - b.Raio <= 0 ||
                b.Y + b.Raio - this._desenhaAreaHeight > 0)
            {
                b.YVelocidade *= -1;
            }
        }

        public bool CanvasPopulado(PontoD posicao, double raio, ICollection<Bola> bolas)
        {
            bool populado = false;
            foreach (Bola bola in bolas)
            {
                if (Math.Abs(bola.X - posicao.X) < bola.Raio || Math.Abs(bola.Y - posicao.Y) < bola.Raio)
                {
                    populado = true;
                }
            }
            return populado;
        }
    }
}