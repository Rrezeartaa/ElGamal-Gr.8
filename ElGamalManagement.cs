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

    }
}