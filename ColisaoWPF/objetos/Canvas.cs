using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Colisao.Core
{
    public class Canvas
    {
        private readonly ObservableCollection<Bola> _bola;
        public NotifyCollectionChangedEventHandler CollectionChangedHandler;
        private readonly DesenhaArea _desenhaArea;
        private readonly Colisao _resultadoColisao;
        private static readonly Random Rnd = new Random();

        public Canvas(DesenhaArea drawingArea)
        {
            this._bola = new ObservableCollection<Bola>();
            this._bola.CollectionChanged += (sender, args) => this.CollectionChangedHandler.Invoke(sender, args);
            this._desenhaArea = drawingArea;
            this._resultadoColisao = new Colisao(this._desenhaArea.Width, this._desenhaArea.Height);
        }

        public void AddObjeto(Bola obj)
        {
            if (this._resultadoColisao.PosicaoOcupada(obj))
            {
                throw new ArgumentException("Posição já ocupada!");
            }
            this._bola.Add(obj);
        }


        public PontoD GetPosicaoNaoOcupada(double raio)
        {
            double x = Rnd.Next((int) (raio + 1), (int) (this._desenhaArea.Width - raio));
            double y = Rnd.Next((int) (raio + 1), (int) (this._desenhaArea.Height - raio));

            if (this._bola.Count == 0)
            {
                return new PontoD(x, y);
            }
            while (this._resultadoColisao.CanvasPopulado(new PontoD(x, y), raio, this._bola))
            {
                x = Rnd.Next((int) (raio + 1), (int) (this._desenhaArea.Width - raio));
                y = Rnd.Next((int) (raio + 1), (int) (this._desenhaArea.Height - raio));
            }
            return new PontoD(x, y);
        }

        public void OnTick()
        {
            HandleCollisions();
            foreach (Bola o in this._bola)
            {
                Move(o);
            }
        }

        private void Move(Bola o)
        {
            o.X += o.XVelocidade;
            o.Y += o.YVelocidade;
            this._desenhaArea.UpdatePosicao(o);
        }

        private void HandleCollisions()
        {
            for (int i = 0; i < this._bola.Count; i++)
            {
                Bola a = this._bola[i];
                this._resultadoColisao.ResolveColisaoBorda(a);
                for (int j = i + 1; j < this._bola.Count; j++)
                {
                    Bola b = this._bola[j];
                    this._resultadoColisao.ResultaColisao(a, b);
                }
            }
        }

        public Tuple<double, double> GetRandomVelocidade(int min, int max)
        {
            return new Tuple<double, double>(Rnd.ProximoDouble(min, max), Rnd.ProximoDouble(min, max));
        }
    }
}