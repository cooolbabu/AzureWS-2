namespace DeviceEvents;

public class RobotX2EventStruct
{
    public string DataStr { get; set; }
    public void PrintDeviceInfo()
    {
        Console.WriteLine($"Current Temperature: {this.DataStr}");
        //return (0);
    }
}
