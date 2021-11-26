using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Steganography.Methods
{
    class Steganography_PVD : AdditionalMethods, ISteganographyMethod
    {
        private const int BYTE = 8;

        //Текущая позиция на изображении
        struct Position
        {
            public int width;
            public int height;
            public int x;
            public int y;

            public void NextPosition()
            {
                if (x == width - 1)
                {
                    x = 0;
                    ++y;
                }
                else ++x;
            }
        }

        //Таблица диапазонов квантования
        struct Table
        {
            public int Lower;
            public int Upper;
            public int AmountOfBit;

            public void getRegion(int d)
            {
                if (d <= 1)
                {
                    Lower = 0; Upper = 1; AmountOfBit = 1;
                }
                else if (d <= 5)
                {
                    Lower = 2; Upper = 5; AmountOfBit = 2;
                }
                else if (d <= 11)
                {
                    Lower = 6; Upper = 11; AmountOfBit = 2;
                }
                else if (d <= 19)
                {
                    Lower = 12; Upper = 19; AmountOfBit = 3;
                }
                else if (d <= 29)
                {
                    Lower = 20; Upper = 29; AmountOfBit = 3;
                }
                else if (d <= 41)
                {
                    Lower = 30; Upper = 41; AmountOfBit = 3;
                }
                else if (d <= 55)
                {
                    Lower = 42; Upper = 55; AmountOfBit = 3;
                }
                else if (d <= 71)
                {
                    Lower = 56; Upper = 71; AmountOfBit = 4;
                }
                else if (d <= 89)
                {
                    Lower = 72; Upper = 89; AmountOfBit = 4;
                }
                else if (d <= 109)
                {
                    Lower = 90; Upper = 109; AmountOfBit = 4;
                }
                else if (d <= 131)
                {
                    Lower = 110; Upper = 131; AmountOfBit = 4;
                }
                else if (d <= 131)
                {
                    Lower = 110; Upper = 131; AmountOfBit = 4;
                }
                else if (d <= 155)
                {
                    Lower = 132; Upper = 155; AmountOfBit = 5;
                }
                else if (d <= 181)
                {
                    Lower = 156; Upper = 181; AmountOfBit = 5;
                }
                else if (d <= 209)
                {
                    Lower = 182; Upper = 209; AmountOfBit = 5;
                }
                else if (d <= 239)
                {
                    Lower = 210; Upper = 239; AmountOfBit = 5;
                }
                else if (d <= 255)
                {
                    Lower = 240; Upper = 255; AmountOfBit = 4;
                }
            }
        }

        //Получить первые три символа сообщения
        public static string GetSteganographyMethod(string file_path)
        {
            //FastBitmap img = FastBitmap.FromFile(file_path);
            using (Bitmap bmp = new Bitmap(file_path))
            {
                LockBitmap img = new LockBitmap(bmp);
                img.LockBits();

                Position cur_pos = new Position { width = img.Width, height = img.Height, x = -1, y = 0 };
                Table cur_table = new Table();

                byte R, G, B;
                int d, dn, dif1, dif2, lower, upper, t, cur_text_pos = 0;
                string str_bit_arr = "";

                //считываем метод
                while (cur_text_pos < 3 * BYTE)
                {
                    cur_pos.NextPosition();
                    R = img.GetPixel(cur_pos.x, cur_pos.y).R;
                    G = img.GetPixel(cur_pos.x, cur_pos.y).G;
                    B = img.GetPixel(cur_pos.x, cur_pos.y).B;
                    d = Math.Min(Math.Min(Math.Abs(R - G), Math.Abs(G - B)), Math.Abs(R - B));

                    if (d == Math.Abs(R - G))
                    {
                        dif1 = Math.Abs(G - B); dif2 = Math.Abs(R - B);
                    }
                    else if (d == Math.Abs(G - B))
                    {
                        dif1 = Math.Abs(R - G); dif2 = Math.Abs(R - B);
                    }
                    else
                    {
                        dif1 = Math.Abs(R - G); dif2 = Math.Abs(G - B);
                    }

                    cur_table.getRegion(d);
                    lower = cur_table.Lower;
                    upper = cur_table.Upper;
                    t = cur_table.AmountOfBit;

                    if (Math.Abs(dif1 - d) > upper - lower + 1 && Math.Abs(dif2 - d) > upper - lower + 1)
                    {
                        dn = d - lower;
                        cur_text_pos += t;
                        str_bit_arr += GetStrBitArrayFromInt(dn, t);
                    }
                }

                img.UnlockBits();
                return Encoding.ASCII.GetString(GetByteArrayFromStrBitArray(str_bit_arr, 3 * BYTE));
            }
        }

        //Размер сообщения, которое можно сокрыть в изображении данным методом
        public static int GetSize(string file_path, byte[] file_bytes, int messageSize)
        {
            //FastBitmap img = FastBitmap.FromFile(file_path);
            using (Bitmap bmp = new Bitmap(file_path))
            {
                LockBitmap img = new LockBitmap(bmp);
                img.LockBits();

                BitArray file_bits = new BitArray(file_bytes);

                Position cur_pos = new Position { width = img.Width, height = img.Height, x = -1, y = 0 };
                Table cur_table = new Table();

                byte R, G, B;
                int d, dn, b, px1, px2, px3, dif1, dif2, lower, upper, t, cur_text_pos = 0, m;

                while (cur_text_pos < file_bits.Length && (cur_pos.y != img.Height - 1 || cur_pos.x != img.Width - 1))
                {
                    cur_pos.NextPosition();

                    R = img.GetPixel(cur_pos.x, cur_pos.y).R;
                    G = img.GetPixel(cur_pos.x, cur_pos.y).G;
                    B = img.GetPixel(cur_pos.x, cur_pos.y).B;
                    d = Math.Min(Math.Min(Math.Abs(R - G), Math.Abs(G - B)), Math.Abs(R - B));

                    if (d == Math.Abs(R - G))
                    {
                        px1 = R; px2 = G; px3 = B; dif1 = Math.Abs(G - B); dif2 = Math.Abs(R - B);
                    }
                    else if (d == Math.Abs(G - B))
                    {
                        px1 = G; px2 = B; px3 = R; dif1 = Math.Abs(R - G); dif2 = Math.Abs(R - B);
                    }
                    else
                    {
                        px1 = R; px2 = B; px3 = G; dif1 = Math.Abs(R - G); dif2 = Math.Abs(G - B);
                    }

                    cur_table.getRegion(d);
                    lower = cur_table.Lower;
                    upper = cur_table.Upper;
                    t = (cur_table.AmountOfBit + cur_text_pos < file_bits.Length) ? cur_table.AmountOfBit : file_bits.Length - cur_text_pos;

                    b = GetIntFromBitArray(file_bits, cur_text_pos, t);
                    dn = lower + b;
                    m = Math.Abs(dn - d);

                    if (Math.Abs(dif1 - d) > upper - lower + 1 && Math.Abs(dif2 - d) > upper - lower + 1)
                    {
                        if (px1 >= px2 && dn > d)
                        {
                            px1 += Convert.ToInt32(Math.Ceiling(Convert.ToDouble(m) / 2));
                            px2 -= m / 2;
                        }
                        else if (px1 < px2 && dn > d)
                        {
                            px1 -= m / 2;
                            px2 += Convert.ToInt32(Math.Ceiling(Convert.ToDouble(m) / 2));
                        }
                        else if (px1 >= px2 && dn <= d)
                        {
                            px1 -= Convert.ToInt32(Math.Ceiling(Convert.ToDouble(m) / 2));
                            px2 += m / 2;
                        }
                        else
                        {
                            px1 += Convert.ToInt32(Math.Ceiling(Convert.ToDouble(m) / 2));
                            px2 -= m / 2;
                        }
                    }
                    else continue;

                    if (px1 > 255)
                    {
                        px1 = 255; px2 = px1 - d - m;
                    }
                    else if (px1 < 0)
                    {
                        px1 = 0; px2 = px1 + d + m;
                    }

                    if (px2 > 255)
                    {
                        px2 = 255; px1 = px2 - d - m;
                    }
                    else if (px2 < 0)
                    {
                        px2 = 0; px1 = px2 + d + m;
                    }

                    d = Math.Abs(px1 - px2);
                    dif1 = Math.Abs(px3 - px1);
                    dif2 = Math.Abs(px3 - px2);

                    if (!((d != Math.Min(Math.Min(d, dif1), dif2)) || (Math.Abs(dif1 - d) <= upper - lower + 1) || (Math.Abs(dif2 - d) <= upper - lower + 1)))
                        cur_text_pos += t;    
                }

                img.UnlockBits();
                if (cur_text_pos >= file_bits.Length) return messageSize + 1; //если все биты сообщения вместились в контейнер
                else if (cur_pos.y == img.Height - 1 && cur_pos.x == img.Width - 1) return messageSize - 1; //если контейнер закончился
                else return 0;
            }
        }

        //Сокрытие сообщения
        public T Encrypt<T>(string file_path, byte[] file_bytes)
        {
            Bitmap bmp = new Bitmap(file_path);
            LockBitmap img = new LockBitmap(bmp);
            img.LockBits();

            BitArray file_bits = new BitArray(file_bytes);

            Position cur_pos = new Position { width = img.Width, height = img.Height, x = -1, y = 0 };
            Table cur_table = new Table();

            byte R, G, B;
            int d, dn, old_d, b, px1, px2, px3, dif1, dif2, lower, upper, t, cur_text_pos = 0, m;

            while(cur_text_pos < file_bits.Length)
            {
                cur_pos.NextPosition();
                R = img.GetPixel(cur_pos.x, cur_pos.y).R;
                G = img.GetPixel(cur_pos.x, cur_pos.y).G;
                B = img.GetPixel(cur_pos.x, cur_pos.y).B;
                d = Math.Min(Math.Min(Math.Abs(R - G), Math.Abs(G - B)), Math.Abs(R - B));
                old_d = d;

                if (d == Math.Abs(R - G))
                {
                    px1 = R; px2 = G; px3 = B; dif1 = Math.Abs(G - B); dif2 = Math.Abs(R - B);
                }
                else if (d == Math.Abs(G - B))
                {
                    px1 = G; px2 = B; px3 = R; dif1 = Math.Abs(R - G); dif2 = Math.Abs(R - B);
                }
                else
                {
                    px1 = R; px2 = B; px3 = G; dif1 = Math.Abs(R - G); dif2 = Math.Abs(G - B);
                }

                cur_table.getRegion(d);
                lower = cur_table.Lower;
                upper = cur_table.Upper;
                t = (cur_table.AmountOfBit + cur_text_pos < file_bits.Length) ? cur_table.AmountOfBit : file_bits.Length - cur_text_pos;

                b = GetIntFromBitArray(file_bits, cur_text_pos, t);
                dn = lower + b;
                m = Math.Abs(dn - d);

                if (Math.Abs(dif1 - d) > upper - lower + 1 && Math.Abs(dif2 - d) > upper - lower + 1)
                {
                    if (px1 >= px2 && dn > d)
                    {
                        px1 += Convert.ToInt32(Math.Ceiling(Convert.ToDouble(m) / 2));
                        px2 -= m / 2;
                    }
                    else if (px1 < px2 && dn > d)
                    {
                        px1 -= m / 2;
                        px2 += Convert.ToInt32(Math.Ceiling(Convert.ToDouble(m) / 2));
                    }
                    else if (px1 >= px2 && dn <= d)
                    {
                        px1 -= Convert.ToInt32(Math.Ceiling(Convert.ToDouble(m) / 2));
                        px2 += m / 2;
                    }
                    else
                    {
                        px1 += Convert.ToInt32(Math.Ceiling(Convert.ToDouble(m) / 2));
                        px2 -= m / 2;
                    }
                }
                else continue;

                if (px1 > 255)
                {
                    px1 = 255; px2 = px1 - d - m;
                }
                else if (px1 < 0)
                {
                    px1 = 0; px2 = px1 + d + m;
                }

                if (px2 > 255)
                {
                    px2 = 255; px1 = px2 - d - m;
                }
                else if (px2 < 0)
                {
                    px2 = 0; px1 = px2 + d + m;
                }

                d = Math.Abs(px1 - px2);
                dif1 = Math.Abs(px3 - px1);
                dif2 = Math.Abs(px3 - px2);

                if (!((d != Math.Min(Math.Min(d, dif1), dif2)) || (Math.Abs(dif1 - d) <= upper - lower + 1) || (Math.Abs(dif2 - d) <= upper - lower + 1)))
                    cur_text_pos += t;
                    

                if (old_d == Math.Abs(R - G))
                    img.SetPixel(cur_pos.x, cur_pos.y, Color.FromArgb(px1, px2, px3));
                else if (old_d == Math.Abs(G - B))
                    img.SetPixel(cur_pos.x, cur_pos.y, Color.FromArgb(px3, px1, px2));
                else
                    img.SetPixel(cur_pos.x, cur_pos.y, Color.FromArgb(px1, px3, px2));
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

                Position cur_pos = new Position { width = img.Width, height = img.Height, x = -1, y = 0 };
                Table cur_table = new Table();

                byte R, G, B;
                int d, dn, dif1, dif2, lower, upper, t, cur_text_pos = 0;
                string str_bit_arr = "";

                //пропускаем метод
                while (cur_text_pos < 3 * BYTE)
                {
                    cur_pos.NextPosition();
                    R = img.GetPixel(cur_pos.x, cur_pos.y).R;
                    G = img.GetPixel(cur_pos.x, cur_pos.y).G;
                    B = img.GetPixel(cur_pos.x, cur_pos.y).B;
                    d = Math.Min(Math.Min(Math.Abs(R - G), Math.Abs(G - B)), Math.Abs(R - B));

                    if (d == Math.Abs(R - G))
                    {
                        dif1 = Math.Abs(G - B); dif2 = Math.Abs(R - B);
                    }
                    else if (d == Math.Abs(G - B))
                    {
                        dif1 = Math.Abs(R - G); dif2 = Math.Abs(R - B);
                    }
                    else
                    {
                        dif1 = Math.Abs(R - G); dif2 = Math.Abs(G - B);
                    }

                    cur_table.getRegion(d);
                    lower = cur_table.Lower;
                    upper = cur_table.Upper;
                    t = cur_table.AmountOfBit;

                    if (Math.Abs(dif1 - d) > upper - lower + 1 && Math.Abs(dif2 - d) > upper - lower + 1)
                    {
                        dn = d - lower;
                        cur_text_pos += t;
                        str_bit_arr += GetStrBitArrayFromInt(dn, t);
                    }
                }
                str_bit_arr = str_bit_arr.Remove(0, 3 * BYTE);
                cur_text_pos = 0;

                //считываем формат
                while (cur_text_pos < 4 * BYTE)
                {
                    cur_pos.NextPosition();
                    R = img.GetPixel(cur_pos.x, cur_pos.y).R;
                    G = img.GetPixel(cur_pos.x, cur_pos.y).G;
                    B = img.GetPixel(cur_pos.x, cur_pos.y).B;
                    d = Math.Min(Math.Min(Math.Abs(R - G), Math.Abs(G - B)), Math.Abs(R - B));

                    if (d == Math.Abs(R - G))
                    {
                        dif1 = Math.Abs(G - B); dif2 = Math.Abs(R - B);
                    }
                    else if (d == Math.Abs(G - B))
                    {
                        dif1 = Math.Abs(R - G); dif2 = Math.Abs(R - B);
                    }
                    else
                    {
                        dif1 = Math.Abs(R - G); dif2 = Math.Abs(G - B);
                    }

                    cur_table.getRegion(d);
                    lower = cur_table.Lower;
                    upper = cur_table.Upper;
                    t = cur_table.AmountOfBit;

                    if (Math.Abs(dif1 - d) > upper - lower + 1 && Math.Abs(dif2 - d) > upper - lower + 1)
                    {
                        dn = d - lower;
                        cur_text_pos += t;
                        str_bit_arr += GetStrBitArrayFromInt(dn, t);
                    }
                }
                string format = Encoding.ASCII.GetString(GetByteArrayFromStrBitArray(str_bit_arr, 4 * BYTE));
                str_bit_arr = str_bit_arr.Remove(0, 4 * BYTE);
                cur_text_pos = 0;

                //считываем размер строки
                while (cur_text_pos < 4 * BYTE)
                {
                    cur_pos.NextPosition();
                    R = img.GetPixel(cur_pos.x, cur_pos.y).R;
                    G = img.GetPixel(cur_pos.x, cur_pos.y).G;
                    B = img.GetPixel(cur_pos.x, cur_pos.y).B;
                    d = Math.Min(Math.Min(Math.Abs(R - G), Math.Abs(G - B)), Math.Abs(R - B));

                    if (d == Math.Abs(R - G))
                    {
                        dif1 = Math.Abs(G - B); dif2 = Math.Abs(R - B);
                    }
                    else if (d == Math.Abs(G - B))
                    {
                        dif1 = Math.Abs(R - G); dif2 = Math.Abs(R - B);
                    }
                    else
                    {
                        dif1 = Math.Abs(R - G); dif2 = Math.Abs(G - B);
                    }

                    cur_table.getRegion(d);
                    lower = cur_table.Lower;
                    upper = cur_table.Upper;
                    t = cur_table.AmountOfBit;

                    if (Math.Abs(dif1 - d) > upper - lower + 1 && Math.Abs(dif2 - d) > upper - lower + 1)
                    {
                        dn = d - lower;
                        cur_text_pos += t;
                        str_bit_arr += GetStrBitArrayFromInt(dn, t);
                    }
                }
                int text_length = GetIntFromStrBitArray(str_bit_arr, 4 * BYTE);
                str_bit_arr = str_bit_arr.Remove(0, 4 * BYTE);
                cur_text_pos = 0;

                int count = 0;
                byte[] text = new byte[text_length];
                while (cur_text_pos < text_length * BYTE)
                {
                    cur_pos.NextPosition();
                    R = img.GetPixel(cur_pos.x, cur_pos.y).R;
                    G = img.GetPixel(cur_pos.x, cur_pos.y).G;
                    B = img.GetPixel(cur_pos.x, cur_pos.y).B;
                    d = Math.Min(Math.Min(Math.Abs(R - G), Math.Abs(G - B)), Math.Abs(R - B));

                    if (d == Math.Abs(R - G))
                    {
                        dif1 = Math.Abs(G - B); dif2 = Math.Abs(R - B);
                    }
                    else if (d == Math.Abs(G - B))
                    {
                        dif1 = Math.Abs(R - G); dif2 = Math.Abs(R - B);
                    }
                    else
                    {
                        dif1 = Math.Abs(R - G); dif2 = Math.Abs(G - B);
                    }

                    cur_table.getRegion(d);
                    lower = cur_table.Lower;
                    upper = cur_table.Upper;
                    t = cur_table.AmountOfBit;

                    if (Math.Abs(dif1 - d) > upper - lower + 1 && Math.Abs(dif2 - d) > upper - lower + 1)
                    {
                        dn = d - lower;
                        cur_text_pos += t;
                        str_bit_arr += GetStrBitArrayFromInt(dn, t);

                        if (str_bit_arr.Length >= BYTE)
                        {
                            text[count++] = Convert.ToByte(GetIntFromStrBitArray(str_bit_arr, BYTE));
                            str_bit_arr = str_bit_arr.Remove(0, BYTE);
                        }
                    }
                }

                img.UnlockBits();
                return new Output_file { format = (format[0] == '*') ? format.Substring(1, format.Length - 1) : format, file_byte = text };
            }   
        }
    }
}
