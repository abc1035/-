using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 在线考试系统
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:在线考试系统"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:在线考试系统;assembly=在线考试系统"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:ScrollingTextControl/>
    ///
    /// </summary>
    public class ScrollingTextControl : Label
    {
        /// <summary>
        /// 定时器
        /// </summary>
        Timer MarqueeTimer = new Timer();
        /// <summary>
        /// 滚动文字源
        /// </summary>
        String _TextSource = "滚动文字源";
        /// <summary>
        /// 输出文本
        /// </summary>
        String _OutText = string.Empty;
        /// <summary>
        /// 过度文本存储
        /// </summary>
        string _TempString = string.Empty;
        /// <summary>
        /// 文字的滚动速度
        /// </summary>
        double _RunSpeed = 1000;

        DateTime _SignTime;
        bool _IfFirst = true;

        /// <summary>
        /// 滚动一循环字幕停留的秒数,单位为毫秒,默认值停留3秒
        /// </summary>
        int _StopSecond = 3000;

        /// <summary>
        /// 滚动一循环字幕停留的秒数,单位为毫秒,默认值停留3秒
        /// </summary>
        public int StopSecond
        {
            get { return _StopSecond; }
            set
            {
                _StopSecond = value;
            }
        }

        /// <summary>
        /// 滚动的速度
        /// </summary>
        public double RunSpeed
        {
            get { return _RunSpeed; }
            set
            {
                _RunSpeed = value;
                MarqueeTimer.Interval = _RunSpeed;
            }
        }

        /// <summary>
        /// 滚动文字源
        /// </summary>
        public string TextSource
        {
            get { return _TextSource; }
            set
            {
                _TextSource = value;
                _TempString = _TextSource + "   ";
                _OutText = _TempString;
            }
        }

        private string SetContent
        {
            get { return Content.ToString(); }
            set
            {
                Content = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ScrollingTextControl()
        {
            MarqueeTimer.Interval = _RunSpeed;//文字移动的速度
            MarqueeTimer.Enabled = true;      //开启定时触发事件
            MarqueeTimer.Elapsed += new ElapsedEventHandler(MarqueeTimer_Elapsed);//绑定定时事件
            this.Loaded += new RoutedEventHandler(ScrollingTextControl_Loaded);//绑定控件Loaded事件
        }


        void ScrollingTextControl_Loaded(object sender, RoutedEventArgs e)
        {
            _TextSource = SetContent;
            _TempString = _TextSource + "   ";
            _OutText = _TempString;
            _SignTime = DateTime.Now;
        }


        void MarqueeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (string.IsNullOrEmpty(_OutText)) return;

            if (_OutText.Substring(1) + _OutText[0] == _TempString)
            {
                if (_IfFirst)
                {
                    _SignTime = DateTime.Now;
                }

                if ((DateTime.Now - _SignTime).TotalMilliseconds > _StopSecond)
                {
                    _IfFirst = true; ;
                }
                else
                {
                    _IfFirst = false;
                    return;
                }
            }

            _OutText = _OutText.Substring(1) + _OutText[0];


            Dispatcher.BeginInvoke(new Action(() =>
            {
                SetContent = _OutText;
            }));


        }

    }
}
