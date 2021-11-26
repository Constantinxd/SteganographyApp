using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Steganography.Methods
{
    class Steganography_LSB_Palette : AdditionalMethods, ISteganographyMethod
    {
        private const int first_bit = 0; // позиция первого наименее значащего бита
        private const int second_bit = 1; // позиция второго наименее значащего бита
        private const int BYTE = 8;

        //Получить первые три символа сообщения
        public static string GetSteganographyMethod(string file_path)
        {
            byte[] image_bytes = File.ReadAllBytes(file_path);

            int cur_pos = 13;

            //считываем метод
            BitArray method_byte = new BitArray(3 * BYTE);
            for (int i = 0; i < 3 * BYTE / 2; i++)
            {
                BitArray bytes = new BitArray(new byte[] { image_bytes[cur_pos] });
                method_byte[2 * i] = bytes[second_bit];
                method_byte[2 * i + 1] = bytes[first_bit];
                ++cur_pos;
            }

            string method = Encoding.ASCII.GetString(GetByteArrayFromBitArray(method_byte));

            return method;
        }

        //Размер сообщения, которое можно сокрыть в изображении данным методом
        public static int GetSize(string file_path)
        {
            byte[] image_bytes = File.ReadAllBytes(file_path);
            BitArray palette_info_byte = new BitArray(new byte[] { image_bytes[10] });
            BitArray palette_info = new BitArray(3);
            palette_info[0] = palette_info_byte[0]; palette_info[1] = palette_info_byte[1]; palette_info[2] = palette_info_byte[2];

            byte color_depth = Convert.ToByte(GetIntFromBitArray(palette_info));
            return (1 << color_depth + 1) * 3 / 4;
        }

        //Сокрытие сообщения
        public T Encrypt<T>(string file_path, byte[] file_bytes)
        {
            byte[] image_bytes = File.ReadAllBytes(file_path);

            int cur_pos = 13; //с этой позиции начинается палитра

            //записываем сообщение
            for (int i = 0; i < file_bytes.Length; i++)
            {
                BitArray byte_char = new BitArray(new byte[] { file_bytes[i] });

                for (int j = 0; j < byte_char.Length / 2; j++)
                {
                    BitArray bytes = new BitArray(new byte[] { image_bytes[cur_pos] });
                    int c = image_bytes[cur_pos];
                    bytes[second_bit] = byte_char[2 * j];
                    bytes[first_bit] = byte_char[2 * j + 1];
                    image_bytes[cur_pos] = Convert.ToByte(GetIntFromBitArray(bytes));
                    if (image_bytes[cur_pos] != c) MessageBox.Show(cur_pos.ToString());
                    ++cur_pos;
                }
            }

            return (dynamic)image_bytes;
        }

        //Извлечение сообщения
        public Output_file Decrypt(string file_path)
        {
            byte[] image_bytes = File.ReadAllBytes(file_path);

            int cur_pos = 13 + (3 * BYTE / 2); //с 13 байта начинается палитра. пропускаем первые 3 байта сообщения, в которых написан метод

            //считываем формат
            BitArray format_byte = new BitArray(4 * BYTE);
            for (int i = 0; i < 4 * BYTE / 2; i++)
            {
                BitArray bytes = new BitArray(new byte[] { image_bytes[cur_pos] });
                format_byte[2 * i] = bytes[second_bit];
                format_byte[2 * i + 1] = bytes[first_bit];
                ++cur_pos;
            }
            string format = Encoding.ASCII.GetString(GetByteArrayFromBitArray(format_byte));

            //считываем размер строки
            BitArray text_length_byte = new BitArray(BYTE);
            for (int i = 0; i < BYTE / 2; i++)
            {
                BitArray bytes = new BitArray(new byte[] { image_bytes[cur_pos] });
                text_length_byte[2 * i] = bytes[second_bit];
                text_length_byte[2 * i + 1] = bytes[first_bit];
                ++cur_pos;
            }
            byte text_length = Convert.ToByte(GetIntFromBitArray(text_length_byte));


            byte[] text = new byte[text_length];
            for (int i = 0; i < text.Length; i++)
            {
                BitArray byte_char = new BitArray(new byte[] { text[i] });

                for (int j = 0; j < byte_char.Length / 2; j++)
                {
                    BitArray bytes = new BitArray(new byte[] { image_bytes[cur_pos] });
                    byte_char[2 * j] = bytes[second_bit];
                    byte_char[2 * j + 1] = bytes[first_bit];
                    ++cur_pos;
                }

                text[i] = Convert.ToByte(GetIntFromBitArray(new BitArray(byte_char)));
            }

            return new Output_file { format = (format[0] == '*') ? format.Substring(1, format.Length - 1) : format, file_byte = text };
        }
    }
}
