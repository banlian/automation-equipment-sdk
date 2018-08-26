namespace Automation.FrameworkExtension.platforms.points
{
    public interface IPos
    {
        int Index { get; set; }
        string Name { get; set; }
        string Description { get; set; }

        double[] Data();

        double Distance(IPos pos);
    }
}