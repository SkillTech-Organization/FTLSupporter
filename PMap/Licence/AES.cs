using System;
using System.Collections.Generic;
using System.Text;

using sc;
using aes;

namespace PMap.Licence
{
    abstract class AES
    {
        public static byte[] Code(string input, string password, bool encode)
        {
            return Code(System.Text.Encoding.UTF8.GetBytes(input), password, encode);
        }

        public static byte[] Code(byte[] inbuff, string password, bool encode)
        {
            // context
            int keySize = 32; // = bits256, 24 = bits192, 16 = bits128

            IBlockCipher ibc = AesFactory.GetAes();

            // set key paramters, fixed in this example
            byte[] iv = new byte[ibc.BlockSizeInBytes()]; // 16 for Aes
            for (int i = 0; i < iv.Length; ++i) iv[i] = 0;
            byte[] salt = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            int iterationCount = 1024;

            byte[] key = KeyGen.DeriveKey(password, keySize, salt, iterationCount);
            StreamCtx _aes = StreamCipher.MakeStreamCtx(ibc, key, iv);

            byte[] outbuff = StreamCipher.Encode(_aes, inbuff, (encode ? StreamCipher.ENCRYPT : StreamCipher.DECRYPT));

            return outbuff;
        }
    }
}
