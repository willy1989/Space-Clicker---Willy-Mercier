using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static string ConvertNumberToShortText(float number)
    {
        if (number < 1000f)
        {
            number = Mathf.Round(number);

            return number.ToString();
        }
            
        else if (number >= 1000f && number < 100000f)
        {
            number /= 1000f;

            number = Mathf.Round(number * 10f) / 10f;

            return number.ToString() + " k";
        }

        else if (number >= 100000f && number < 1000000f)
        {
            number /= 1000f;

            number = Mathf.Round(number / 1000f);

            return number.ToString() + " k";
        }

        number = Mathf.Round(number / 1000000f);
        return number.ToString() + " m";
    }
}
