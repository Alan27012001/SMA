using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SMA.Helpers
{
    public class Contraseña
    {
        private const string llave = "6&%FJcMj#.LO=1";
        private const int interacciones = 999;

        public byte[] Encriptar(string contraseña)
        {
            return EncriptarCadenaABytes(contraseña);
        }

        public bool CompararContraseña(byte[] contraseña1, byte[] contraseña2)
        {
            var cadena1 = Encoding.ASCII.GetString(contraseña1);
            var cadena2 = Encoding.ASCII.GetString(contraseña2);

            return cadena1.Equals(cadena2);
        }

        private byte[] EncriptarCadenaABytes(string contraseña)
        {
            var cadenacompleta = contraseña + llave;
            var sha = new SHA256Managed();
            byte[] bytes = null;

            try
            {
                bytes = Encoding.UTF8.GetBytes(cadenacompleta);
                for (var i = 0; i < interacciones; i++)
                    bytes = sha.ComputeHash(bytes);
            }
            finally { sha.Clear(); }

            return bytes;
        }
    }
}
