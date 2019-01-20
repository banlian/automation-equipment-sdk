using Automation.FrameworkExtension.elements;
using Automation.FrameworkExtension.motionDriver;

namespace Automation.FrameworkExtension.elementsInterfaces
{
    public interface ICylinderEx : IElement
    {
        CylinderType Type { set; get; }
        string Description { set; get; }
        bool Enable { set; get; }


        string DriverName1 { set; get; }
        int DiOrg { set; get; }
        int DiWork { set; get; }
        string DriverName2 { set; get; }
        int DoOrg { set; get; }
        int DoWork { set; get; }

        /// <summary>
        /// Cylinder Di Driver
        /// </summary>
        MotionCardWrapper Driver1 { get; }
        /// <summary>
        /// Cylinder Do Driver
        /// </summary>
        MotionCardWrapper Driver2 { get; }

    
      


        IDiEx[] GetDiExs();
        IDoEx[] GetDoExs();
    }
}