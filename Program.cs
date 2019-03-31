using System;
using System.Numerics;

namespace lab5.Rabin
{
    class Program
    {
        static void Main(string[] args)
        {      
            
            try
            {
                Rabin.EncryptionBigText();
                Rabin.DecryptionBigText();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }  
    }
    }
}
