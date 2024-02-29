using System;
using System.Text;

public class NumberFormatter
{
    [System.Serializable]
    public struct Params
    {
        public int digit;
        public NumberType numberType;
        public HeadType headType;
        public FootType footType;
        public ZeroType zeroType;
    }

    public enum NotationType
    {
        named,
        scientific,
    }

    public enum NumberType
    {
        decimalNumber, //����
        integerNumber,
        time,
        mul100,
    }

    public enum HeadType
    {
        none,
        add,
        subtract,
        mul,
    }
    public enum FootType
    {
        none,
        percent,
        perSec,
        mlPerSec,
        perLeaf
    }

    public enum ZeroType
    {
        number,
        free,
    }

    public static NotationType notationType = NotationType.named;

    public static Params defaultParams
        = new Params()
        {
            digit = 2,
            footType = FootType.none,
            headType = HeadType.none,
            numberType = NumberType.decimalNumber,
            zeroType = ZeroType.number,
        };

    public static string Text(double number) => Text(number, defaultParams);
    public static string Text(double number, Params _params)
    {
        string txt = "";

        if(_params.zeroType == ZeroType.free && number == 0)
        {
            txt += ZeroText(_params.zeroType);
            return txt;
        }

        //head
        txt += HeadText(_params.headType);
        //number
        txt += NumberText(number, _params.numberType, _params.digit); //�\�L�̕ύX�͖��Ή�
        //foot
        txt += FootText(_params.footType);

        return txt;
    }

    static string NumberText(double number, NumberType type, int digit)
    {
        if (type == NumberType.time) return FormatTime_DoubleSymbol(number, digit);
        else
        {
            if (notationType == NotationType.named)
            {
                if (type is NumberType.decimalNumber) return DoubleToStringWithSuffix(number, digit);
                else if (type is NumberType.integerNumber) return DoubleToStringWithSuffix(number, digit, true);
                else if (type is NumberType.mul100) return DoubleToStringWithSuffix(number * 100, digit);
            }
            else if (notationType == NotationType.scientific)
            {
                if (type is NumberType.decimalNumber) return DoubleToStringScientific(number, digit);
                else if (type is NumberType.integerNumber) return DoubleToStringScientific(number, digit, true);
                else if (type is NumberType.mul100) return DoubleToStringScientific(number * 100, digit);
            }
        }

        return "unexpected setting";
    }

    static string HeadText(HeadType type)
    {
        switch (type)
        {
            case HeadType.none: return "";
            case HeadType.add: return "+ ";
            case HeadType.subtract: return "- ";
            case HeadType.mul: return "× ";
            default: return "";
        }
    }

    static string FootText(FootType type)
    {
        switch (type)
        {
            case FootType.none: return "";
            case FootType.percent: return " %";
            case FootType.perSec: return " / s";
            case FootType.mlPerSec: return " ml/s";
            case FootType.perLeaf: return " / leaf";
            default: return "";
        }
    }

    static string ZeroText(ZeroType type)
    {
        switch (type)
        {
            case ZeroType.number: return "0";
            case ZeroType.free: return "FREE";
            default: return "";
        }
    }

    public static string DoubleToStringWithSuffix(double num, int digits, bool isInteger = false)
    {
        string[] digit = new string[]
        {
            "", "K","M","B","T","Qa","Qi","Sx","Sp","Oc","No","Dc",
            "Ud","Dd","Td","Qad","Qid","Sxd","Spd","Ods","Nod","Vg","Uvg","Dvg","Tvg","Qavg","Qivg","Sxvg","Spvg","Ocvs","Novg",
            "Tg","Utg","Dtg","Ttg","Qatg","Qitg","Sxts","Sptg","Octg","Notg","Qag", "Uqag", "Dqag", "Tqag", "Qaqag", "Qiqag", "Sxqag", "Spqag", "Ocqag", "Noqag",
            "Qig", "UQig", "DQig", "TQig", "QaQig", "QiQig", "SxQig", "SpQig", "OcQig", "NoQig", "Sxg", "USxg", "DSxg", "TSxg", "QaSxg", "QiSxg", "SxSxg", "SpSxg", "OcSxg", "NoSxg",
            "Spg", "USpg", "DSpg", "TSpg", "QaSpg", "QiSpg", "SxSpg", "SpSpg", "OcSpg", "NoSpg", "Ocg", "UOcg", "DOcg", "TOcg", "QaOcg", "QiOcg", "SxOcg", "SpOcg", "OcOcg", "NoOcg",
            "Nog", "UNog", "DNog", "TNog", "QaNog", "QiNog", "SxNog", "SpNog", "OcNog", "NoNog", "C", "Uc"
        };
        if (num is double.PositiveInfinity) return "Infinity";
        if (num is double.NegativeInfinity) return "-Infinity";

        if (num < 0)
            return "-" + DoubleToStringWithSuffix(-num, digits);

        if (num < 1000)
        {
            if (isInteger) return ((int)num).ToString();
            else return Math.Round(num, digits).ToString();
        }


        int order = (int)Math.Floor(Math.Log10(num) / 3);
        if(order < 0)
        {
            if (isInteger) return ((int)num).ToString();
            else return Math.Round(num, digits).ToString();
        }
        if (order < digit.Length)
        {
            double divisor = Math.Pow(10, order * 3);
            double truncated = Math.Truncate(num / divisor * Math.Pow(10, digits)) / Math.Pow(10, digits);
            return truncated.ToString() + digit[order];
        }
        else
        {
            double divisor = Math.Pow(10, (digit.Length - 1) * 3);
            double truncated = Math.Truncate(num / divisor * Math.Pow(10, digits)) / Math.Pow(10, digits);
            return truncated.ToString() + digit[digit.Length - 1];
        }
    }

