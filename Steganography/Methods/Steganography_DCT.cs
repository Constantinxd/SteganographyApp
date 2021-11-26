using System;
using System.Collections;
using System.Drawing;
using System.Text;

namespace Steganography.Methods
{
    class Steganography_DCT : AdditionalMethods, ISteganographyMethod
    {
        private const int BYTE = 8;
        private const int x1 = 5, y1 = 7, x2 = 6, y2 = 7; // координаты используемых коэффициентов (x1, y1) и (x2, y2) блока 8x8

        //Текущая позиция на изображении
        struct Position
        {
            public int width;
            public int height;
            public int x;
            public int y;

            public void NextPosition()
            {
                if (x + 8 >= width)
                {
                    x = 0;
                    y += 8;
                }
                else x += 8;
            }
        }

        //Получить первые три символа сообщения
        public static string GetSteganographyMethod(string file_path)
        {
            using (Bitmap bmp = new Bitmap(file_path))
            {
                LockBitmap img = new LockBitmap(bmp);
                img.LockBits();

                double[,] block = new double[8, 8];
                double num1, num2;

                Position cur_pos = new Position { width = img.Width, height = img.Height, x = 0, y = 0 };

                //считываем метод
                BitArray method_byte = new BitArray(3 * BYTE);
                for (int i = 0; i < 3 * BYTE; i++)
                {
                    for (int k = 0; k < 8; k++)
                        for (int m = 0; m < 8; m++)
                            block[k, m] = img.GetPixel(cur_pos.x + m, cur_pos.y + k).B;

                    DCT(block); ROUND(block);

                    num1 = (int)Math.Abs(block[x1, y1]);
                    num2 = (int)Math.Abs(block[x2, y2]);

                    if (num1 - num2 > 0) method_byte[i] = false;
                    else method_byte[i] = true;

                    cur_pos.NextPosition();
                }

                img.UnlockBits();
                return Encoding.ASCII.GetString(GetByteArrayFromBitArray(method_byte));
            }
        }

        //Размер сообщения, которое можно сокрыть в изображении данным методом
        public static int GetSize(string file_path)
        {
            using (Bitmap bmp = new Bitmap(file_path))
            {
                return (bmp.Width / 8) * (bmp.Height / 8) / 8;
            }
        }

        //Сокрытие сообщения
        public T Encrypt<T>(string file_path, byte[] file_bytes)
        {
            double[,] block = new double[8, 8];
            double num1, num2;

            Bitmap bmp = new Bitmap(file_path);
            LockBitmap img = new LockBitmap(bmp);
            img.LockBits();

            BitArray file_bits = new BitArray(file_bytes);

            Position cur_pos = new Position { width = img.Width, height = img.Height, x = 0, y = 0 };

            int count = 0;

            while (count != file_bits.Length)
            {
                for (int k = 0; k < 8; k++)
                    for (int m = 0; m < 8; m++)
                        block[k, m] = img.GetPixel(cur_pos.x + m, cur_pos.y + k).B;

                DCT(block);
                ROUND(block);

                num1 = (int)Math.Abs(block[x1, y1]);
                num2 = (int)Math.Abs(block[x2, y2]);

                if (file_bits[count])
                {
                    if (num2 - num1 <= 25)
                        num2 = (block[x2, y2] >= 0) ? num1 + 50 : -1 * (num1 + 50);
                }
                else
                {
                    if (num1 - num2 <= 25)
                        num1 = (block[x1, y1] >= 0) ? num2 + 50 : -1 * (num2 + 50);
                }

                block[x1, y1] = num1;
                block[x2, y2] = num2;

                IDCT(block);
                ROUND(block);

                for (int k = 0; k < 8; k++)
                {
                    for (int m = 0; m < 8; m++)
                    {
                        if (block[k, m] < 0) block[k, m] = 0;
                        if (block[k, m] > 255) block[k, m] = 255;
                        Color px = img.GetPixel(cur_pos.x + m, cur_pos.y + k);
                        img.SetPixel(cur_pos.x + m, cur_pos.y + k, Color.FromArgb(px.A, px.R, px.G, (byte)block[k, m]));
                    }
                }

                ++count;
                cur_pos.NextPosition();
            }

            img.UnlockBits();
            return (dynamic)bmp;
        }

