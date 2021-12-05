using System;

namespace ASP.NET_GUI.Models
{
    public class ElGamalModel
    {
        public string plaintext { get; set; }

        public string potential_plaintext { get; set; }

        public string ciphertext { get; set; }

        public string xml_string { get; set; }

        public string parameters { get; set; }

        public string plainError { get; set; }

        public string cipherError { get; set; }

        public string error { get; set; }
    }
}
