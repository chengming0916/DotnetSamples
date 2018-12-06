using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace NLog2UI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private NLog.ILogger Logger = LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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

                    Thread.Sleep(TimeSpan.FromMilliseconds(500));
                }
            });
        }
    }
}
