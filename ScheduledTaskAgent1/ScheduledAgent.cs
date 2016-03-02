using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;

namespace ScheduledTaskAgent1
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent 构造函数，初始化 UnhandledException 处理程序
        /// </remarks>
        static ScheduledAgent()
        {
            // 订阅托管的异常处理程序
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// 出现未处理的异常时执行的代码
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // 出现未处理的异常；强行进入调试器
                Debugger.Break();
            }
        }

        /// <summary>
        /// 运行计划任务的代理
        /// </summary>
        /// <param name="task">
        /// 调用的任务
        /// </param>
        /// <remarks>
        /// 调用定期或资源密集型任务时调用此方法
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
 
            foreach(ShellTile title in ShellTile.ActiveTiles)
            {
                FlipTileData flip = new FlipTileData();
                flip.Title = "2014/8";
                title.Update(flip);
            }

            //ShellToast toast = new ShellToast();
            //toast.Title = "My dfsfe";
            //toast.Content = "adfa";
            //toast.Show();
            NotifyComplete();
        }
    }
}