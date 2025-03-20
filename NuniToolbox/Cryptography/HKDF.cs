using System.Security.Cryptography;

namespace NuniToolbox.Cryptography;

/// <summary>
/// Implementation of RFC 5869 - HMAC-based Extract-and-Expand Key Derivation Function (https://tools.ietf.org/html/rfc5869)
/// </summary>
public class HKDF
{
    private Func<HMAC> m_hmacFactory;

    /// <summary>
    /// Creates a new instance of <see cref="HKDF" />.
    /// </summary>
    /// <param name="hmacFactory">Factory method for the HMAC used by the HKDF</param>
    public HKDF(Func<HMAC> hmacFactory)
    {
        m_hmacFactory = hmacFactory ?? throw new ArgumentNullException(nameof(hmacFactory));
    }

    /// <summary>
    /// Extracts the PRK out of the IKM and then expands it to the OKM.
    /// </summary>
    /// <param name="ikm">The input keying material to extract a pseudorandom key (PRK) out of</param>
    /// <param name="salt">Optional salt used as key for the internal HMAC operation; will be set to a hash length zeroed array if null or empty</param>
    /// <param name="info">An optional context and application specific information</param>
    /// <param name="length">The length of the output keying material in bytes (&lt;= 255 * hash length)</param>
    /// <returns>The expanded output keying material (OKM)</returns>
    public byte[] DeriveKey(byte[] ikm, byte[] salt, byte[] info, int length)
    {
        byte[] prk = null;

        try
        {
            prk = Extract(ikm, salt);

            return Expand(prk, info, length);
        }
        finally
        {
            if (prk is not null)
            {
                // don't keep the PRK in memory
                Array.Clear(prk, 0, prk.Length);
            }
        }
    }

    /// <summary>
    /// Implements step 1: Extract
    /// </summary>
    /// <param name="ikm">The input keying material to extract a pseudorandom key (PRK) out of</param>
    /// <param name="salt">Optional salt used as key for the internal HMAC operation; will be set to a hash length zeroed array if null or empty</param>
    /// <returns>The extracted pseudorandom key (PRK)</returns>
    public byte[] Extract(byte[] ikm, byte[] salt)
    {
        using HMAC hmac = m_hmacFactory.Invoke();

        if (salt == null || salt.Length == 0)
        {
            salt = new byte[hmac.HashSize / 8];
        }

        hmac.Key = salt;

        return hmac.ComputeHash(ikm);
    }

    /// <summary>
    /// Implements step 2: Expand
    /// </summary>
    /// <param name="prk">A pseudorandom key used as key for the internal HMAC operation</param>
    /// <param name="info">An optional context and application specific information</param>
    /// <param name="length">The length of the output keying material in bytes (&lt;= 255 * hash length)</param>
    /// <returns>The expanded output keying material (OKM)</returns>
    public byte[] Expand(byte[] prk, byte[] info, int length)
    {
        using HMAC hmac = m_hmacFactory.Invoke();

        if (prk == null || prk.Length == 0)
        {
            throw new ArgumentException($"{nameof(prk)} must not be null or empty", nameof(prk));
        }

        int hashLength = hmac.HashSize / 8;

        if (length < 1 || length > 255 * hashLength)
        {
            throw new ArgumentException($"{nameof(length)} must be: 1 <= {nameof(length)} <= {255 * hashLength:0,000} (255 * hash length in bytes)", nameof(length));
        }

        info ??= [];

        byte[] lastT = [];

        try
        {
            int n = (int)Math.Ceiling((double)length / hashLength);
            byte[] finalKey = new byte[length];
            int nPendingBytes = length;

            hmac.Key = prk;

            for (int i = 0; i < n && nPendingBytes > 0; i++)
            {
                byte[] input = PrepareExpandInput(lastT, info, i + 1);

                if (lastT.Length > 0)
                {
                    // don't keep any part of the OKM in memory, if not needed anymore
                    Array.Clear(lastT, 0, lastT.Length);
                }

                lastT = hmac.ComputeHash(input);

                Buffer.BlockCopy(lastT, 0, finalKey, i * hashLength, Math.Min(lastT.Length, nPendingBytes));

                nPendingBytes -= lastT.Length;
            }

            return finalKey;
        }
        finally
        {
            if (lastT.Length > 0)
            {
                // don't keep any part of the OKM in memory, if not needed anymore
                Array.Clear(lastT, 0, lastT.Length);
            }
        }
    }

    private byte[] PrepareExpandInput(byte[] lastT, byte[] info, int i)
    {
        byte[] input = new byte[lastT.Length + info.Length + 1];

        if (lastT.Length > 0)
        {
            Buffer.BlockCopy(lastT, 0, input, 0, lastT.Length);
        }

        if (info.Length > 0)
        {
            Buffer.BlockCopy(info, 0, input, lastT.Length, info.Length);
        }

        input[^1] = (byte)i;

        return input;
    }
}
