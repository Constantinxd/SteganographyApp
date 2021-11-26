using System;
using System.Collections.Generic;
using System.Text;

namespace Steganography.Methods
{
    class SteganographyMethodCreater
    {
        public static ISteganographyMethod Create(string selected_method)
        {
            if (selected_method == "LSB_Palette" || selected_method == "PAL") return new Steganography_LSB_Palette();
            else if (selected_method == "LSB") return new Steganography_LSB();
            else if (selected_method == "DCT") return new Steganography_DCT();
            else return new Steganography_PVD();
        }
    }
}
