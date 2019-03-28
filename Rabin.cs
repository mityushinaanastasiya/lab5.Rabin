using System;
using System.Numerics;
using System.IO;
using System.Collections.Generic;

namespace lab5.Rabin
{
    class Rabin
    {
        //работает//static BigInteger p = 199, q = 139, OpenyKey = p * q;
        static BigInteger p = 3004913, q = 20979403, OpenyKey = p * q; // не работает(
        public static void EncryptionBigText()
        {
            string textPath = @".\text\example.txt";
            FileStream fstream = new FileStream(textPath, FileMode.Open);
            StreamWriter streamWriter = new StreamWriter(@".\text\code.txt");

            //определение длины сообщения М
            //должно выполнятся условие M<C
            //Чтобы оно было всегда меньше, то я беру блок на один бит меньше 
            byte[] array = OpenyKey.ToByteArray();
            int maxLenghtM = array.Length - 1;

            //массив для записи в него сообщения М
            byte[] buf = new byte [maxLenghtM];

            BigInteger M, C;

            while (fstream.Length - fstream.Position > maxLenghtM)
            {
                fstream.Read(buf, 0, maxLenghtM);
                //M = new BigInteger(buf);
                M = 53041280806939; //для отладки
                C = BigInteger.ModPow(M, 2, OpenyKey);
                streamWriter.Write(C.ToString() + " ");

                //Для отладки. чтобы сразу провреить правильно ли декодируется наш шифр
                DecryptionBigText(C);
            }
            buf = new byte[fstream.Length - fstream.Position];
            fstream.Read(buf, 0, Convert.ToInt32(fstream.Length - fstream.Position));
            M = new BigInteger(buf);
            C = BigInteger.ModPow(M, 2, OpenyKey);
            streamWriter.Write(C.ToString());
        }

        public static void DecryptionBigText(BigInteger C)
        {
            //чтение из файла
            //string textPath = @".\text\code.txt";
            //StreamReader streamReader = new StreamReader(textPath);
            //string c = "";
            //BigInteger C;
            //int sr;
            //while (!streamReader.EndOfStream)
            //{
            //    sr = streamReader.Read();
            //    while (sr!= 32)
            //    {
            //        c = c + Convert.ToString(sr);
            //        sr = streamReader.Read();
            //    }
            //    C = new BigInteger();
            //    C = BigInteger.Parse(c);
            //    //вставить код, который далее
            //}

            BigInteger m1, m2, m3, m4, a, b, M1, M2, M3, M4;
            m1 = BigInteger.ModPow(C, (p + 1) / 4, p);
            m2 = BigInteger.ModPow(subtraction(BigInteger.ModPow(p, 1, p), m1, p), 1, p);
            m3 = BigInteger.ModPow(C, (q + 1) / 4, q);
            m4 = BigInteger.ModPow(subtraction(BigInteger.ModPow(q, 1, q), m3, q), 1, q);
            a = BigInteger.Multiply(q, BigInteger.ModPow(q, p - 2, p));
            b = BigInteger.Multiply(p, BigInteger.ModPow(p, q - 2, q));
            M1 = BigInteger.ModPow( BigInteger.Multiply(a, m1) + BigInteger.Multiply(b, m3), 1, OpenyKey);
            M2 = BigInteger.ModPow(BigInteger.Multiply(a, m1) + BigInteger.Multiply(b, m4), 1, OpenyKey);
            M3 = BigInteger.ModPow(BigInteger.Multiply(a, m2) + BigInteger.Multiply(b, m3), 1, OpenyKey);
            M4 = BigInteger.ModPow(BigInteger.Multiply(a, m2) + BigInteger.Multiply(b, m4), 1, OpenyKey);

        }

        static BigInteger subtraction (BigInteger minuend, BigInteger subtrahend, BigInteger mod)
        {
            BigInteger result = minuend - subtrahend;
            return result < 0 ? minuend + (mod -subtrahend) : result;
        }
    }
}