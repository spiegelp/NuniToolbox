using System.Security.Cryptography;
using System.Text;

namespace NuniToolbox.Test.Cryptography;

public class HKDFTest
{
    // test cases taken from the RFC 5869 (https://tools.ietf.org/html/rfc5869)

    public HKDFTest() { }

    [Theory]
    [InlineData("0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b",
                "000102030405060708090a0b0c",
                "077709362c2e32df0ddc3f0dc47bba6390b6c73bb50f9c3122ec844ad7c2b3e5")]
    [InlineData("000102030405060708090a0b0c0d0e0f101112131415161718191a1b1c1d1e1f202122232425262728292a2b2c2d2e2f303132333435363738393a3b3c3d3e3f404142434445464748494a4b4c4d4e4f",
                "606162636465666768696a6b6c6d6e6f707172737475767778797a7b7c7d7e7f808182838485868788898a8b8c8d8e8f909192939495969798999a9b9c9d9e9fa0a1a2a3a4a5a6a7a8a9aaabacadaeaf",
                "06a6b88c5853361a06104c9ceb35b45cef760014904671014a193f40c15fc244")]
    [InlineData("0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b",
                "",
                "19ef24a32c717b167f33a91d6f648bdf96596776afdb6377ac434c1c293ccb04")]
    public void Test_Extract_SHA256_Ok(string ikmStr, string saltStr, string prkStr)
    {
        byte[] ikm = ConvertHexStringToBytes(ikmStr);
        byte[] salt = ConvertHexStringToBytes(saltStr);

        NuniToolbox.Cryptography.HKDF hkdf = new(() => new HMACSHA256());
        byte[] prk = hkdf.Extract(ikm, salt);

        Assert.NotNull(prk);
        Assert.Equal(prkStr, ConvertBytesToHexString(prk));
    }

    [Theory]
    [InlineData("077709362c2e32df0ddc3f0dc47bba6390b6c73bb50f9c3122ec844ad7c2b3e5",
                "f0f1f2f3f4f5f6f7f8f9",
                42,
                "3cb25f25faacd57a90434f64d0362f2a2d2d0a90cf1a5a4c5db02d56ecc4c5bf34007208d5b887185865")]
    [InlineData("06a6b88c5853361a06104c9ceb35b45cef760014904671014a193f40c15fc244",
                "b0b1b2b3b4b5b6b7b8b9babbbcbdbebfc0c1c2c3c4c5c6c7c8c9cacbcccdcecfd0d1d2d3d4d5d6d7d8d9dadbdcdddedfe0e1e2e3e4e5e6e7e8e9eaebecedeeeff0f1f2f3f4f5f6f7f8f9fafbfcfdfeff",
                82,
                "b11e398dc80327a1c8e7f78c596a49344f012eda2d4efad8a050cc4c19afa97c59045a99cac7827271cb41c65e590e09da3275600c2f09b8367793a9aca3db71cc30c58179ec3e87c14c01d5c1f3434f1d87")]
    [InlineData("19ef24a32c717b167f33a91d6f648bdf96596776afdb6377ac434c1c293ccb04",
                "",
                42,
                "8da4e775a563c18f715f802a063c5a31b8a11f5c5ee1879ec3454e5f3c738d2d9d201395faa4b61a96c8")]
    public void Test_Expand_SHA256_Ok(string prkStr, string infoStr, int length, string okmStr)
    {
        byte[] prk = ConvertHexStringToBytes(prkStr);
        byte[] info = ConvertHexStringToBytes(infoStr);

        NuniToolbox.Cryptography.HKDF hkdf = new(() => new HMACSHA256());
        byte[] okm = hkdf.Expand(prk ,info, length);

        Assert.NotNull(okm);
        Assert.Equal(length, okm.Length);
        Assert.Equal(okmStr, ConvertBytesToHexString(okm));
    }

    [Theory]
    [InlineData("0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b",
                "000102030405060708090a0b0c",
                "f0f1f2f3f4f5f6f7f8f9",
                42,
                "3cb25f25faacd57a90434f64d0362f2a2d2d0a90cf1a5a4c5db02d56ecc4c5bf34007208d5b887185865")]
    [InlineData("000102030405060708090a0b0c0d0e0f101112131415161718191a1b1c1d1e1f202122232425262728292a2b2c2d2e2f303132333435363738393a3b3c3d3e3f404142434445464748494a4b4c4d4e4f",
                "606162636465666768696a6b6c6d6e6f707172737475767778797a7b7c7d7e7f808182838485868788898a8b8c8d8e8f909192939495969798999a9b9c9d9e9fa0a1a2a3a4a5a6a7a8a9aaabacadaeaf",
                "b0b1b2b3b4b5b6b7b8b9babbbcbdbebfc0c1c2c3c4c5c6c7c8c9cacbcccdcecfd0d1d2d3d4d5d6d7d8d9dadbdcdddedfe0e1e2e3e4e5e6e7e8e9eaebecedeeeff0f1f2f3f4f5f6f7f8f9fafbfcfdfeff",
                82,
                "b11e398dc80327a1c8e7f78c596a49344f012eda2d4efad8a050cc4c19afa97c59045a99cac7827271cb41c65e590e09da3275600c2f09b8367793a9aca3db71cc30c58179ec3e87c14c01d5c1f3434f1d87")]
    [InlineData("0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b",
                "",
                "",
                42,
                "8da4e775a563c18f715f802a063c5a31b8a11f5c5ee1879ec3454e5f3c738d2d9d201395faa4b61a96c8")]
    public void Test_DeriveKey_SHA256_Ok(string ikmStr, string saltStr, string infoStr, int length, string okmStr)
    {
        byte[] ikm = ConvertHexStringToBytes(ikmStr);
        byte[] salt = ConvertHexStringToBytes(saltStr);
        byte[] info = ConvertHexStringToBytes(infoStr);

        NuniToolbox.Cryptography.HKDF hkdf = new(() => new HMACSHA256());
        byte[] okm = hkdf.DeriveKey(ikm, salt, info, length);

        Assert.NotNull(okm);
        Assert.Equal(length, okm.Length);
        Assert.Equal(okmStr, ConvertBytesToHexString(okm));
    }

    public string ConvertBytesToHexString(byte[] bytes)
    {
        StringBuilder sb = new StringBuilder(bytes.Length * 2);

        for (int i = 0; i < bytes.Length; i++)
        {
            sb.Append(string.Format("{0:X2}", bytes[i]));
        }

        return sb.ToString().ToLower();
    }

    public byte[] ConvertHexStringToBytes(string hexStr)
    {
        int length = hexStr.Length / 2;
        byte[] bytes = new byte[length];

        for (int i = 0; i < length; i++)
        {
            string hex = hexStr.Substring(i * 2, 2);
            bytes[i] = Convert.ToByte(hex, 16);
        }

        return bytes;
    }
}
