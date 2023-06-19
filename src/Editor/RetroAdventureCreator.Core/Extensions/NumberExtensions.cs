using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Core.Extensions;

    internal static class NumberExtensions
    {
        public static string ToHexString(this int n, int numDigits) => string.Format($"{{0:X{numDigits}}}", n);

        public static string ToHexString(this uint n, int numDigits) => string.Format($"{{0:X{numDigits}}}", n);

        public static string ToHexString(this long n, int numDigits) => string.Format($"{{0:X{numDigits}}}", n);

        public static string ToHexString(this ulong n, int numDigits) => string.Format($"{{0:X{numDigits}}}", n);

        public static bool HasBit(this byte b, int index) => (b & (1 << index)) != 0;

        public static bool HasBit(this int b, int index) => (b & (1 << index)) != 0;

        public static bool HasBit(this ushort b, int index) => (b & (1 << index)) != 0;

        public static byte BinToBCD(this byte v) => (byte)(((v / 10) * 16) + (v % 10));

        public static byte BCDtoBin(this byte v) => (byte)(((v / 16) * 10) + (v % 16));
    }
