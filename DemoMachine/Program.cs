using System;
using System.Threading;
using System.Windows.Forms;
using Automation.FrameworkExtension;
using Automation.FrameworkExtension.frameworkManage;

namespace DemoMachine
{
    internal static class Program
    {
        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            bool createdNew;
            var mutex = new Mutex(false, nameof(DemoMachine), out createdNew);
            if (!createdNew)
            {
                MessageBox.Show("系统中已有一个相同的程序在运行，请确认！");
                Environment.Exit(1);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += (sender, args) =>
            {
                MessageBox.Show("APPLICATION EXCEPTION:" + args.Exception.Message.ToString());
                //throw new Exception("Thread Exception");
            };

            try
            {
                GC.Collect();


                FrameworkManager.IsDebug = true;
                FrameworkManager.Ins.Load(@".\Config\framework.cfg");
                FrameworkManager.Ins.Initialize();


                //初始化
                //initialize machine settings
                Machine.DemoMachine.Ins.Load();
                Machine.DemoMachine.Ins.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"FRAMEWORK EXCEPTION: 初始化失败: {ex.Message}");
                return;
            }

            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"FRAMEWORK EXCEPTION: 程序异常: {ex.Message}");
            }
            finally
            {
                try
                {
                    Machine.DemoMachine.Ins.Terminate();
                    Machine.DemoMachine.Ins.Save();

                    FrameworkManager.Ins.Terminate();
                    FrameworkManager.Ins.Save(@".\Config\framework.cfg");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"FRAMEWORK EXCEPTION: 程序关闭失败: {ex.Message}");
                }
            }
        }
    }
}