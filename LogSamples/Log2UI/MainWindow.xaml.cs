using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Log2UI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILog Logger = LogManager.GetLogger(typeof(MainWindow));
        private readonly ILog UILogger = LogManager.GetLogger("UILogger");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hierarchy = LogManager.GetRepository() as Hierarchy;
            var appenders = hierarchy.Root.Repository.GetAppenders();
            foreach (var appender in appenders)
            {
                if (appender is UILogAppender uILogAppender)
                    uILogAppender.UILogReceived += (s, args) =>
                    {
                        this.LogTextBox.Dispatcher.BeginInvoke(new Action(() => LogTextBox.AppendText(args.Message)));
                    };
            }

            ThreadPool.QueueUserWorkItem(obj =>
            {
                while (true)
                {
                    LogTextBox.Dispatcher.BeginInvoke((Action)delegate
                    {
                        LogTextBox.ScrollToEnd();
                    });

                    Thread.Sleep(500);
                }
            });

            ThreadPool.QueueUserWorkItem(obj =>
            {
                while (true)
                {
                    Logger.Debug("Log debug");
                    Logger.Info("Log info");
                    Logger.Warn("Log warn");
                    Thread.Sleep(500);
                }
            });
        }
    }
}
