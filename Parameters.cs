using System;
using System.Security.Cryptography;

namespace ElGamal
{
    public abstract class Parameters : AsymmetricAlgorithm
    {
         [Serializable]
        public struct ElGamalParameters
        {
            public byte[] P;
            public byte[] G;
            public byte[] Y;
            [NonSerialized] public byte[] X;
        } 
        
        //Metoda: Import ElGamalParameters
        public abstract void ImportParameters(ElGamalParameters import_parameters);
       
    }
}