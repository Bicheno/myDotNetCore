using myWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace myWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //鼠标半小时内无操作--计时器
       // private System.Timers.Timer MouseTimerTick = new System.Timers.Timer(10000);

        //private void TimerTick(object source, System.Timers.ElapsedEventArgs e)
        //{
        //    this.Dispatcher.Invoke(new Action(() =>
        //    {
        //        //锁定
        //        this.IsEnabled = false;
        //    }));
        //}

        //private void Window_Activated(object sender, EventArgs e)
        //{
        //    this.Dispatcher.Invoke(new Action(() =>
        //    {
        //        //解锁
        //        this.IsEnabled = true;
        //    }));
        //    MouseTimerTick.Stop();
        //}

        //private void Window_Deactivated(object sender, EventArgs e)
        //{
        //    MouseTimerTick.Start();
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Task task = new Task(new Action(CheckHeart));
            task.Start();
            //MouseTimerTick.Elapsed += new System.Timers.ElapsedEventHandler(TimerTick);
            //MouseTimerTick.AutoReset = true;
            //MouseTimerTick.Enabled = true;
        }        
        
        private void CheckHeart()
        {
            while (true)
            {
                Thread.Sleep(TimeSpan.FromSeconds(5));

                var time = GetLastInputTime();
                if (time > 10000)
                {
                    SetEnabled(false);
                }
                else
                {
                    SetEnabled(true);
                }
            }
        }

        private void SetEnabled(bool b)
        {
            Dispatcher.Invoke(new Action(()=>{ IsEnabled = b; }));
        }

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]//设置结构体容量
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]//捕获的时间
            public int dwTime;
        }

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        private static int GetLastInputTime()
        {
            LASTINPUTINFO vLastInputInfo = new LASTINPUTINFO();
            vLastInputInfo.cbSize = Marshal.SizeOf(vLastInputInfo);
            //捕获时间
            if (!GetLastInputInfo(ref vLastInputInfo))
                return 0;
            else
                return Environment.TickCount - vLastInputInfo.dwTime;
        }
    }

    public class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
