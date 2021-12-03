using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASP.NET_GUI.Models;
using System.Text;
using ASP.NET_GUI.Classes;

namespace ASP.NET_GUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Encryption()
        {
            return View();
        }

        public IActionResult Decryption()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Encryption(String plaintext)
        {
            ElGamalModel encryptModel = new ElGamalModel();

            if (plaintext == null)
            {
                encryptModel.plainError = "Plaintext nuk duhet të jetë i zbrazët!";

            }
            else
            {
                
                byte[] plaintexti = Encoding.Default.GetBytes(plaintext);

                //generate keys
                Parameters _parameters = new ElGamalManagement();

                // set key size
                _parameters.KeySize = 384;

                // extract and print the xml string
                string xml_string = _parameters.ToXmlString(true);

                Parameters encrypt = new ElGamalManagement();
        
                encrypt.FromXmlString(_parameters.ToXmlString(false));
                byte[] ciphertext = _parameters.EncryptData(plaintexti);

                encryptModel.plaintext = Encoding.UTF8.GetString(plaintexti);
                encryptModel.ciphertext = Convert.ToBase64String(ciphertext);
                encryptModel.plainError = "";
                
                Parameters decrypt = new ElGamalManagement();

                decrypt.FromXmlString(_parameters.ToXmlString(true));
                byte[] potential_plaintext = decrypt.DecryptData(ciphertext);

                encryptModel.potential_plaintext =  Encoding.UTF8.GetString(potential_plaintext);
            }

            return View(encryptModel);
        }

        [HttpPost]
        public ActionResult Decryption(byte[] ciphertext)
        {
            ElGamalModel encryptModel = new ElGamalModel();

            
                // byte[] plaintexti = Encoding.Default.GetBytes(plaintext);

                //generate keys
                Parameters _parameters = new ElGamalManagement();

                // set key size
                _parameters.KeySize = 384;

                // // extract and print the xml string
                // string xml_string = _parameters.ToXmlString(true);

                // Parameters encrypt = new ElGamalManagement();
        
                // encrypt.FromXmlString(_parameters.ToXmlString(false));
                // byte[] ciphertext = _parameters.EncryptData(plaintexti);

                
                Parameters decrypt = new ElGamalManagement();

                decrypt.FromXmlString(_parameters.ToXmlString(true));
                byte[] plaintext = decrypt.DecryptData(ciphertext);

                encryptModel.ciphertext = Convert.ToBase64String(ciphertext);
                encryptModel.plaintext = Convert.ToBase64String(plaintext);
                encryptModel.cipherError = "";

            // else
            // {
            //     encryptModel.cipherError = "Ciphertext nuk duhet të jetë i zbrazët!";
            // }

            return View(encryptModel);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
