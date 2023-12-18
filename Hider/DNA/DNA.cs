using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hider
{
    class DNA
    {
        //public static GenerateSeed byte[] DNA2_Complement = { 13, 2, 8, 7, 1, 4, 14, 5, 0, 15, 3, 10, 11, 6, 9, 12 };
        static int[] GenerateSeed(byte[] data)
        {
            var hash = SHA1.Create().ComputeHash(data);
            int[] ans = new int[hash.Length / sizeof(int)];
            Buffer.BlockCopy(hash, 0, ans, 0, hash.Length);
            ans[0] ^= ans[2];
            ans[1] ^= ans[3];
            return ans;
        }

        public enum Nucleic : byte
        {
            A = 0,
            C = 1,
            G = 2,
            T = 3
        }

        public static readonly byte[] DNA_Complement = { 3, 0, 1, 2 };
        public static byte Complement(byte nucleic)
        {
            return nucleic < 0x4 ? DNA_Complement[nucleic] : (byte)0xFF;
        }
        public static byte[] GetCoverFromFrames(List<Bitmap> frames, int size)
        {
            return null;
        }
        public static byte[] GenerateSequence(byte[] cover, int seed)
        {
            Random random = new Random(seed);

            int length = cover.Length;
            byte[] sequence = new byte[length];
            int[] counter = new int[4];

            foreach (var nucleic in cover)
            {
                ++counter[nucleic];
            }

            for (int i = 1; i < 4; ++i)
            {
                counter[i] += counter[i - 1];
            }

            for (int i = 0; i < length; ++i)
            {
                var number = random.Next(0, counter[3]);
                if (number < counter[0])
                {
                    sequence[i] = 0; // A
                }
                else if (number < counter[1])
                {
                    sequence[i] = 1; // C
                }
                else if (number < counter[2])
                {
                    sequence[i] = 2; // G
                }
                else
                {
                    sequence[i] = 3;
                }
            }
            return sequence;
        }
        public static byte[] HideMessage(byte[] cover, byte[] data, byte[] key)
        {
            return null;
        }

        public static int[] GeneratePermutation(int n, int seed)
        {
            int[] permutation = new int[n];
            for (int i = 0; i < n; ++i)
            {
                permutation[i] = i;
            }

            Random random = new Random(seed);

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                int temp = permutation[i];
                permutation[i] = permutation[j];
                permutation[j] = temp;
            }

            return permutation;
        }
        public static byte[] Encrypt(List<Bitmap> frames, byte[] data, byte[] key)
        {
            int[] seed = GenerateSeed(key);

            byte[] cover = GetCoverFromFrames(frames, data.Length);
            byte[] sequence = GenerateSequence(cover, seed[0]);

            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = (byte)(data[i] ^ sequence[i]); // xor encrypt
            }

            byte[] subCover = new byte[cover.Length];
            cover.CopyTo(subCover, 0);
            int[] permutation = GeneratePermutation(data.Length, seed[1]);

            for (int i = 0; i < data.Length; ++i)
            {
                switch (data[i])
                {
                    case 0: // A
                        subCover[permutation[i]] = cover[permutation[i]];
                        break;
                    case 1: // C
                        subCover[permutation[i]] = Complement(cover[permutation[i]]);
                        break;
                    case 2: // G
                        subCover[permutation[i]] =
                                    Complement(Complement(cover[permutation[i]]));
                        break;
                    case 3: // T
                        subCover[permutation[i]] =
                                    Complement(Complement(Complement(cover[permutation[i]])));
                        break;
                }
            }

            return subCover;
        }

        public static byte[] Encrypt(byte[] cover, byte[] data, byte[] key)
        {
            int[] seed = GenerateSeed(key);

            byte[] sequence = GenerateSequence(cover, seed[0]);

            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = (byte)(data[i] ^ sequence[i]); // xor encrypt
            }

            byte[] subCover = new byte[cover.Length];
            cover.CopyTo(subCover, 0);
            int[] permutation = GeneratePermutation(data.Length, seed[1]);

            for (int i = 0; i < data.Length; ++i)
            {
                switch (data[i])
                {
                    case 0: // A
                        subCover[permutation[i]] = cover[permutation[i]];
                        break;
                    case 1: // C
                        subCover[permutation[i]] = Complement(cover[permutation[i]]);
                        break;
                    case 2: // G
                        subCover[permutation[i]] =
                                    Complement(Complement(cover[permutation[i]]));
                        break;
                    case 3: // T
                        subCover[permutation[i]] =
                                    Complement(Complement(Complement(cover[permutation[i]])));
                        break;
                }
            }

            return subCover;
        }

        public static byte[] Decrypt(List<Bitmap> frames, byte[] subCover, byte[] key)
        {
            int[] seed = GenerateSeed(key);

            byte[] cover = GetCoverFromFrames(frames, subCover.Length);
            int[] permutation = GeneratePermutation(subCover.Length, seed[1]);

            byte[] data = new byte[cover.Length];
            for (int i = 0; i < subCover.Length; ++i)
            {
                if (subCover[permutation[i]] == cover[permutation[i]])
                {
                    data[i] = 0;
                }
                else if (subCover[permutation[i]] == Complement(cover[permutation[i]]))
                {
                    data[i] = 1;
                }
                else if (subCover[permutation[i]] == Complement(Complement(cover[permutation[i]])))
                {
                    data[i] = 2;
                }
                else
                {
                    data[i] = 3;
                }
            }

            byte[] sequence = GenerateSequence(cover, seed[0]);

            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = (byte)(data[i] ^ sequence[i]); // xor decrypt
            }

            return data;
        }

        public static byte[] Decrypt(byte[] cover, byte[] subCover, byte[] key)
        {
            int[] seed = GenerateSeed(key);

            int[] permutation = GeneratePermutation(subCover.Length, seed[1]);

            byte[] data = new byte[cover.Length];
            for (int i = 0; i < subCover.Length; ++i)
            {
                if (subCover[permutation[i]] == cover[permutation[i]])
                {
                    data[i] = 0;
                }
                else if (subCover[permutation[i]] == Complement(cover[permutation[i]]))
                {
                    data[i] = 1;
                }
                else if (subCover[permutation[i]] == Complement(Complement(cover[permutation[i]])))
                {
                    data[i] = 2;
                }
                else
                {
                    data[i] = 3;
                }
            }

            byte[] sequence = GenerateSequence(cover, seed[0]);

            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = (byte)(data[i] ^ sequence[i]); // xor decrypt
            }

            return data;
        }
    }
}
