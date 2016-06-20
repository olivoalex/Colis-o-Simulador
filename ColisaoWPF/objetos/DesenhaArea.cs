using System.Collections.ObjectModel;

namespace Colisao.Core
{
    public abstract class DesenhaArea
    {
        public ObservableCollection<object> Objetos;

        public abstract double Height { get; }

        public abstract double Width { get; }

        public abstract void UpdatePosicao(Bola ball);
    }
}