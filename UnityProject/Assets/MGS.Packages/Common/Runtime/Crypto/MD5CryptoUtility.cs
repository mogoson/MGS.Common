/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MD5CryptoUtility.cs
 *  Description  :  Utility for MD5 crypto service provider.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/23/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.IO;
using System.Security.Cryptography;

namespace MGS.Common.Crypto
{
    /// <summary>
    /// Utility for MD5 crypto service provider.
    /// </summary>
    public sealed class MD5CryptoUtility
    {
        /// <summary>
        /// MD5CryptoServiceProvider for utility.
        /// </summary>
        private static MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        /// <summary>
        /// Compute hash of byte array.
        /// </summary>
        /// <param name="buffer">Source byte array.</param>
        /// <returns>Hash code.</returns>
        public static string ComputeHash(byte[] buffer)
        {
            var hashBytes = md5.ComputeHash(buffer);
            return BitConverter.ToString(hashBytes);
        }

        /// <summary>
        /// Compute hash of string.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns>Hash code.</returns>
        public static string ComputeHash(string source)
        {
            var sourceBytes = System.Text.Encoding.Default.GetBytes(source);
            return ComputeHash(sourceBytes);
        }

        /// <summary>
        /// Compute hash of file.
        /// </summary>
        /// <param name="filePath">Path of source file.</param>
        /// <returns>Hash code.</returns>
        public static string ComputeFileHash(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var md5 = new MD5CryptoServiceProvider();
                var hashBytes = md5.ComputeHash(stream);
                return BitConverter.ToString(hashBytes);
            }
        }
    }
}