using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using ShortUrl.Model;

namespace ShortUrl.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        #region Var
        private string _apiKey;
        #endregion

        #region Private Property
        private string m_APIKey
        {
            get
            {
                if (_apiKey == null)
                    return string.Empty;
                return _apiKey;
            }
            set
            {
                _apiKey = value;
            }
        }
        #endregion

        private DataItem _dataItem;
        
        public string LongURL
        {
            get { return _dataItem.longUrl; }
            set
            {
                if (_dataItem.longUrl != value)
                {
                    _dataItem.longUrl = value;
                    RaisePropertyChanged("LongURL");
                }
            }
        }
        public string ShortURL
        {
            get { return _dataItem.shortUrl; }
            set
            {
                if (_dataItem.shortUrl != value)
                {
                    _dataItem.shortUrl = value;
                    RaisePropertyChanged("ShortURL");
                }
            }
        }

        public string ErrorMessage
        {
            get { return _dataItem.errorMessage; }
            set
            {
                if (_dataItem.errorMessage != value)
                {
                    _dataItem.errorMessage = value;
                    RaisePropertyChanged("ErrorMessage");
                }
            }
        }

        public EnumType Type
        {
            get { return _dataItem.type; }
            set
            {
                if (_dataItem.type != value)
                {
                    _dataItem.type = value;
                    RaisePropertyChanged("Type");
                }
            }
        }


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    _dataItem = item;
                });
        }

        #region Event
        public ICommand ButtonClick
        {
            get
            {
                return new RelayCommand<object>(btn_execute);
            }
        }

        private void btn_execute(object parameter)
        {
            try
            {
                if (Clipboard.ContainsText())
                {
                    string url = Clipboard.GetText();
                    //string pattern = @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";
                    //if (Regex.IsMatch(pattern, url, RegexOptions.IgnoreCase))
                        LongURL = url;
                }
                ThreadPool.QueueUserWorkItem(o =>
                {
                    Google.URLShortener g = new Google.URLShortener();
                    switch (Type)
                    {
                        case EnumType.Shortener:
                            {
                                if (string.IsNullOrEmpty(LongURL))
                                {
                                    if (string.IsNullOrEmpty(LongURL))
                                    {
                                        ShortURL = "沒有輸入值";
                                        return;
                                    }
                                }

                                try
                                {
                                    string shortUrl = g.CreateURLShortener(LongURL);
                                    ShortURL = shortUrl;
                                }
                                catch (Exception ex)
                                {
                                    ErrorMessage = ex.Message;
                                }

                                
                                break;
                            }
                        case EnumType.Expand:
                            {
                                g.GetURLShortener(LongURL);
                                //ShortURL = shortUrl;
                                break;
                            }
                        case EnumType.List:
                            break;
                    }
                    
                });
            }
            catch (Exception ex)
            {
                addResultMessages(ex.ToString());
            }
            finally
            {
                
            }
        }
        #endregion

        #region method
        public bool addResultMessages(string addString)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                //if (_resultMessages != null)
                //{
                //    _resultMessages.Add(new Log()
                //    {
                //        number = _resultMessages.Count + 1,
                //        content = addString
                //    });
                //}
            });
            return true;
        }

        public void getClopboardText()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                
            }));
            
            //DispatcherHelper.CheckBeginInvokeOnUI(() =>
            //{
                
            //});
           
            return ; 
        }

        #endregion
    }
}