namespace DeviceEvents;

public class RobotX2EventStruct
{
    public float temperature { get; set; }
    public void printDeviceInfo()
    {
        Console.WriteLine($"Current Temperature: {this.temperature}");
        //return (0);
    }
}
