using System.Collections.Generic;

public static class Formatter
{
    public enum CashIconType
    {
        None,
        Stack,
        Plus
    }

    private static List<(double top, double del, string suffix, string name)> orderMap = new List<(double, double, string, string)>()
        {
            ( 1e3,  1,     "",  ""),
            ( 1e6,  1e3,  "K", "thousand"),
            ( 1e9,  1e6,  "M", "million"),
            ( 1e12, 1e9,  "B", "billion"),
            ( 1e15, 1e12, "t", "trillion"),
            ( 1e18, 1e15, "q", "quadrillion"),
            ( 1e21, 1e18, "Q", "quintillion"),
            ( 1e24, 1e21, "s", "sextillion"),
            ( 1e27, 1e24, "S", "septillion"),
            ( 1e30, 1e27, "o", "octillion"),
            ( 1e33, 1e30, "n", "nonillion"),
            ( 1e36, 1e33, "d", "decillion"),
            ( 1e39, 1e36, "U", "undecilion"),
            ( 1e42, 1e39, "D", "duodecillion"),
            ( 1e45, 1e42, "T", "tredecillion"),
            ( 1e48, 1e45, "Qt", "quattuordecillion"),
            ( 1e51, 1e48, "Sd", "sexdecillion"),
            ( 1e54, 1e51, "St", "septendecillion"),
            ( 1e57, 1e54, "O", "octodecillion"),
            ( 1e60, 1e57, "N", "novemdecillion"),
            ( 1e63, 1e60, "v", "vigintillion"),
            ( 1e66, 1e63, "c", "unvigintillion")
        };

    public static string FormatCashWithIcon(double value, bool addSuffix = true, bool verboseSuffix = false, CashIconType iconType = CashIconType.Stack)
    {
        if (iconType == CashIconType.None)
        {
            return FormatCash(value, addSuffix, verboseSuffix);
        }
        else
        {
            var sprite = iconType == CashIconType.Stack ? 1 : 0;
            return $"<sprite={sprite}> {FormatCash(value, addSuffix, verboseSuffix)}";
        }
    }

    public static string FormatCash(double value, bool addSuffix = true, bool verboseSuffix = false)
    {
        for (int i = 0; i < orderMap.Count; ++i)
        {
            if (value < orderMap[i].top)
            {
                return
                    addSuffix ?
                        verboseSuffix ?
                        $"{value / orderMap[i].del:0.00} {orderMap[i].name}" :
                        $"{value / orderMap[i].del:0.00}{orderMap[i].suffix}" :
                    $"{value / orderMap[i].del:0.00}";
            }
        }

        return value.ToString("0.00e0");
    }

    public static string FormatGems(double value, bool addSuffix = true)
    {
        if (addSuffix)
        {
            return $"{value:0}G";
        }
        else
        {
            return $"{value:0}";
        }
    }

    public static string GetNumberName(double value)
    {
        for (int i = 0; i < orderMap.Count; ++i)
        {
            if (value < orderMap[i].top)
            {
                return orderMap[i].name;
            }
        }

        return "";
    }
}

