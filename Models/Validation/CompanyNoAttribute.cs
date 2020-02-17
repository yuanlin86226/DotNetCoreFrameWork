using System;
using System.ComponentModel.DataAnnotations;
using Utils;

namespace Models.Validation
{
    public class CompanyNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string CompanyNo = (string)value;
            IsNumberUtils NumberCheck = new IsNumberUtils();

            if (CompanyNo.Length != 8)
                return false;

            if (NumberCheck.IsNumber(CompanyNo) == false)
                return false;

            if (CompanyNumberCheck(CompanyNo) == false)
                return false;

            return true;
        }


        /*項 目 計 算 方 法 說 明 
          統一編號 0 4 5 9 5 2 5 7 　 
          邏輯乘數 1 2 1 2 1 2 4 1 兩數上下對應相乘 
          乘   積 0 8 5 1 5 4 2 7
                       8     0   乘積直寫並上下相加
          -------------------------------------------                
          乘積之和 0 8 5 9 5 4 2 7 將相加之和再相加 
          　      0+8+5+9+5+4+2+7=40 　 
          最後結果, 40 能被 10 整除, 故 04595257 符合邏輯。 
          
          
          若第七位數字為 7 時
          統一編號 1 0 4 5 8 5 7 5 倒數號二位為 7 
          邏輯乘數 1 2 1 2 1 2 4 1 兩數上下對應相乘 
          乘    積 1 0 4 1 8 1 2 5
                         0   0 8   乘積直寫並上下相加
          --------------------------------------------               
          乘積之和 1 0 4 1 8 1 1 5
                              0   再相加時最後第二位數取 0 或 1 均可。 
          　      1+0+4+1+8+1+1+5=21 　 
          　      1+0+4+1+8+1+0+5=20 　 
          最後結果中, 20 能被 10 整除, 故 10458575 符合邏輯。 
        */

        public bool CompanyNumberCheck(string CompanyNo)
        {
            string[] CompanyNoList = CompanyNo.Split("");
            double[] Logical = new double[] { 1, 2, 1, 2, 1, 2, 4, 1 };
            double[] Product = new double[8];
            double CheckSum = 0;

            for (int i = 0; i < Logical.Length; i++)
            {
                Product[i] = double.Parse(CompanyNoList[i]) * Product[i];
            }

            if (CompanyNoList[6] != "7")
            {
                for (int i = 0; i < Product.Length; i++)
                {
                    if (Product[i] > 10)
                        Product[i] = (Product[i] % 10) + Math.Floor(Product[i] / 10);
                    CheckSum += Product[i];
                }
                if (CheckSum % 10 != 0)
                    return false;
            }
            else
            {
                for (int i = 0; i < Product.Length; i++)
                {
                    if (Product[i] > 10 && i != 6)
                        Product[i] = (Product[i] % 10) + Math.Floor(Product[i] / 10);

                    if (i == 6)
                        Product[i] = 1;

                    CheckSum += Product[i];
                }
                if (CheckSum % 10 != 0 || (CheckSum - 1) % 10 != 0)
                    return false;
            }

            return true;
        }
    }
}