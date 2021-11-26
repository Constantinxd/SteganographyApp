using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Steganography.Methods
{
    public class AdditionalMethods
    {
        //Получить из массива бит массив байт
        public static byte[] GetByteArrayFromBitArray(BitArray arr)
        {
            byte[] bytes = new byte[arr.Length / 8];

            for (int i = 0; i + 8 <= arr.Length; i += 8)
            {
                byte sum = 0;

                for (int j = 0; j < 8; j++)
                    if (arr[i + j]) sum += Convert.ToByte(1 << j);

                bytes[i / 8] = sum;
            }

            return bytes;
        }

        //Получить число типа int, из массива бит
        public static int GetIntFromBitArray(BitArray arr)
        {
            int sum = 0;

            for (int i = 0; i < arr.Count; i++)
                if (arr[i]) sum += Convert.ToInt32(1 << i);

            return sum;
        }

        //Получить число типа int, из массива бит, начиная с позиции pos, длиной в len
        public static int GetIntFromBitArray(BitArray arr, int pos, int len)
        {
            int sum = 0;
            int i = 0;

            for (int k = pos; k < pos + len; k++)
            {
                if (arr[k]) sum += Convert.ToInt32(1 << i);
                i++;
            }

            return sum;
        }

        //Получить число типа int, из массива бит, представленного строкой arr, начиная с 0, длиной в len
        public static int GetIntFromStrBitArray(string arr, int len)
        {
            int sum = 0;
            int i = 0;

            for (int k = 0; k < len; k++)
            {
                if (arr[k] == '1') sum += Convert.ToInt32(1 << i);
                i++;
            }

            return sum;
        }

        //Получить массив байт, из массива бит, представленного строкой arr, начиная с 0, длиной в len
        public static byte[] GetByteArrayFromStrBitArray(string arr, int len)
        {
            byte[] bytes = new byte[len / 8];

            for (int i = 0; i + 8 <= len; i += 8)
            {
                byte sum = 0;

                for (int j = 0; j < 8; j++)
                    if (arr[j + i] == '1') sum += Convert.ToByte(1 << j);

                bytes[i / 8] = sum;
            }

            return bytes;
        }

        //Получить двоичное число в виде строки, из числа num типа int, длиной в len
        public static string GetStrBitArrayFromInt(int num, int len)
        {
            string str = "";

            while (num != 0)
            {
                if (num % 2 == 0) str += "0";
                else str += "1";

                num /= 2;
            }

            if (len > str.Length)
                while (len > str.Length) str += "0";

            return str;
        }
    }
}
