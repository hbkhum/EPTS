namespace Devices.VisaCom.Core
{
    internal static class MeasureType
    {
        public static string EnableOutput(InputType command)
        {
            var sType = "";
            switch (command)
            {
                case InputType.TurnOn:
                    sType = "OUTput ON";
                    break;
                case InputType.TurnOff:
                    sType = "OUTput OFF";
                    break;
            }
            return sType;
        }
        public static string SetMeasure(MeasType command)
        {

            var sType = "";
            switch (command)
            {
                case MeasType.VoltDc:
                    sType = "VOLT:DC";
                    break;
                case MeasType.VoltAc:
                    sType = "VOLT:AC";
                    break;
                case MeasType.Res:
                    sType = "RES 100000,10,";
                    break;
                case MeasType.Fres:
                    sType = "FRES";
                    break;
                case MeasType.CurrDc:
                    sType = "CURR:DC";
                    break;
                case MeasType.CurrAc:
                    sType = "CURR:AC";
                    break;
                case MeasType.Freq:
                    sType = "FREQ";
                    break;
                case MeasType.Per:
                    sType = "PER";
                    break;
            }
            return "CONF:" + sType;
        }

        public static string SetSenSe(MeasType command)
        {
            var sType = "";
            switch (command)
            {
                case MeasType.VoltDc:
                    sType = "VOLT:DC";
                    break;
                case MeasType.VoltAc:
                    sType = "VOLT:AC";
                    break;
                case MeasType.Res:
                    sType = "RES 100000,10,";
                    break;
                case MeasType.Fres:
                    sType = "FRES";
                    break;
                case MeasType.CurrDc:
                    sType = "CURR:DC";
                    break;
                case MeasType.CurrAc:
                    sType = "CURR:AC";
                    break;
                case MeasType.Freq:
                    sType = "FREQ";
                    break;
                case MeasType.FreqRangLow:
                    sType = "FREQ:RANG:LOW 3";
                    break;
                case MeasType.Per:
                    sType = "PER";
                    break;
            }
            return "SENS:FUNC " + (char)34 + sType + (char)34;
        }

        public static string GetMeasure(MeasType command)
        {
            var sType = "";
            switch (command)
            {
                case MeasType.VoltDc:
                    sType = "VOLT:DC?";
                    break;
                case MeasType.VoltAc:
                    sType = "VOLT:AC?";
                    break;
                case MeasType.Res:
                    sType = "RES? 100000,10,";
                    break;
                case MeasType.Fres:
                    sType = "FRES?";
                    break;
                case MeasType.CurrDc:
                    sType = "CURR:DC?";
                    break;
                case MeasType.CurrAc:
                    sType = "CURR:AC?";
                    break;
                case MeasType.Freq:
                    sType = "FREQ?";
                    break;
                case MeasType.Per:
                    sType = "PER?";
                    break;
            }
            return "MEAS:" + sType;
        }

    }
}
