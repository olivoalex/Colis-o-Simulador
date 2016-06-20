using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Colisao.Core;

namespace ColisaoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _timer;
        private DesenhaAreaCanvas _canvasDrawingArea;
        private Canvas _world;
        private readonly BrushConverter _brushConverter;

        public MainWindow()
        {
            this._brushConverter = new BrushConverter();
            this._timer = new DispatcherTimer(DispatcherPriority.Render, this.Dispatcher)
            {
                Interval = TimeSpan.FromMilliseconds(5),
                IsEnabled = true
            };
            this._timer.Tick += _timer_Tick;
            InitializeComponent();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            this._world.OnTick();
        }

        private void CanvasMain_OnLoaded(object sender, RoutedEventArgs e)
        {
            // Definindo raio mínimo
            this.IntegerUpDownRadius.Minimum = 5;
            // Definindo raio máximo
            this.IntegerUpDownRadius.Maximum = (int?)(this.CanvasMain.ActualWidth / 2);

            this._canvasDrawingArea = new DesenhaAreaCanvas(this.CanvasMain);
            this._world = new Canvas(this._canvasDrawingArea);
            this._world.CollectionChangedHandler += CollectionChangedHandler;
            this._timer.Start();
        }

        private void CollectionChangedHandler(object sender,
                                              NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            foreach (BolaWPF changedObject in notifyCollectionChangedEventArgs.NewItems.Cast<BolaWPF>())
            {
                switch (notifyCollectionChangedEventArgs.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        this.CanvasMain.Children.Add(changedObject.WpfShape);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        this.CanvasMain.Children.Remove(changedObject.WpfShape);
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        break;
                    case NotifyCollectionChangedAction.Move:
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void BotaoAddBola_Click(object sender, RoutedEventArgs e)
        {
            Color? color = this.ColorPickerColor.SelectedColor;
            if (color == null)
            {
                MessageBox.Show("Por favor, selecione uma cor!");
                return;
            }
            int? radius = this.IntegerUpDownRadius.Value;
            if (radius != null)
            {
                PontoD position = this._world.GetPosicaoNaoOcupada((double) radius);
                Tuple<double, double> speed = this._world.GetRandomVelocidade(-1, 1);

                this._world.AddObjeto(new BolaWPF(position, speed.Item1, speed.Item2, (double) radius,
                    GetBrushFromColor((Color) color)));
            }
            else
            {
                MessageBox.Show("Por favor, adicionar o raio.");
            }
        }


        private Brush GetBrushFromColor(Color color)
        {
            return (Brush) this._brushConverter.ConvertFromString(color.ToString());
        }
    }
}