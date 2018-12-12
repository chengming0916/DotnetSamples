using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CustomControlValidator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();
        }
    }

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _test;
        public string Test
        {
            get { return _test; }
            set
            {
                _test = value;
                RaisePropertyChanged(nameof(Test));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RequiredValidator : Validator
    {
        public override string ErrorMessage
        {
            get { return "不能为空"; }
        }

        public override bool InitialValidation()
        {
            if (Source == null) return false;
            return string.IsNullOrEmpty(Source.ToString());
        }
    }

    public abstract class Validator : FrameworkElement
    {
        static Validator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Validator), new FrameworkPropertyMetadata(typeof(Validator)));
        }

        public virtual string ErrorMessage { get { return string.Empty; } }

        public abstract bool InitialValidation();

        public FrameworkElement ElementName
        {
            get { return (FrameworkElement)GetValue(ElementNameProperty); }
            set { SetValue(ElementNameProperty, value); }
        }

        public static readonly DependencyProperty ElementNameProperty
            = DependencyProperty.Register("ElementName", typeof(FrameworkElement), typeof(Validator), new PropertyMetadata(null));



        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(Validator),
                new UIPropertyMetadata(new PropertyChangedCallback(ValidPropertyChanged)));

        private static void ValidPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var validator = d as Validator;
            if (validator != null) validator.SetSourceFromProperty();
            if (string.IsNullOrEmpty(e.NewValue.ToString()))
            {
                if (validator != null)
                {
                    validator.IsValid = validator.InitialValidation();
                    if (validator.ElementName.DataContext != null)
                        validator.ShowToolTip();
                    validator.IsValid = false;
                }
            }
        }

        private void ShowToolTip()
        {
            if (IsValid)
            {
                timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(1.5)
                };
                _toolTip = new ToolTip
                {
                    StaysOpen = true,
                    PlacementTarget = ElementName,
                    Placement = PlacementMode.Right,
                    Content = ErrorMessage,
                    IsOpen = true
                };
                timer.Tick += (sender, args) =>
                {
                    _toolTip.IsOpen = false;
                    timer.Stop();
                };
                timer.Start();
            }
        }

        private void SetSourceFromProperty()
        {
            var expression = this.GetBindingExpression(SourceProperty);
            if (expression != null && this.ElementName == null)
                this.SetValue(Validator.ElementNameProperty, expression.DataItem as FrameworkElement);
        }

        private ToolTip _toolTip;

        private DispatcherTimer timer;

        public bool IsValid { get; set; }
    }
}
