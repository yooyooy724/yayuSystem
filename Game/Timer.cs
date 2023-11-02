public class SimpleTimer : IUpdatePerTime, ITime
{
    public float time { get; private set; }
    public void UpdatePerTime(float time)
    {
        this.time += time;
    }

    public void Reset()
    {
        time = 0;
    }
}

public interface ITime
{
    float time { get; }
}