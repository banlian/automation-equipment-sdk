using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Automation.FrameworkExtension.common;
using Automation.FrameworkExtension.motionDriver;
using Automation.FrameworkExtension.stateMachine;

namespace Automation.FrameworkExtension.frameworkManage
{
    /// <summary>
    /// 
    /// </summary>
    public class FrameworkManager : UserSettings<FrameworkManager>
    {
        #region singleton

        private FrameworkManager()
        {
        }

        public static FrameworkManager Ins { get; private set; } = new FrameworkManager();

        #endregion


        public bool Reboot = false;

        public static bool IsSimulate = false;

        public static string FrameworkExceptionHead = "FRAMEWORK EXCEPTION";


        #region  framework debug methods



        public static bool IsDebug = false;

        private static FrameworkDebugForm _debugForm;


        public static void Debug(string log)
        {
            if (!IsDebug)
            {
                return;
            }

            if (_debugForm != null)
            {
                _debugForm.UpdateLog(log);
            }
        }
        public static void Error(string msg)
        {
            if (IsDebug)
            {
                Debug(msg);
            }

            MessageBox.Show(msg, FrameworkExceptionHead, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        public new void Load(string env)
        {
            if (File.Exists(env))
            {
                var ins = UserSettings<FrameworkManager>.Load(env);
                if (ins != null)
                {
                    Ins = ins;
                }
            }
            else
            {
                Ins = new FrameworkManager();
                Ins.Save(env);
            }

            _debugForm = new FrameworkDebugForm();

            LoadMotionDriverTypes(Directory.GetCurrentDirectory());
            LoadStationTaskTypes(Directory.GetCurrentDirectory());

        }


        #region framework reflection methods




        public static Dictionary<string, Type> TaskTypes = new Dictionary<string, Type>();
        public static Dictionary<string, Type> MotionCardTypes = new Dictionary<string, Type>();


        public static void LoadMotionDriverTypes(string folder)
        {
            var files = Directory.GetFiles(folder).Select(f => new FileInfo(f)).ToList();
            files = files.FindAll(f => f.Name.EndsWith(".dll") && f.Name.StartsWith("Automation"));

            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file.FullName);
                var motionCardTypes = assembly.GetExportedTypes().Where(t => t.GetInterface(nameof(IMotionCard)) != null);
                foreach (var t in motionCardTypes)
                {
                    MotionCardTypes.Add(t.Name, t);
                }
            }
            FrameworkManager.Debug($"加载运动驱动类型：\n{string.Join("\n", MotionCardTypes.Select(t => t.Key))}");
        }


        public static void LoadStationTaskTypes(string folder)
        {
            var files = Directory.GetFiles(folder).Select(f => new FileInfo(f)).ToList();
            files = files.FindAll(f => (f.Name.EndsWith(".dll") && f.Name.StartsWith("Automation")) || f.Name.EndsWith(".exe"));

            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file.FullName);
                var taskTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(StationTask)));
                foreach (var t in taskTypes)
                {
                    TaskTypes.Add(t.Name, t);
                }
            }
            FrameworkManager.Debug($"加载用户定义任务类型：\n{string.Join("\n", TaskTypes.Select(t => t.Key))}");
        }



        #endregion

        /// <summary>
        /// 初始化PrimsFactory
        /// </summary>
        public void Initialize()
        {
            if (IsDebug)
            {
                _debugForm?.Show();
            }
        }


        public void Terminate()
        {
            if (IsDebug)
            {
                _debugForm?.Hide();
                _debugForm?.Close();
            }
        }


        public override bool Verify()
        {
            return true;
        }



    }
}