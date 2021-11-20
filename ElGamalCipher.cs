using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ElGamal
{
    // Klasa e cila perdoret per bazat ENC/DEC dhe per te ndare te dhenat ne blloqe

    public abstract class ElGamalCipher : ElGamalManagement
    {
        protected int block_size;
        protected int plaintext_blocksize;
        protected int ciphertext_blocksize;

        protected ElGamalKeyStruct current_key;

        public ElGamalCipher(ElGamalKeyStruct elg_current_key)
        {
            // vendosja e detajeve te celesit 
            current_key = elg_current_key;

            // llogaritja e madhesise se blloqeve
            plaintext_blocksize = (elg_current_key.P.bitCount() - 1) / 8;
            ciphertext_blocksize = ((elg_current_key.P.bitCount() + 7) / 8) * 2; //ciphertext is twice as plaintext

            // vendosja e bllokut default per tekstin origjinal
            block_size = plaintext_blocksize;
        }

        
    }
}