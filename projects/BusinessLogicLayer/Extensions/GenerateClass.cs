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
    public static long GenerateRandomUniqueBarcodeNo(this IProductRules productRules, int shorCode)
    {
        long barcodeNo;
        string firstDigit = "8";
        string randomDigits = "";
        for (int i = 0; i < 6; i++)
            randomDigits += random.Next(0, 10).ToString();
        string lastDigits = shorCode.ToString();
        do { barcodeNo = long.Parse(firstDigit + randomDigits + lastDigits); }
        while (productRules.BarcodeNoMustBeUnique(barcodeNo));
        return barcodeNo;
    }
    public static int GenerateRandomUniqueShortCode(this IProductRules productRules, short categoryNo)
    {
        int shortCode;
        string frontDigits = random.Next(1000, 10000).ToString();
        do { shortCode = int.Parse(categoryNo.ToString() + frontDigits); }
        while (productRules.ShortCodeMustBeUnique(shortCode));
        return shortCode;
    }
    public static short GenerateRandomUniqueCategoryNo(this ICategoryRules categoryRules)
    {
        short categoryNo;
        do { categoryNo = (short)random.Next(100, 1000); }
        while (categoryRules.CategoryNoMustBeUnique(categoryNo));
        return categoryNo;
    }
    public static string GenerateStoreNo(this IStoreRules storeRules, int cityCode)
    {
        string formattedCityCode = cityCode.ToString("D2");
        string storeNo;
        do { storeNo = formattedCityCode + random.Next(1000, 10000).ToString(); } while (storeRules.StoreNoMustBeUnique(storeNo));
        return storeNo;
    }
    public static int GenerateRandomUniqueUserCode(this IUserRules userRules)
    {
        int userCode;
        do { userCode = random.Next(100000, 1000000); }
        while (userRules.UserCodeMustBeUnique(userCode));
        return userCode;
    }
    public static int GenerateRandomUserPassword()
    {
        return random.Next(10000000, 100000000);
    }
}