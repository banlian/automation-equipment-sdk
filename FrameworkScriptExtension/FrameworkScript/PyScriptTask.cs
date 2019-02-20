using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Automation.FrameworkExtension.common;
using Automation.FrameworkExtension.stateMachine;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace Automation.FrameworkScriptExtension.FrameworkScript
{
    public class PyScriptTask : StationTask
    {
        public PyScriptTask(int id, string name, Station station) : base(id, name, station)
        {
            _scriptEngine = Python.CreateEngine();
            _scriptEngine.Runtime.LoadAssembly(Assembly.GetAssembly(typeof(RunningState)));

            _scriptScope = _scriptEngine.CreateScope();
            _scriptScope.SetVariable("t", this);

            _scriptFileName = $@".\Scripts\{Name}.py";
        }

        private ScriptEngine _scriptEngine;
        private ScriptScope _scriptScope;
        private ScriptSource _scriptSource;

        private string _scriptFileName;
        private string _scriptString;

        private DateTime _lastRunDateTime;

        protected override int ResetLoop()
        {
            var scriptFile = File.ReadAllText(_scriptFileName);
            if (string.IsNullOrEmpty(scriptFile))
            {
                Log($"Load Script Error", LogLevel.Error);
                return -1;
            }

            _scriptScope.SetVariable("state", RunningState.Resetting);

            if (_scriptSource == null)
            {
                _lastRunDateTime = DateTime.Now;
             
                _scriptSource = _scriptEngine.CreateScriptSourceFromString(scriptFile);
                _scriptSource.Execute(_scriptScope);
            }
            else
            {
                var fileInfo = new FileInfo($@".\Scripts\{Name}.py");
                if (fileInfo.LastWriteTime > _lastRunDateTime)
                {
                    _scriptSource = _scriptEngine.CreateScriptSourceFromString(scriptFile);
                    _scriptSource.Execute(_scriptScope);
                }
                else
                {
                    _scriptSource.Execute(_scriptScope);
                }
            }
            return -1;
        }


        protected override int RunLoop()
        {
            var scriptFile = File.ReadAllText(_scriptFileName);
            if (string.IsNullOrEmpty(scriptFile))
            {
                Log($"Load Script Error", LogLevel.Error);
                return -1;
            }

            _scriptScope.SetVariable("state", RunningState.Running);

            if (_scriptSource == null)
            {
                _lastRunDateTime = DateTime.Now;

                _scriptSource = _scriptEngine.CreateScriptSourceFromString(scriptFile);
                _scriptSource.Execute(_scriptScope);
            }
            else
            {
                var fileInfo = new FileInfo(_scriptFileName);
                if (fileInfo.LastWriteTime > _lastRunDateTime)
                {
                    _scriptSource = _scriptEngine.CreateScriptSourceFromString(scriptFile);
                    _scriptSource.Execute(_scriptScope);
                }
                else
                {
                    _scriptSource.Execute(_scriptScope);
                }
            }
            return -1;
        }
    }
}
