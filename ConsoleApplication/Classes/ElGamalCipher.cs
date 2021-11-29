using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ElGamal_Gr._8
{
    // Klasa e cila perdoret per bazat ENC/DEC dhe per te ndare te dhenat ne blloqe

    public abstract class ElGamalCipher : ElGamalManagement
    {
        protected int block_size;
        protected int plaintext_blocksize;
        protected int ciphertext_blocksize;
        protected ElGamalKeyStruct current_key;
        protected abstract byte[] ProcessDataBlock(byte[] plaintext_data_block);
        protected abstract byte[] ProcessFinalDataBlock(byte[] final_plaintext_block);

        public ElGamalCipher(ElGamalKeyStruct elg_current_key)
        {
            // vendosja e detajeve te celesit 
            current_key = elg_current_key;

            // llogaritja e madhesise se blloqeve
            plaintext_blocksize = (elg_current_key.P.bitCount() - 1) / 8;
            ciphertext_blocksize = ((elg_current_key.P.bitCount() + 7) / 8) * 2; 

            // vendosja e bllokut default per tekstin origjinal
            block_size = plaintext_blocksize;
        }

         public byte[] ProcessData(byte[] data)
        {
            MemoryStream stream = new MemoryStream();

            // llogaritja e blloqeve te kompletuara
            int complete_blocks = data.Length / block_size;

            byte[] hold_data_block = new Byte[block_size];

            int i = 0;

            for (; i < complete_blocks; i++)
            {
                // kopjimi i bllokut dhe krijimi i big integer
                Array.Copy(data, i * block_size, hold_data_block, 0, block_size);
                // procesimi i bllokut
                byte[] result = ProcessDataBlock(hold_data_block);
                // shkruarja e te dhenave te procesuara ne stream
                stream.Write(result, 0, result.Length);
            }

            // procesimi i bllokut final
            byte[] last_block = new Byte[data.Length - (complete_blocks * block_size)];

            Array.Copy(data, i * block_size, last_block, 0, last_block.Length);

            // procesimi i bllokut final
            byte[] final_result = ProcessFinalDataBlock(last_block);

            // shkruarja e bajtave te fundit te te dhenave ne stream
            stream.Write(final_result, 0, final_result.Length);

            // kthimi i permbajtjeve te stream si nje byte array 
            return stream.ToArray();
        }
    }
}