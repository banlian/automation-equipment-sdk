using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Automation.FrameworkExtension.deviceDriver;
using Automation.FrameworkExtension.elements;
using Automation.FrameworkExtension.elementsInterfaces;
using Automation.FrameworkExtension.motionDriver;
using Automation.FrameworkExtension.platforms.motionPlatforms;
using Automation.FrameworkExtension.stateMachine;

namespace Automation.FrameworkExtension.loadUtils
{
    public static class StateMachineExtension
    {
        public static void Deserialize(this StateMachine machine, string file)
        {
            if (!File.Exists(file))
            {
                return;
            }
            SectionReader sectionReader = new SectionReader();

            try
            {
                using (var fs = new FileStream(file, FileMode.Open))
                {
                    using (var sw = new StreamReader(fs))
                    {
                        string line = sw.ReadLine();
                        while (line != null)
                        {
                            line = line.Replace("\t", "").Replace("\r\n", "");
                            if (line.StartsWith(@"\\") || line.StartsWith("//"))
                            {
                                //read next line
                                line = sw.ReadLine();
                                continue;
                            }

                            //normal line parse
                            sectionReader.ReadLine(machine, line);

                            //read next line
                            line = sw.ReadLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{sectionReader.Section} {sectionReader.IndentSection} Deserialize Machine Error:{ex.Message}");
                throw ex;
            }


        }

        public static void Serialize(this StateMachine machine, string file)
        {
            using (var fs = new FileStream(file, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {

                    sw.WriteLine(new SectionWriter<MotionCardWrapper>("MOTION", machine.MotionExs).Export());
                    sw.WriteLine(new SectionWriter<DiEx>("DI", machine.DiExs).Export());
                    sw.WriteLine(new SectionWriter<DoEx>("DO", machine.DoExs).Export());
                    sw.WriteLine(new SectionWriter<CylinderEx>("CY", machine.CylinderExs).Export());
                    sw.WriteLine(new SectionWriter<VioEx>("VIO", machine.VioExs).Export());
                    sw.WriteLine();


                    sw.WriteLine(new SectionWriter<DiEx>("ESTOP", machine.DiEstop).Export());
                    sw.WriteLine(new SectionWriter<DiEx>("START", machine.DiStart).Export());
                    sw.WriteLine(new SectionWriter<DiEx>("STOP", machine.DiStop).Export());
                    sw.WriteLine(new SectionWriter<DiEx>("RESET", machine.DiReset).Export());
                    sw.WriteLine();


                    sw.WriteLine(new SectionWriter<DoEx>("LIGHTGREEN", machine.DoLightGreen).Export());
                    sw.WriteLine(new SectionWriter<DoEx>("LIGHTYELLOW", machine.DoLightYellow).Export());
                    sw.WriteLine(new SectionWriter<DoEx>("LIGHTRED", machine.DoLightRed).Export());
                    sw.WriteLine(new SectionWriter<DoEx>("BUZZER", machine.DoBuzzer).Export());
                    sw.WriteLine();


                    sw.WriteLine(new SectionWriter<AxisEx>("AXIS", machine.AxisExs).Export());
                    sw.WriteLine(new SectionWriter<PlatformEx>("PLATFORM", machine.Platforms).Export());
                    sw.WriteLine();
                    sw.WriteLine(new SectionWriter<IDevice>("DEVICE", machine.Devices).Export());
                    sw.WriteLine();

                    sw.WriteLine(new SectionWriter<Station>("STATION", machine.Stations).Export());
                }
            }
        }


        public static string SerializeToString(this StateMachine machine)
        {
            var sb = new StringBuilder();

            sb.AppendLine(new SectionWriter<MotionCardWrapper>("MOTION", machine.MotionExs).Export());
            sb.AppendLine(new SectionWriter<DiEx>("DI", machine.DiExs).Export());
            sb.AppendLine(new SectionWriter<DoEx>("DO", machine.DoExs).Export());
            sb.AppendLine(new SectionWriter<CylinderEx>("CY", machine.CylinderExs).Export());
            sb.AppendLine(new SectionWriter<VioEx>("VIO", machine.VioExs).Export());
            sb.AppendLine();

            sb.AppendLine(new SectionWriter<DiEx>("ESTOP", machine.DiEstop).Export());
            sb.AppendLine(new SectionWriter<DiEx>("START", machine.DiStart).Export());
            sb.AppendLine(new SectionWriter<DiEx>("STOP", machine.DiStop).Export());
            sb.AppendLine(new SectionWriter<DiEx>("RESET", machine.DiReset).Export());
            sb.AppendLine();

            sb.AppendLine(new SectionWriter<DoEx>("LIGHTGREEN", machine.DoLightGreen).Export());
            sb.AppendLine(new SectionWriter<DoEx>("LIGHTYELLOW", machine.DoLightYellow).Export());
            sb.AppendLine(new SectionWriter<DoEx>("LIGHTRED", machine.DoLightRed).Export());
            sb.AppendLine(new SectionWriter<DoEx>("BUZZER", machine.DoBuzzer).Export());
            sb.AppendLine();


            sb.AppendLine(new SectionWriter<AxisEx>("AXIS", machine.AxisExs).Export());
            sb.AppendLine(new SectionWriter<PlatformEx>("PLATFORM", machine.Platforms).Export());
            sb.AppendLine();
            sb.AppendLine(new SectionWriter<IDevice>("DEVICE", machine.Devices).Export());
            sb.AppendLine();

            sb.AppendLine(new SectionWriter<Station>("STATION", machine.Stations).Export());

            return sb.ToString();
        }





    }
}