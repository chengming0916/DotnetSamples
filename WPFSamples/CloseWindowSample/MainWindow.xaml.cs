using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CloseWindowSample
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            EventServiceFactory.EventService.GetEvent<GenericEvent<string>>()
                .Subscribe(x =>
                {
                    if (x.Topic == "CloseWindow")
                        this.Close();
                });
        }
    }

    public class MainViewModel : BindableBase
    {
        public MainViewModel()
        {
            CloseCommand = new DelegateCommand(Close);
            BehaviorCloseCommand = new DelegateCommand(Close1);
            EventCloseCommand = new DelegateCommand(Close2);
        }

        private void Close2()
        {
            EventServiceFactory.EventService.GetEvent<GenericEvent<string>>().Publish(new EventParameters<string>
            {
                Topic = "CloseWindow"
            });
        }

        private void Close1()
        {
            this.Closed = true;
        }

        private void Close()
        {
            this.DialogResult = true;
        }

        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand BehaviorCloseCommand { get; private set; }
        public DelegateCommand EventCloseCommand { get; private set; }

        private bool? m_dialogResult;
        public bool? DialogResult
        {
            get { return m_dialogResult; }
            set { SetProperty(ref m_dialogResult, value); }
        }

        private bool m_closed;
        public bool Closed
        {
            get { return m_closed; }
            set { SetProperty(ref m_closed, value); }
        }
    }

    /// <summary>
    /// 关闭窗体附加属性
    /// </summary>
    public static class DialogCloser
    {
        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached("DialogResult", typeof(bool?), typeof(DialogCloser),
                new PropertyMetadata(DialogResultChanged));

        private static void DialogResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                //window.DialogResult = e.NewValue as bool?;
                if ((bool)e.NewValue)
                {
                    window.Close();
                }
            }
        }

        public static void SetDialogResult(Window target, bool? value)
        {
            target.SetValue(DialogResultProperty, value);
        }
    }

    /// <summary>
    /// 关闭窗体Behavior
    /// </summary>
    public class WindowBehavior : Behavior<Window>
    {
        public WindowBehavior()
        {
        }

        public bool Close
        {
            get { return (bool)GetValue(CloseProperty); }
            set { SetValue(CloseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Close.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseProperty =
            DependencyProperty.Register("Close", typeof(bool), typeof(WindowBehavior), new PropertyMetadata(false, OnCloseChanged));

        private static void OnCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = ((WindowBehavior)d).AssociatedObject;
            var newValue = (bool)e.NewValue;
            if (newValue)
                window.Close();
        }
    }

    public static class EventServiceFactory
    {
        private static EventAggregator m_eventService;

        private static readonly object m_syncRoot = new object();

        public static EventAggregator EventService
        {
            get
            {
                lock (m_syncRoot)
                {
                    return m_eventService ?? (m_eventService = new EventAggregator());
                }
            }
        }
    }

    public class GenericEvent<TValue> : PubSubEvent<EventParameters<TValue>> { }
    public class GenericEvent : PubSubEvent<EventParameters<int>> { }
    public class EventParameters<TValue>
    {
        public string Topic { get; set; }

        public Action ExpectedAction { get; set; }

        public TValue Value { get; set; }
    }

}
