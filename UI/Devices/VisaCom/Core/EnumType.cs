namespace Devices.VisaCom.Core
{
    public  enum MeasType
    {
        VoltDc = 0,
        VoltAc = 1,
        Res = 2,
        Fres = 3,
        CurrDc = 4,
        CurrAc = 5,
        Freq = 6,
        Per = 7,
        FreqRangLow = 8,
    }
    public  enum InputType
    {
        TurnOn = 0,
        TurnOff = 1
    }
    public  enum FilterType
    {
        Lf3Hz = 0,
        Lf20Hz = 1,
        Lf200Hz = 2
    }


}
