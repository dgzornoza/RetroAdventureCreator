namespace RetroAdventureCreator.Core.Extensions;

internal static class NumberExtensions
    {
        public static byte ToBaseZero(this byte number) => (byte)(number - 1);

        public static string ToHexString(this int number, int numDigits) => string.Format($"{{0:X{numDigits}}}", number);

        public static string ToHexString(this uint number, int numDigits) => string.Format($"{{0:X{numDigits}}}", number);

        public static string ToHexString(this long number, int numDigits) => string.Format($"{{0:X{numDigits}}}", number);

        public static string ToHexString(this ulong number, int numDigits) => string.Format($"{{0:X{numDigits}}}", number);

        public static bool HasBit(this byte number, int index) => (number & (1 << index)) != 0;

        public static bool HasBit(this int number, int index) => (number & (1 << index)) != 0;

        public static bool HasBit(this ushort number, int index) => (number & (1 << index)) != 0;

        public static byte BinToBCD(this byte number) => (byte)(((number / 10) * 16) + (number % 10));

        public static byte BCDtoBin(this byte number) => (byte)(((number / 16) * 10) + (number % 16));

        public static byte GetByte(this short number, int index) => (byte)(number >> (--index * 8));
    }
