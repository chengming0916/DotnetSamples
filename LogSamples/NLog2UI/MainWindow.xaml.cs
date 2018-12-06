using Log2UI;
using NLog;
using NLog.Targets;
using System;
using System.Threading;
using System.Windows;

namespace NLog2UI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        static MainWindow()
        {
            Target.Register<EventTarget>("event");
        }

        private NLog.ILogger Logger = LogManager.GetCurrentClassLogger();
        private bool _isRunning;

        public MainWindow()
        {
            InitializeComponent();
            this.Closed += (s, e) => _isRunning = false;
            var targtes = Logger.Factory.Configuration.AllTargets;
            foreach (var item in targtes)
            {
                if (item is EventTarget target)
                {
                    target.LogReceived += (s, e) =>
                    {
                        LogTextBox.Dispatcher.Invoke(() =>
                        {
                            LogTextBox.AppendText(e.Message);
                        });
                    };
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //NLog.Targets.Target target =

            ThreadPool.QueueUserWorkItem(state =>
            {
                while (true)
                {
                    Logger.Debug("nlog debug test");
                    Logger.Trace("nlog debug test");
                    Logger.Warn("nlog debug test");
                    Logger.Info("nlog debug test");
                    Logger.Error("nlog debug test");
                    Logger.Fatal("nlog debug test");

                    Thread.Sleep(TimeSpan.FromMilliseconds(5000));
                }
            });

            _isRunning = true;
            ThreadPool.QueueUserWorkItem(state =>
            {
                while (_isRunning)
                {
                    LogTextBox.Dispatcher.Invoke(() =>
                    {
                        LogTextBox.ScrollToEnd();
                    });
                    Thread.Sleep(500);
                }
            });
        }

    }
}