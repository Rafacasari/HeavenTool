using Force.Crc32;
using System;
using System.Text;

namespace HeavenTool.Utility
{
    public static class HashUtility
    {
        public static uint ToCRC32(this string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            return Crc32Algorithm.Compute(bytes);
        }

        public static uint ToMurmur(this string value)
        {
            ReadOnlySpan<byte> inputSpan = Encoding.UTF8.GetBytes(value).AsSpan();

            return Murmur.Hash32(ref inputSpan, 0);
        }
    }
}
