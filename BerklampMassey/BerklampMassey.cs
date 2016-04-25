using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerklampMassey
{
    class BerklampMassey
    {

        public static int modulo2sum(int a, int b)
        {
            return (a == b) ? 0 : 1;
        }

        public static int BerklampMasseyLinearity(byte[] binarySequence, out byte[] connectionPolynomial)
        {
            int linearComplexity, index, d, m;
            int n = binarySequence.Length;
            connectionPolynomial  = new byte[n];
            byte[] b = new byte[n];
            byte[] t = new byte[n];

            b[0] = connectionPolynomial[0] = 1;
            linearComplexity = index = 0;
            m = -1;

            while (index < n)
            {
                d = binarySequence[index];
                for (int i = 1; i <= linearComplexity; i++)
                    d = modulo2sum(d, connectionPolynomial[i] * binarySequence[index - i]);

                if (d == 1)
                {
                    Array.Copy(connectionPolynomial, t, n);
                    for (int i = 0; (i + index - m) < n; i++)
                        connectionPolynomial[i + index - m] ^= b[i];

                    if (linearComplexity <= (index / 2))
                    {
                        linearComplexity = index + 1 - linearComplexity;
                        m = index;
                        Array.Copy(t, b, n);
                    }
                }
                index++;
            }
            return linearComplexity;
        }

         static void Main(string[] args)
        {
            byte[] byteSequence = new byte[] { 1, 0, 0, 1, 1, 0, 0, 0, 1 };
            byte[] connectionPolynomial;
            int linearComplexity = BerklampMasseyLinearity(byteSequence, out connectionPolynomial);
            Console.WriteLine("Linear span: " + linearComplexity);
            foreach(var byteSeq in byteSequence)
            {
                Console.Write(byteSeq.ToString() + " ");
            }
            Console.ReadKey();
        }
    }
}
