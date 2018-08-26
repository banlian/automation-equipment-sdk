using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Automation.FrameworkExtension.stateMachine;

namespace Automation.FrameworkExtension.platforms.calibrations
{
    public class AutoCalib : StationTask
    {
        public event Action<int> ProgressEvent;

        public AutoCalib() : base(0, "AutoCalib", null)
        {
            if (!Directory.Exists($@".\Calib"))
            {
                Directory.CreateDirectory(@".\Calib");
            }

            CalibInfo = GetType().Name;
            DataList = new List<string>();
        }

        public string CalibInfo { get; set; }

        public List<string> DataList { get; set; }

        protected override int ResetLoop()
        {
            UninitCalib();
            return 0;
        }

        protected override int RunLoop()
        {
            try
            {
                if (Station.AutoState != StationAutoState.WaitRun)
                {
                    MessageBox.Show($"工站{Station.Name}未复位", CalibInfo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }
                Log($"CALIBRATION RUN START...");
                ClearData();

                UpdateProgress(1);
                InitCalib();
                UpdateProgress(5);

                if (MessageBox.Show($"标定初始化完成. 开始{CalibInfo}标定?", CalibInfo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    ThrowException("Calib Cancelled");
                }

                DoCalib();
                UpdateProgress(95);

                SaveData();

            }
            catch (Exception e)
            {
                Log($"CALIBRATION RUN FAIL:{e.Message}");
                MessageBox.Show($"{CalibInfo}标定运行异常:{e.Message}");
            }
            finally
            {
                try
                {
                    UninitCalib();
                    UpdateProgress(100);

                    Log($"CALIBRATION RUN SUCCESS");

                    DisplayOutput();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"{CalibInfo}标定复位异常:{e.Message}");
                }
            }

            UpdateProgress(100);
            return -1;
        }

        protected virtual void UpdateProgress(int obj)
        {
            ProgressEvent?.Invoke(obj);
        }

        public virtual void ClearData()
        {
            DataList.Clear();
        }

        public virtual void InitCalib()
        {
        }

        public virtual void DoCalib()
        {
        }

        public virtual void SaveData(string fileAppendix = null)
        {
            string file;
            if (fileAppendix != null)
            {
                file = $@".\Calib\{CalibInfo}-{fileAppendix}-{DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")}.dat";
            }
            else
            {
                file = $@".\Calib\{CalibInfo}-{DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")}.dat";
            }

            using (var fs = new FileStream(file, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    foreach (var data in DataList)
                    {
                        sw.WriteLine(data);
                    }
                }
            }
        }

        public virtual void UninitCalib()
        {
        }

        public virtual string DisplayOutput()
        {
            var sb = new StringBuilder();
            var props = this.GetType().GetProperties().ToList().FindAll(p => p.GetCustomAttribute<CategoryAttribute>()?.Category == "OUTPUT");

            foreach (var p in props)
            {
                var str = $"{p.Name}:\r\n{p.GetValue(this)}";
                Log(str);
                sb.AppendLine(str);
            }

            return sb.ToString();

        }

    }
}