    public static string DoubleToStringScientific(double num, int sigDigits, bool isInteger = false)
    {
        if (num is double.PositiveInfinity) return "Infinity";
        if (num is double.NegativeInfinity) return "-Infinity";

        if (num < 0)
            return "-" + DoubleToStringScientific(-num, sigDigits);

        if (num < 1000)
        {
            if (isInteger) return ((int)num).ToString();
            else return Math.Round(num, sigDigits).ToString();
        }

        double absNum = Math.Abs(num);
        int exponent = (int)Math.Floor(Math.Log10(absNum));
        double mantissa = absNum / Math.Pow(10, exponent);

        if (sigDigits > 0)
        {
            mantissa = Math.Round(mantissa, sigDigits, MidpointRounding.AwayFromZero);
            if (mantissa >= 10)
            {
                mantissa /= 10;
                exponent++;
            }
        }

        if (exponent == 0)
        {
            return mantissa.ToString("F" + (sigDigits > 0 ? sigDigits : 0));
        }
        else
        {
            string exponentStr = exponent.ToString();
            if (exponent < 0)
            {
                exponentStr = "e-" + (-exponent).ToString();
            }
            else if (exponent > 0)
            {
                exponentStr = "e+" + exponentStr;
            }
            return mantissa.ToString("F" + (sigDigits > 0 ? sigDigits : 0)) + exponentStr;
        }
    }

    public static string FormatTime_DoubleSymbol(double seconds, int decimals = 2)
    {
        if (seconds is double.PositiveInfinity) return "Infinity";
        if (seconds is double.NegativeInfinity) return "-Infinity";

        string[] units = { "s", "m", "h", "d", "y" };
        double[] dividers = { 60.0, 60.0, 24.0, 7.0, 365.0, double.MaxValue };
        int index = 0;

        while (index < dividers.Length && seconds >= dividers[index])
        {
            seconds /= dividers[index];
            index++;
        }

        if (index == 0)
        {
            return $"{Math.Round(seconds, decimals)}{units[0]}";
        }
        else if (index == units.Length - 1)
        {
            return $"{Math.Round(seconds, decimals)}{units[index]}";
        }
        else
        {
            int whole = (int)Math.Floor(seconds);
            double fraction = seconds - whole;

            if (whole > 0)
            {
                return $"{whole}{units[index]} {Math.Round(fraction * dividers[index - 1], decimals)}{units[index - 1]}";
            }
            else
            {
                return $"{Math.Round(fraction * dividers[index - 1], decimals)}{units[index - 1]}";
            }
        }
    }

    public static string FormatTime_SingleSymbol(double seconds)
    {
        if (seconds is double.PositiveInfinity) return "Infinity";
        if (seconds is double.NegativeInfinity) return "-Infinity";

        string[] units = { "s", "m", "h", "d", "y" };
        double[] dividers = { 60.0, 60.0, 24.0, 365.0, double.MaxValue };
        int index = 0;

        while (index < dividers.Length && seconds >= dividers[index])
        {
            seconds /= dividers[index];
            index++;
        }

        int whole = (int)Math.Floor(seconds);

        return $"{whole}{units[index]}";
    }

    public static string FormatTime(double seconds)
    {
        const int SecondsPerMinute = 60;
        const int MinutesPerHour = 60;
        const int HoursPerDay = 24;
        const int DaysPerYear = 365;  // Simplified; not accounting for leap years

        int years = (int)(seconds / (DaysPerYear * HoursPerDay * MinutesPerHour * SecondsPerMinute));
        seconds -= years * DaysPerYear * HoursPerDay * MinutesPerHour * SecondsPerMinute;

        int days = (int)(seconds / (HoursPerDay * MinutesPerHour * SecondsPerMinute));
        seconds -= days * HoursPerDay * MinutesPerHour * SecondsPerMinute;

        int hours = (int)(seconds / (MinutesPerHour * SecondsPerMinute));
        seconds -= hours * MinutesPerHour * SecondsPerMinute;

        int minutes = (int)(seconds / SecondsPerMinute);
        seconds -= minutes * SecondsPerMinute;

        StringBuilder sb = new StringBuilder();
        if (years > 0) sb.AppendFormat("{0}y ", years);
        if (days > 0 || sb.Length > 0) sb.AppendFormat("{0}d ", days);
        if (hours > 0 || sb.Length > 0) sb.AppendFormat("{0}h ", hours);
        if (minutes > 0 || sb.Length > 0) sb.AppendFormat("{0}m ", minutes);
        sb.AppendFormat("{0}s", seconds);

        return sb.ToString().Trim();
    }
}