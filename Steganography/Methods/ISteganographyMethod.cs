using System;
using System.Collections.Generic;
using System.Text;

namespace Steganography.Methods
{
    interface ISteganographyMethod
    {
        T Encrypt<T>(string file_path, byte[] file_bytes);

        Output_file Decrypt(string file_path);
    }
}
