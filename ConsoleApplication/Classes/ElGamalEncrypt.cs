using System;

namespace ElGamal_Gr._8
{
    public class ElGamalEncrypt : ElGamalCipher
    {
        Random random_number;

        public ElGamalEncrypt(ElGamalKeyStruct p_struct) : base(p_struct)
        {
            random_number = new Random();
        }

        //Metoda ProcessData
        protected override byte[] ProcessDataBlock(byte[] plaintext_data_block)
        {
            // numri random K
            BigInteger K;
            // krijimi i K, i cili eshte numer random     
            do
            {
                K = new BigInteger();
                K.genRandomBits(current_key.P.bitCount() - 1, random_number);
            }
            while 
            
            (K.gcd(current_key.P - 1) != 1);

            // llogaritja e vlerave A = G exp K mod P 
            // dhe B = ((Y exp K mod P) * plain_data_block) / P
            // Y eshte rezultati i enkriptuar i Alice (celesi publik)
            BigInteger A = current_key.G.modPow(K, current_key.P);
            BigInteger B = (current_key.Y.modPow(K, current_key.P) * new BigInteger(plaintext_data_block)) % (current_key.P);

            // ciphertext
            byte[] cipher_result = new byte[ciphertext_blocksize];

            // kopjimi i bajtave nga A dhe B ne array qe eshte cipher_result
            byte[] a_bytes = A.getBytes();
            Array.Copy(a_bytes, 0, cipher_result, ciphertext_blocksize / 2 - a_bytes.Length, a_bytes.Length); //96 bit

            byte[] b_bytes = B.getBytes();
            Array.Copy(b_bytes, 0, cipher_result, ciphertext_blocksize - b_bytes.Length, b_bytes.Length); //96 bit

            // kthimi i array pas bashkimit te A dhe B  
            return  cipher_result;
        }

        protected override byte[] ProcessFinalDataBlock(byte[] final_block)
        {
            if (final_block.Length > 0)
            {
                if (final_block.Length < block_size)
                {
                    // krijimi i nje blloku me madhesi te plote duke shtuar zero
                   
                    byte[] padded_block = new byte[block_size];
                    Array.Copy(final_block, 0, padded_block, 0, final_block.Length);
                    return ProcessDataBlock(padded_block);
                }
                else
                {
                    return ProcessDataBlock(final_block);
                }
            }
            else
            {
                return new byte[0];
            }
        }
    }
}