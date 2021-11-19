using System;
using System.Security.Cryptography;

namespace ElGamal
{
    public class Parameters
    {
         [Serializable]
        public struct ElGamalParameters
        {
            public byte[] P;
            public byte[] G;
            public byte[] Y;
            [NonSerialized] public byte[] X;
        } 
        
    }
}