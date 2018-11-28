namespace Automation.FrameworkExtension.motionDriver
{

    /// <summary>
    /// motion card must be implemented as this interface
    /// </summary>
    public interface IMotionCard
    {

        string Name { get; set; }

        int DeviceID { get; set; }

        bool Initialize();
        bool Terminate();

        bool LoadParams(string configFileName, params object[] objects);


        bool ClearAlarm(int index, int i);



        int GetDo(int index, int port, out int status);
        int SetDo(int index, int port, int status);

        int GetDi(int index, int port, out int status);
        int SetDi(int index, int port, int status);


        int GetEncPos(int index, int axis, ref double d);
        int SetEncPos(int index, int axis, int pos);
        int GetCmdPos(int index, int axis, ref double d);
        int SetCmdPos(int index, int axis, int pos);


        int Servo(int index, int axis, bool enable);
        int AxisAbsMove(int index, int axis, int pos, int vel);
        int AxisRelMove(int index, int axis, int step, int vel);
        bool IsAxisStop(int index, int axis);
        int AxisStop(int index, int axis);


        int SetHomeVel(int index, int axis, int vel);
        int AxisHomeMove(int index, int axis);
        bool IsAxisHmv(int index, int axis);


        bool IsAxisServo(int index, int axis);
        bool IsAxisAlarm(int index, int axis);
        bool IsAxisEmg(int index, int axis);
        bool IsAxisAstp(int index, int axis);
        bool IsAxisInp(int index, int axis);
        bool IsAxisMel(int index, int axis);
        bool IsAxisPel(int index, int axis);
        bool IsAxisOrg(int index, int axis);
    }
}