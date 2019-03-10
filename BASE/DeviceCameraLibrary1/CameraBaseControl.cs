using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Automation.FrameworkExtension.deviceDriver;
using DeviceCameraLibrary1;

namespace Automation.Base.DeviceCameraLibrary1
{
    public partial class CameraBaseControl : UserControl, IDeviceControl<ICamera>
    {
        public CameraBaseControl()
        {
            InitializeComponent();
        }

        private ICamera _camera;
        public void LoadDevice(ICamera device)
        {
            if (device != null)
            {
                groupBoxDev.Text = device.ToString();

                _camera = device;
            }
        }

        public void UserActivate()
        {
            throw new NotImplementedException();
        }

        public void UserDeactivate()
        {
            throw new NotImplementedException();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (_camera != null)
                {
                    _camera.Initialize();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (_camera != null)
                {
                    _camera.Terminate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void buttonSnap_Click(object sender, EventArgs e)
        {
            try
            {
                if (_camera != null)
                {
                    var img = _camera.Snap();
                    UpdateImage(img);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void UpdateImage(Bitmap img)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Bitmap>(UpdateImage), img);
            }
            else
            {
                pictureBoxImage.Image = img;
                pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }



    }
}
