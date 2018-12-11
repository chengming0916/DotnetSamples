using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

namespace DataErrorInfoSample
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

    public class MainWindowViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _error;

        public string this[string columnName]
        {
            get { return GetErrorFor(columnName); }
        }

        private string GetErrorFor(string columnName)
        {
            switch (columnName)
            {
                case "Test":
                    if (string.IsNullOrEmpty(Test))
                        return "测试";
                    break;

                default:
                    break;
            }
            return string.Empty;
        }

        public string Error
        {
            get { return _error; }
            set { _error = value; }
        }

        private string _test;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Test
        {
            get { return _test; }
            set
            {
                _test = value;
                RaisePropertyChanged(nameof(Test));
            }
        }
    }
}
