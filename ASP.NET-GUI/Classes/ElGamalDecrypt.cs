using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Security.Cryptography;

namespace ASP.NET_GUI.Classes
{
    public class ElGamalDecrypt : ElGamalCipher
    {
        public ElGamalDecrypt(ElGamalKeyStruct p_struct) : base(p_struct)
        {
            // vendosja e madhesise default te bllokut per tu bere ciphertext
            block_size = ciphertext_blocksize;
        }

        protected override byte[] ProcessDataBlock(byte[] plaintext_data_block)
        {
            // ekstraktimi i byte arrays qe riprezentojne A dhe B 
            byte[] a_bytes = new byte[ciphertext_blocksize / 2];
            Array.Copy(plaintext_data_block, 0, a_bytes, 0, a_bytes.Length);
            
            byte[] b_bytes = new Byte[ciphertext_blocksize / 2];
            Array.Copy(plaintext_data_block, a_bytes.Length, b_bytes, 0, b_bytes.Length);

            // krijimi i big integers nga byte arrays
            BigInteger A = new BigInteger(a_bytes);
            BigInteger B = new BigInteger(b_bytes);

            //c2 * c1 exp X mod p 
            BigInteger M = (B * A.modPow(current_key.X, current_key.P).modInverse(current_key.P)) % current_key.P;

            // kthimi i rezultatit
            byte[] m_bytes = M.getBytes();

            if (m_bytes.Length < plaintext_blocksize)
            {
                byte[] full_block_result = new byte[plaintext_blocksize];
                Array.Copy(m_bytes, 0, full_block_result, plaintext_blocksize - m_bytes.Length, m_bytes.Length);

                m_bytes = full_block_result;
            }
            return m_bytes;
        }

        protected override byte[] ProcessFinalDataBlock(byte[] plaintext_final_block)
        {
            if (plaintext_final_block.Length > 0)
            {
                return ProcessDataBlock(plaintext_final_block);
            }
            else
            {
                return new byte[0];
            }
        }
    }
}