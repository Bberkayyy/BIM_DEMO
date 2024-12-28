using BusinessLogicLayer.BusinessRules.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Extensions;

public static class GenerateClass
{
    private static Random random = new Random();
    public static long GenerateRandomUniqueBarcodeNo(int shorCode)
    {
        string firstDigit = "8";
        string randomDigits = "";
        for (int i = 0; i < 6; i++)
            randomDigits += random.Next(0, 10).ToString();
        string lastDigits = shorCode.ToString();
        long barcodeNo = long.Parse(firstDigit + randomDigits + lastDigits);
        return barcodeNo;
    }
    public static int GenerateRandomUniqueShortCode(short categoryNo)
    {
        string frontDigits = random.Next(1000, 10000).ToString();
        return int.Parse(categoryNo.ToString() + frontDigits);
    }
    public static short GenerateRandomUniqueCategoryNo(this ICategoryRules categoryRules)
    {
        short categoryNo;
        do { categoryNo = (short)random.Next(100, 1000); }
        while (categoryRules.CategoryNoMustBeUnique(categoryNo));
        return categoryNo;
    }
}