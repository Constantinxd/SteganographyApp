using System;
using System.Collections;
using System.Drawing;
using System.Text;

namespace Steganography.Methods
{
    class Steganography_LSB : AdditionalMethods, ISteganographyMethod
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

        //Перезаписать последний бит цветового канала
        public static int SetLSB(int component, bool bit)
        {
            component = (component >> 1) << 1;
            if (bit) component |= 1;

            return component;
        }

        //Получить первые три символа сообщения
        public static string GetSteganographyMethod(string file_path)
        {
            using (Bitmap bmp = new Bitmap(file_path))
            {
                LockBitmap img = new LockBitmap(bmp);
                img.LockBits();

                Position cur_pos = new Position { width = img.Width, height = img.Height, x = 0, y = 0 };

                //считываем метод
                BitArray method_byte = new BitArray(3 * BYTE);
                for (int i = 0; i < 3 * BYTE; i++)
                {
                    Color px = img.GetPixel(cur_pos.x, cur_pos.y);
                    int component = Convert.ToInt32(px.R);
                    method_byte[i] = (component % 2 == 1);

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
                return (bmp.Width * bmp.Height) / 8;
            }
        }

        //Сокрытие сообщения
        public T Encrypt<T>(string file_path, byte[] file_bytes)
        {
            Bitmap bmp = new Bitmap(file_path);
            LockBitmap img = new LockBitmap(bmp);
            img.LockBits();

            Position cur_pos = new Position { width = img.Width, height = img.Height, x = 0, y = 0 };

            for (int i = 0; i < file_bytes.Length; i++)
            {
                BitArray byte_char = new BitArray(new byte[] { file_bytes[i] });

                for (int j = 0; j < byte_char.Length; j++)
                {
                    Color px = img.GetPixel(cur_pos.x, cur_pos.y);

                    int component = Convert.ToInt32(px.R);         
                    component = SetLSB(component, byte_char[j]);
                    img.SetPixel(cur_pos.x, cur_pos.y, Color.FromArgb(px.A, component, px.G, px.B));
                    cur_pos.NextPosition();
                }
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

                Position cur_pos = new Position { width = img.Width, height = img.Height, x = 0, y = 0 };

                //пропускаем метод
                for (int i = 0; i < 3 * BYTE; i++)
                    cur_pos.NextPosition();

                //считываем формат
                BitArray format_byte = new BitArray(4 * BYTE);
                for (int i = 0; i < 4 * BYTE; i++)
                {
                    Color px = img.GetPixel(cur_pos.x, cur_pos.y);
                    int component = Convert.ToInt32(px.R);
                    format_byte[i] = (component % 2 == 1);

                    cur_pos.NextPosition();
                }
                string format = Encoding.ASCII.GetString(GetByteArrayFromBitArray(format_byte));

                //считываем размер строки
                BitArray text_length_byte = new BitArray(4 * BYTE);
                for (int i = 0; i < 4 * BYTE; i++)
                {
                    Color px = img.GetPixel(cur_pos.x, cur_pos.y);
                    int component = Convert.ToInt32(px.R);
                    text_length_byte[i] = (component % 2 == 1);

                    cur_pos.NextPosition();
                }
                int text_length = GetIntFromBitArray(text_length_byte);

                byte[] text = new byte[text_length];
                for (int i = 0; i < text.Length; i++)
                {
                    BitArray byte_char = new BitArray(new byte[] { text[i] });
                    for (int j = 0; j < byte_char.Length; j++)
                    {
                        Color px = img.GetPixel(cur_pos.x, cur_pos.y);
                        int component = Convert.ToInt32(px.R);
                        byte_char[j] = Convert.ToBoolean(component & 1);

                        cur_pos.NextPosition();
                    }

                    text[i] = Convert.ToByte(GetIntFromBitArray(byte_char));
                }

                img.UnlockBits();
                return new Output_file { format = (format[0] == '*') ? format.Substring(1, format.Length - 1) : format, file_byte = text };
            }
        }
    }
}
