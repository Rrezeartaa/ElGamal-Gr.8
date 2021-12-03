using System;
using System.Security.Cryptography;

namespace ASP.NET_GUI.Classes
{
    public class ElGamalManagement : Parameters
    {
        public struct ElGamalKeyStruct
        {
            public BigInteger P;
            public BigInteger G;
            public BigInteger Y;
            public BigInteger X;
        }

        //celesi i tanishem qe po perdoret
        private ElGamalKeyStruct current_key;

        public ElGamalManagement()
        {
            
            current_key = new ElGamalKeyStruct();
            
            current_key.P = new BigInteger(0);
            current_key.G = new BigInteger(0);
            current_key.Y = new BigInteger(0);
            current_key.X = new BigInteger(0);

            // madhesia default e celesit
            KeySizeValue = 384;

            // rangu i celesave
            LegalKeySizesValue = new[] { new KeySizes(384, 1088, 8) };
        }

         public override string SignatureAlgorithm
        {
            get
            {
                return "ElGamal";
            }
        }

        public override string KeyExchangeAlgorithm
        {
            get
            {
                return "ElGamal";
            }
        }

        private void CreateKeyPair(int key_strength)
        {
            // krijimi i numrit random
            Random random_number = new Random();

            current_key.X = new BigInteger();
            current_key.G = new BigInteger();

            // krijimi i numrit prime P
            current_key.P = BigInteger.genPseudoPrime(key_strength, 16, random_number);

            // krijimi i dy numrave random, qe jane me te vegjel se P 
            current_key.X.genRandomBits(key_strength - 1, random_number);
            current_key.G.genRandomBits(key_strength - 1, random_number);

            // llogaritja e Y modPow(exp, modulo) Y = GexpX modP
            current_key.Y = current_key.G.modPow(current_key.X, current_key.P);
        }

        //Kontrollimi nese user-i nuk ka dhene input
        private bool NeedToGenerateKey()
        {
            return current_key.P == 0 && current_key.G == 0 && current_key.Y == 0;
        }

        //Nese jo krijo ciftin e celesave
        public ElGamalKeyStruct KeyStruct
        {
            get
            {
                if (NeedToGenerateKey())
                {
                    CreateKeyPair(KeySizeValue);
                }
                return current_key;
            }
            set
            {
                current_key = value;
            }

        }

        public override void ImportParameters(ElGamalParameters p_parameters)
        {
          
            current_key.P = new BigInteger(p_parameters.P);
            current_key.G = new BigInteger(p_parameters.G);
            current_key.Y = new BigInteger(p_parameters.Y);

            if (p_parameters.X != null && p_parameters.X.Length > 0)
            {
                current_key.X = new BigInteger(p_parameters.X);
            }
            // vendosja e gjatesise se celesit bazuar ne importim
            KeySizeValue = current_key.P.bitCount();
        }

        public override ElGamalParameters ExportParameters(bool include_private_params)
        {
            //Nese nuk ka asgje per eksportim krijo ciftin e pare te celesave
            if (NeedToGenerateKey())
            {
                CreateKeyPair(KeySizeValue);
            }

            ElGamalParameters elgamal_params = new ElGamalParameters();

            elgamal_params.P = current_key.P.getBytes();
            elgamal_params.G = current_key.G.getBytes();
            elgamal_params.Y = current_key.Y.getBytes();

            if (include_private_params)
            {
                elgamal_params.X = current_key.X.getBytes();
            }
            else
            {
                elgamal_params.X = new byte[1];
            }

            return elgamal_params;
        }

        public override byte[] EncryptData(byte[] data)
        {
            if (NeedToGenerateKey())
            {
                // krijimi i nje celesi te ri para se me eksportu 
                CreateKeyPair(KeySizeValue);
            }
            // enkriptimi i te dhenave
            ElGamalEncrypt encrypt_data = new ElGamalEncrypt(current_key);
            return encrypt_data.ProcessData(data);
        }

        public override byte[] DecryptData(byte[] p_data)
        {
            if (NeedToGenerateKey())
            {
                // krijimi i nje celesi te ri para se me eksportu 
                CreateKeyPair(KeySizeValue);
            }
            // dekriptimi i te dhenave
            ElGamalDecrypt decrypt_data = new ElGamalDecrypt(current_key);
            return decrypt_data.ProcessData(p_data);
        }
        public override byte[] Sign(byte[] p_hashcode)
        {
            throw new System.NotImplementedException();
        }
        public override bool VerifySignature(byte[] p_hashcode, byte[] p_signature)
        {
            throw new System.NotImplementedException();
        }
    }

}