        //Извлечение сообщения
        public Output_file Decrypt(string file_path)
        {
            using (Bitmap bmp = new Bitmap(file_path))
            {
                LockBitmap img = new LockBitmap(bmp);
                img.LockBits();

                double[,] block = new double[8, 8];
                
                double num1, num2;

                Position cur_pos = new Position { width = img.Width, height = img.Height, x = 0, y = 0 };

                //пропускаем метод
                for (int i = 0; i < 3 * BYTE; i++)
                    cur_pos.NextPosition();

                //считываем формат
                BitArray format_byte = new BitArray(4 * BYTE);
                for (int i = 0; i < 4 * BYTE; i++)
                {
                    for (int k = 0; k < 8; k++)
                        for (int m = 0; m < 8; m++)
                            block[k, m] = img.GetPixel(cur_pos.x + m, cur_pos.y + k).B;

                    DCT(block); ROUND(block);

                    num1 = (int)Math.Abs(block[x1, y1]);
                    num2 = (int)Math.Abs(block[x2, y2]);

                    if (num1 - num2 > 0) format_byte[i] = false;
                    else format_byte[i] = true;

                    cur_pos.NextPosition();
                }
                string format = Encoding.ASCII.GetString(GetByteArrayFromBitArray(format_byte));

                //считываем размер строки
                BitArray text_length_byte = new BitArray(4 * BYTE);
                for (int i = 0; i < 4 * BYTE; i++)
                {
                    for (int k = 0; k < 8; k++)
                        for (int m = 0; m < 8; m++)
                            block[k, m] = img.GetPixel(cur_pos.x + m, cur_pos.y + k).B;

                    DCT(block); ROUND(block);

                    num1 = (int)Math.Abs(block[x1, y1]);
                    num2 = (int)Math.Abs(block[x2, y2]);

                    if (num1 - num2 > 0) text_length_byte[i] = false;
                    else text_length_byte[i] = true;

                    cur_pos.NextPosition();
                }
                int text_length = GetIntFromBitArray(text_length_byte);

                byte[] text = new byte[text_length];
                BitArray byte_char = new BitArray(BYTE);
                for (int i = 0; i < text_length * BYTE; i++)
                {
                    for (int k = 0; k < 8; k++)
                        for (int m = 0; m < 8; m++)
                            block[k, m] = img.GetPixel(cur_pos.x + m, cur_pos.y + k).B;

                    DCT(block); ROUND(block);

                    num1 = (int)Math.Abs(block[x1, y1]);
                    num2 = (int)Math.Abs(block[x2, y2]);

                    if (i % BYTE == 0 && i != 0)
                    {
                        text[(i / BYTE) - 1] = Convert.ToByte(GetIntFromBitArray(byte_char));
                        byte_char.SetAll(false);
                    }

                    if (num1 - num2 > 0) byte_char[i % BYTE] = false;
                    else byte_char[i % BYTE] = true;

                    cur_pos.NextPosition();
                }
                text[text_length - 1] = Convert.ToByte(GetIntFromBitArray(byte_char));

                img.UnlockBits();
                return new Output_file { format = (format[0] == '*') ? format.Substring(1, format.Length - 1) : format, file_byte = text };
            }
        }

        //Округление двумерного массива data
        public static void ROUND(double[,] data)
        {
            int rows = data.GetLength(0);
            int cols = data.GetLength(1);

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    data[i, j] = Math.Round(data[i, j]);
        }

        //ДКП преобразование для одномерного массива
        public static void DCT(double[] arr)
        {
            double[] result = new double[arr.Length];
            double sum;

            for (int i = 0; i < arr.Length; i++)
            {
                sum = 0;
                for (int n = 0; n < arr.Length; n++)
                    sum += arr[n] * Math.Cos(((2.0 * n + 1.0) * i * Math.PI) / (2.0 * arr.Length));
                result[i] = Math.Sqrt(2.0 / arr.Length) * sum;
            }

            for (int i = 0; i < arr.Length; i++)
                arr[i] = result[i];

            arr[0] /= Math.Sqrt(2.0);
        }

        //Обратное ДКП преобразование для одномерного массива
        public static void IDCT(double[] arr)
        {
            double[] result = new double[arr.Length];
            double sum;

            for (int i = 0; i < arr.Length; i++)
            {
                sum = arr[0] / Math.Sqrt(2.0);
                for (int n = 1; n < arr.Length; n++)
                    sum += arr[n] * Math.Cos(((2.0 * i + 1.0) * n * Math.PI) / (2.0 * arr.Length));

                result[i] = Math.Sqrt(2.0 / arr.Length) * sum;
            }

            for (int i = 0; i < arr.Length; i++)
                arr[i] = result[i];
        }

        //ДКП преобразование для двумерного массива
        public static void DCT(double[,] arr)
        {
            int rowLen = arr.GetLength(0);
            int colLen = arr.GetLength(1);

            double[] row = new double[colLen];
            double[] col = new double[rowLen];

            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < row.Length; j++)
                    row[j] = arr[i, j];

                DCT(row);

                for (int j = 0; j < row.Length; j++)
                    arr[i, j] = row[j];
            }

            for (int i = 0; i < colLen; i++)
            {
                for (int j = 0; j < col.Length; j++)
                    col[j] = arr[j, i];

                DCT(col);

                for (int j = 0; j < col.Length; j++)
                    arr[j, i] = col[j];
            }
        }

        //Обратное ДКП преобразование для двумерного массива
        public static void IDCT(double[,] arr)
        {
            int rowLen = arr.GetLength(0);
            int colLen = arr.GetLength(1);

            double[] row = new double[colLen];
            double[] col = new double[rowLen];

            for (int i = 0; i < colLen; i++)
            {
                for (int j = 0; j < row.Length; j++)
                    col[j] = arr[j, i];

                IDCT(col);

                for (int j = 0; j < col.Length; j++)
                    arr[j, i] = col[j];
            }

            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < row.Length; j++)
                    row[j] = arr[i, j];

                IDCT(row);

                for (int j = 0; j < row.Length; j++)
                    arr[i, j] = row[j];
            }
        }
    }
}
