using System;
using System.Security.Cryptography;

namespace ElGamal
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


    }
}