using System;
using System.Collections.Generic;
using TonSdk.Core;

namespace Utils
{
    public static class TonAddressConverter
    {
        private const byte NoBounceableTag = 0x51;
        private const byte TestOnlyTag = 0x80;

        private static readonly Dictionary<string, byte> ToByteMap = new();

        static TonAddressConverter()
        {
            for (int ord = 0; ord <= 0xff; ord++)
            {
                string hex = ord.ToString("x2");
                ToByteMap[hex] = (byte)ord;
            }
        }
        
        public static string ToUserFriendlyAddress(this Address address)
        {
            (int Wc, byte[] Hex) parsedAddress = ParseHexAddress(address.ToString(AddressType.Raw));
            byte tag = NoBounceableTag;
            if (address.IsTestOnly())
                tag |= TestOnlyTag;

            byte[] addr = new byte[34];
            addr[0] = tag;
            addr[1] = (byte)parsedAddress.Wc;
            parsedAddress.Hex.CopyTo(addr, 2);

            byte[] addressWithChecksum = new byte[36];
            Array.Copy(addr, addressWithChecksum, addr.Length);
            byte[] checksum = Crc16(addr);
            addressWithChecksum[34] = checksum[0];
            addressWithChecksum[35] = checksum[1];

            return Convert.ToBase64String(addressWithChecksum)
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
        }

        private static (int Wc, byte[] Hex) ParseHexAddress(string hexAddress)
        {
            if (!hexAddress.Contains(":"))
                throw new Exception($"Wrong address {hexAddress}. Address must include \":\".");

            string[] parts = hexAddress.Split(':');
            if (parts.Length != 2)
                throw new Exception($"Wrong address {hexAddress}. Address must include \":\" only once.");

            int wc = int.Parse(parts[0]);
            if (wc != 0 && wc != -1)
                throw new Exception($"Wrong address {hexAddress}. WC must be 0 or -1, but {wc} received.");

            string hex = parts[1];
            if (hex.Length != 64)
                throw new Exception($"Wrong address {hexAddress}. Hex part must be 64 bytes, but {hex.Length} received.");

            return (wc, HexToBytes(hex));
        }

        private static byte[] Crc16(byte[] data)
        {
            const ushort poly = 0x1021;
            ushort reg = 0;

            foreach (byte b in data)
            {
                reg ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                {
                    if ((reg & 0x8000) != 0)
                        reg = (ushort)((reg << 1) ^ poly);
                    else
                        reg <<= 1;
                }
            }

            return new[] { (byte)(reg >> 8), (byte)(reg & 0xff) };
        }

        private static byte[] HexToBytes(string hex)
        {
            hex = hex.ToLower();
            if (hex.Length % 2 != 0)
                throw new Exception("Hex string must have a length multiple of 2: " + hex);

            byte[] result = new byte[hex.Length / 2];
            for (int i = 0; i < result.Length; i++)
            {
                string hexSubstring = hex.Substring(i * 2, 2);
                if (!ToByteMap.ContainsKey(hexSubstring))
                    throw new Exception("Invalid hex character: " + hexSubstring);

                result[i] = ToByteMap[hexSubstring];
            }

            return result;
        }
    }
}