using System;
using System.IO;
using System.Linq;
using System.Reflection;
using WeifenLuo.WinFormsUI.Docking;

namespace Automation.FrameworkUtilityLib.UtilViews
{
    public partial class VersionForm : DockContent
    {
        public VersionForm()
        {
            InitializeComponent();
        }

        private void VersionForm_Load(object sender, EventArgs e)
        {
            var assemblies = Directory.GetFiles(Directory.GetCurrentDirectory()).Where(f=>f.Contains("Automation") && f.EndsWith(".dll")).Select(Assembly.LoadFrom).ToList();

            listBox1.Items.Clear();

            var a = Assembly.GetEntryAssembly().GetName();
            listBox1.Items.Add($"{a.Name}: {a.Version}");
            foreach (var ass in assemblies)
            {
                if (ass.GetName().Name.StartsWith("Automation"))
                {
                    listBox1.Items.Add($"{ass.GetName().Name}: {ass.GetName().Version}");
                }
            }
        }
    }
}