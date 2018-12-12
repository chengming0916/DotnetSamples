using System;
using System.Collections;
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
using System.Windows.Shapes;

namespace DataErrorInfoSample
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            this.DataContext = new Window1ViewModel();
        }
    }

    public class Window1ViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public Window1ViewModel()
        {
            Test = string.Empty;

            this.TestCommand = new DelegateCommand(obj =>
            {

            }, obj => !HasErrors);
        }

        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public bool HasErrors { get { return _errors.Count > 0; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private string _test;
        public string Test
        {
            get { return _test; }
            set
            {
                if (_test != value)
                {
                    var propertyName = nameof(Test);
                    if (string.IsNullOrEmpty(value))
                    {
                        if (!_errors.ContainsKey(propertyName))
                            _errors.Add(propertyName, new List<string>());
                        _errors[propertyName].Add("不能为空");
                    }
                    else
                    {
                        if (_errors.ContainsKey(propertyName))
                            _errors.Remove(propertyName);
                    }

                    _test = value;
                    RaisePropertyChanged(nameof(Test));
                    RaiseErrorChanged(nameof(Test));
                    TestCommand?.RaiseCanExecuteChanged();
                    //TestCommand.CanExecute(null);
                    //CommandManager.AddCanExecuteHandler(this, TestCommand.CanExecuteChanged);
                }
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName)) return _errors[propertyName];
            return _errors.Values;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RaiseErrorChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public DelegateCommand TestCommand { get; private set; }
    }

    /// <summary>
    /// Delegatecommand
    /// </summary>
    public class DelegateCommand : ICommand
    {
        Func<object, bool> canExecute;
        Action<object> executeAction;
        bool canExecuteCache;

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            bool temp = canExecute(parameter);

            if (canExecuteCache != temp)
            {
                canExecuteCache = temp;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }

            return canExecuteCache;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }

        #endregion

        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}
