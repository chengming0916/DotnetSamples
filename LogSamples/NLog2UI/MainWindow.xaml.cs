<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
﻿using NLog;
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
=======
=======
>>>>>>> develop
=======
>>>>>>> develop
﻿using Log2UI;
using NLog;
using NLog.Targets;
using System;
using System.Threading;
using System.Windows;
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> develop
=======
>>>>>>> develop
=======
>>>>>>> develop

namespace NLog2UI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        private NLog.ILogger Logger = LogManager.GetCurrentClassLogger();
=======
=======
>>>>>>> develop
=======
>>>>>>> develop
        static MainWindow()
        {
            Target.Register<EventTarget>("event");
        }

        private NLog.ILogger Logger = LogManager.GetCurrentClassLogger();
        private bool _isRunning;
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> develop
=======
>>>>>>> develop
=======
>>>>>>> develop

        public MainWindow()
        {
            InitializeComponent();
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
=======
>>>>>>> develop
=======
>>>>>>> develop
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
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> develop
=======
>>>>>>> develop
=======
>>>>>>> develop
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
            //NLog.Targets.Target target =

>>>>>>> develop
=======
            //NLog.Targets.Target target =

>>>>>>> develop
=======
            //NLog.Targets.Target target =

>>>>>>> develop
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

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
                    Thread.Sleep(TimeSpan.FromMilliseconds(500));
                }
            });
        }
    }
}
=======
=======
>>>>>>> develop
=======
>>>>>>> develop
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
<<<<<<< HEAD
<<<<<<< HEAD
}
>>>>>>> develop
=======
}
>>>>>>> develop
=======
}
>>>>>>> develop
