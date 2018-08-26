using System;
using System.Windows.Forms;

namespace Automation.FrameworkExtension.stateMachine
{
    public partial class StationStateControl : UserControl
    {

        public StateMachine Machine;

        public Station Station;

        public StationStateControl()
        {
            InitializeComponent();
        }

        private void StationStateControl_Load(object sender, EventArgs e)
        {
            if (Station != null)
            {
                dataGridViewStation.Columns.Clear();
                dataGridViewStation.Columns.Add("Station", "Station");
                dataGridViewStation.Columns.Add("State", "State");


                dataGridViewStation.Rows.Clear();

                var i = dataGridViewStation.Rows.Add(1);
                dataGridViewStation.Rows[i].Cells[0].Value = "Machine";
                dataGridViewStation.Rows[i].Cells[1].Value = "Machine";
                i = dataGridViewStation.Rows.Add(1);
                dataGridViewStation.Rows[i].Cells[0].Value = "MachineState";
                dataGridViewStation.Rows[i].Cells[1].Value = Machine.State;
                i = dataGridViewStation.Rows.Add(1);
                dataGridViewStation.Rows[i].Cells[0].Value = "MachineAutoState";
                dataGridViewStation.Rows[i].Cells[1].Value = Machine.AutoState;

                i = dataGridViewStation.Rows.Add(1);
                dataGridViewStation.Rows[i].Cells[0].Value = "Station";
                dataGridViewStation.Rows[i].Cells[1].Value = Station.Name;
                i = dataGridViewStation.Rows.Add(1);
                dataGridViewStation.Rows[i].Cells[0].Value = "StationState";
                dataGridViewStation.Rows[i].Cells[1].Value = Station.State;
                i = dataGridViewStation.Rows.Add(1);
                dataGridViewStation.Rows[i].Cells[0].Value = "StationAutoState";
                dataGridViewStation.Rows[i].Cells[1].Value = Station.AutoState;


                foreach (var stationTask in Station.Tasks)
                {
                    var index = dataGridViewStation.Rows.Add(1);
                    dataGridViewStation.Rows[index].Cells[0].Value = stationTask.Value.Name;
                    dataGridViewStation.Rows[index].Cells[1].Value = stationTask.Value.State;
                }

                timer1.Start();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Station != null)
            {
                dataGridViewStation.Rows[1].Cells[1].Value = Machine.State;
                dataGridViewStation.Rows[2].Cells[1].Value = Machine.AutoState;
                dataGridViewStation.Rows[3].Cells[1].Value = Station.Name;
                dataGridViewStation.Rows[4].Cells[1].Value = Station.State;
                dataGridViewStation.Rows[5].Cells[1].Value = Station.AutoState;


                int index = 6;
                foreach (var stationTask in Station.Tasks)
                {
                    dataGridViewStation.Rows[index].Cells[0].Value = stationTask.Value.Name;
                    dataGridViewStation.Rows[index].Cells[1].Value = stationTask.Value.State;
                    index++;
                }
            }
        }
    }
}
