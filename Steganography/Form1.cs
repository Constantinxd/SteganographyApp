using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steganography
{
    struct Output_file
    {
        public string format;
        public byte[] file_byte;
    }

    public partial class Form1 : Form
    {
        private int count;
        private int imageSize;
        private int messageSize;
        private string imageFormatHide;
        private string imageFormatExtract;
        Methods.ISteganographyMethod method;

        public Form1()
        {
            InitializeComponent();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            imageSize = 0;
            messageSize = 0;
            count = 1;
            imageFormatExtract = "";
            imageFormatHide = "";
            textBoxHideMessageText.Enabled = false;
            this.Width = 575; //1134
        }

        //Кнопка для выбора файла при сокрытии
        private void radioButtonHideMessageFile_Click(object sender, EventArgs e)
        {
            textBoxHideMessageText.Enabled = false;
            textBoxHideMessageFile.Enabled = true;
            buttonHideMessageFile.Enabled = true;

            messageSize = GetMessageSize();
            UpdateLabelMessageSize();

            if (comboBoxHideMessageMethod.Items.Count > 0 && comboBoxHideMessageMethod.SelectedItem.ToString() == "PVD")
            {
                imageSize = GetImageSize();
                UpdateLabelImageSize();
            }
        }

        //Кнопка для выбора текста при сокрытии
        private void radioButtonHideMessageText_Click(object sender, EventArgs e)
        {
            textBoxHideMessageFile.Enabled = false;
            buttonHideMessageFile.Enabled = false;
            textBoxHideMessageText.Enabled = true;

            messageSize = GetMessageSize();
            UpdateLabelMessageSize();

            if (comboBoxHideMessageMethod.Items.Count > 0 && comboBoxHideMessageMethod.SelectedItem.ToString() == "PVD")
            {
                imageSize = GetImageSize();
                UpdateLabelImageSize();
            }
        }  

        //Возвращает метод, формат файла и длину файла, в виде массива байтов
        private byte[] GetAdditionalInfo(int file_bytes_len)
        {
            string method;
            string format;
            byte[] file_length;

            //Определяем метод
            if (comboBoxHideMessageMethod.SelectedItem.ToString() == "LSB_Palette") method = "PAL";
            else method = comboBoxHideMessageMethod.SelectedItem.ToString();

            //Определяем формат файла
            if (radioButtonHideMessageFile.Checked)
            {
                format = textBoxHideMessageFile.Text.Substring(textBoxHideMessageFile.Text.LastIndexOf('.') + 1);
                if (format.Length == 3) format = '*' + format;
            }
            else format = "etxt";

            //Определяем длину файла
            if (comboBoxHideMessageMethod.SelectedItem.ToString() == "LSB_Palette") file_length = new byte[1] { Convert.ToByte(file_bytes_len) };
            else file_length = BitConverter.GetBytes(file_bytes_len);

            return Encoding.ASCII.GetBytes(method + format).Concat(file_length).ToArray();
        }

        //Записывает размер сообщения
        private int GetMessageSize()
        {
            int size = 0;

            if (radioButtonHideMessageFile.Checked && textBoxHideMessageFile.Text != "")
            {
                StreamReader sr = new StreamReader(textBoxHideMessageFile.Text);
                size = Encoding.GetEncoding("Windows-1251").GetBytes(sr.ReadToEnd()).Length;
            }
            
            if (radioButtonHideMessageText.Checked) size = Encoding.GetEncoding("Windows-1251").GetBytes(textBoxHideMessageText.Text).Length;

            return size;
        }

        //Записывает максимальный размер сообщения
        private int GetImageSize()
        {
            int size = 0; 

            if (comboBoxHideMessageMethod.Items.Count > 0)
            {
                if (comboBoxHideMessageMethod.SelectedItem.ToString() == "LSB_Palette")
                {
                    size = Methods.Steganography_LSB_Palette.GetSize(textBoxHideImage.Text) - 8;
                }
                else if (comboBoxHideMessageMethod.SelectedItem.ToString() == "LSB")
                {
                    size = Methods.Steganography_LSB.GetSize(textBoxHideImage.Text) - 11;
                }
                else if (comboBoxHideMessageMethod.SelectedItem.ToString() == "DCT")
                {
                    size = Methods.Steganography_DCT.GetSize(textBoxHideImage.Text) - 11;
                }
                else if (comboBoxHideMessageMethod.SelectedItem.ToString() == "PVD" && messageSize != 0)
                {
                    byte[] file_bytes;
                    if (radioButtonHideMessageFile.Checked) file_bytes = File.ReadAllBytes(textBoxHideMessageFile.Text);
                    else file_bytes = Encoding.GetEncoding("Windows-1251").GetBytes(textBoxHideMessageText.Text);
                    byte[] additional_info_bytes = GetAdditionalInfo(file_bytes.Length);
                    file_bytes = additional_info_bytes.Concat(file_bytes).ToArray();

                    size = Methods.Steganography_PVD.GetSize(textBoxHideImage.Text, file_bytes, messageSize);
                }
                else size = 0;
            }

            return size;
        }

        //Записывем формат изображения
        private string GetImageFormat(string image_path)
        {
            string format = "";
            byte[] image_bytes = File.ReadAllBytes(image_path);
            byte[] formatBMP = new byte[2];
            byte[] formatPNG = new byte[3];
            formatBMP[0] = image_bytes[0]; formatBMP[1] = image_bytes[1];
            formatPNG[0] = image_bytes[1]; formatPNG[1] = image_bytes[2]; formatPNG[2] = image_bytes[3];

            if (Encoding.ASCII.GetString(image_bytes, 0, 6).Equals("GIF89a")) format = "GIF";
            else
            {
                using (Bitmap img = new Bitmap(image_path))
                {
                    int depth = Image.GetPixelFormatSize(img.PixelFormat);

                    if (depth != 24 && depth != 32)
                    {
                        throw new Exception();
                    }
                }

                if (Encoding.ASCII.GetString(formatBMP) == "BM") format = "BMP";
                else if (Encoding.ASCII.GetString(formatPNG) == "PNG") format = "PNG";
            }

            if (format == "") throw new Exception();

            return format;

        }

        //Получить метод из сообщения
        private string GetMethod(string image_path)
        {
                if (Methods.Steganography_LSB_Palette.GetSteganographyMethod(image_path) == "PAL") return "PAL";
                else if (Methods.Steganography_LSB.GetSteganographyMethod(image_path) == "LSB") return "LSB";
                else if (Methods.Steganography_PVD.GetSteganographyMethod(image_path) == "PVD") return "PVD";
                else if (Methods.Steganography_DCT.GetSteganographyMethod(image_path) == "DCT") return "DCT";
                else throw new Exception("Изображение не содержит информации");            
        }

        //Обновляет список стеганографических методов
        private void UpdateComboBoxMethods()
        {
            if (imageFormatHide == "GIF")
            {
                comboBoxHideMessageMethod.Items.Clear();
                comboBoxHideMessageMethod.Items.Add("LSB_Palette");
                comboBoxHideMessageMethod.Enabled = false;
                comboBoxHideMessageMethod.DropDownStyle = ComboBoxStyle.Simple;
                comboBoxHideMessageMethod.SelectedIndex = 0;
            }
            else
            {
                comboBoxHideMessageMethod.Items.Clear();
                comboBoxHideMessageMethod.Items.Add("LSB");
                comboBoxHideMessageMethod.Items.Add("PVD");
                comboBoxHideMessageMethod.Items.Add("DCT");
                comboBoxHideMessageMethod.Enabled = true;
                comboBoxHideMessageMethod.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxHideMessageMethod.SelectedIndex = 0;
            }
        }

        //Обновляет строку максимального размера сообщения
        private void UpdateLabelImageSize()
        {
            if (comboBoxHideMessageMethod.Items.Count > 0 && comboBoxHideMessageMethod.SelectedItem.ToString() == "PVD" && messageSize != 0)
            {
                if (imageSize == messageSize + 1) labelHideSettingsMaxSize.Text = "более " + messageSize + " байт.";
                else if (imageSize == messageSize - 1) labelHideSettingsMaxSize.Text = "менее " + messageSize + " байт.";
            }
            else labelHideSettingsMaxSize.Text = imageSize + " байт.";
        }

        //Обновляет строку размера сообщения
        private void UpdateLabelMessageSize()
        {
            labelHideSettingsSize.Text = messageSize.ToString() + " байт.";
        }

        //Кнопка сокрытия информации
        private void buttonHide_Click(object sender, EventArgs e)   
        {
            try
            {
                if (imageFormatHide == "") throw new Exception("Введите изображение");
                if (messageSize == 0) throw new Exception("Введите сообщение");
                if (messageSize > imageSize) throw new Exception("Не хватает места");

                byte[] file_bytes;
                if (radioButtonHideMessageFile.Checked) file_bytes = File.ReadAllBytes(textBoxHideMessageFile.Text);
                else file_bytes = Encoding.GetEncoding("Windows-1251").GetBytes(textBoxHideMessageText.Text);
                byte[] additional_info_bytes = GetAdditionalInfo(file_bytes.Length);
                file_bytes = additional_info_bytes.Concat(file_bytes).ToArray();

                method = Methods.SteganographyMethodCreater.Create(comboBoxHideMessageMethod.SelectedItem.ToString());

                if (comboBoxHideMessageMethod.SelectedItem.ToString() == "LSB_Palette")
                {
                    byte[] new_file_bytes = method.Encrypt<byte[]>(textBoxHideImage.Text, file_bytes);
                    saveFileDialog.Filter = "GIF(*.gif)|*.gif";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            File.WriteAllBytes(saveFileDialog.FileName, new_file_bytes);
                        }
                        catch
                        {
                            MessageBox.Show("При сохранении изображения произошла ошибка.");
                        }
                    }
                }
                else
                {
                    using (Bitmap img = method.Encrypt<Bitmap>(textBoxHideImage.Text, file_bytes))
                    {
                        if (imageFormatHide == "BMP") saveFileDialog.Filter = "BMP(*.bmp)|*.bmp";
                        else saveFileDialog.Filter = "PNG(*.png)|*.png";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                if (imageFormatHide == "BMP")
                                    img.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                                else
                                    img.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            }
                            catch
                            {
                                MessageBox.Show("При сохранении изображения произошла ошибка.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Кнопка извлечения информации
        private void buttonExtract_Click(object sender, EventArgs e)
        {
            try
            {
                if (imageFormatExtract == "") throw new Exception("Введите изображение");
                Output_file out_file;

                method = Methods.SteganographyMethodCreater.Create(GetMethod(textBoxExtractImage.Text));

                out_file = method.Decrypt(textBoxExtractImage.Text);

                if (out_file.format == "etxt")
                {
                    textBoxExtractMessageText.Enabled = true;
                    textBoxExtractMessageText.Text = Encoding.GetEncoding("Windows-1251").GetString(out_file.file_byte);
                }
                else
                {
                    textBoxExtractMessageText.Enabled = false;
                    textBoxExtractMessageText.Text = "Файл успешно извлечен в папку с исходным изображением." + Environment.NewLine + "Путь:" + Environment.NewLine + textBoxExtractImage.Text.Substring(0, textBoxExtractImage.Text.LastIndexOf('\\') + 1) + "file" + (count) + "." + out_file.format;
                    File.WriteAllBytes(textBoxExtractImage.Text.Substring(0, textBoxExtractImage.Text.LastIndexOf('\\') + 1) + "file" + (count++) + "." + out_file.format, out_file.file_byte);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        //Кнопка открытия файла, подлежащего сокрытию
        private void buttonHideMessageFile_Click(object sender, EventArgs e)
        {
            if (openFileDialogMessageFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBoxHideMessageFile.Text = openFileDialogMessageFile.FileName;
                    messageSize = GetMessageSize();
                    UpdateLabelMessageSize();

                    if (comboBoxHideMessageMethod.Items.Count > 0 && comboBoxHideMessageMethod.SelectedItem.ToString() == "PVD")
                    {
                        imageSize = GetImageSize();
                        UpdateLabelImageSize();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("При открытии файла произошла ошибка: " + ex.Message);
                }
            }
        }

        //Кнопка открытия изображения при сокрытии
        private void buttonHideImageOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialogImage.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imageFormatHide = GetImageFormat(openFileDialogImage.FileName);
                    textBoxHideImage.Text = openFileDialogImage.FileName;
                    UpdateComboBoxMethods();
                }
                catch
                {
                    MessageBox.Show("Поддерживаются только 24 и 32 битные BMP и PNG изображения, а также изображения формата GIF89a");
                }
            }
        }

        //Кнопка открытия изображения при извлечении 
        private void buttonExtractImageOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialogImage.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imageFormatExtract = GetImageFormat(openFileDialogImage.FileName);
                    textBoxExtractImage.Text = openFileDialogImage.FileName;
                }
                catch
                {
                    MessageBox.Show("Поддерживаются только 24 и 32 битные BMP и PNG изображения, а также изображения формата GIF89a");
                }
            }
        }

        //Контейнер для текста при сокрытии был изменен
        private void textBoxHideMessageText_TextChanged(object sender, EventArgs e)
        {
            messageSize = GetMessageSize();
            UpdateLabelMessageSize();

            if (comboBoxHideMessageMethod.Items.Count > 0 && comboBoxHideMessageMethod.SelectedItem.ToString() == "PVD")
            {
                imageSize = GetImageSize();
                UpdateLabelImageSize();
            }
        }

        //Изменили стеганографический метод
        private void comboBoxHideMessageMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            imageSize = GetImageSize();
            UpdateLabelImageSize();
        }

        //Кнопка переключения на меню сокрытия информации
        private void buttonMenuHide_Click(object sender, EventArgs e)
        {
            groupBoxMenuExtract.Visible = false;
            groupBoxMenuHide.Visible = true;
            groupBoxMenuHide.Location = groupBoxMenuExtract.Location;
        }

        //Кнопка переключения на меню извлечения информации
        private void buttonMenuExtract_Click(object sender, EventArgs e)
        {
            groupBoxMenuExtract.Visible = true;
            groupBoxMenuHide.Visible = false;
            groupBoxMenuExtract.Location = groupBoxMenuHide.Location;
        }

        //Кнопка вызова формы Помощь
        private void toolStripMenuHelp_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
