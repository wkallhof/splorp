namespace Splorp.Core;

public interface ITimer
{
    uint GetTicks();
    void Delay(uint milliseconds);
}