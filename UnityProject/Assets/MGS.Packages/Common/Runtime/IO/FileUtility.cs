/*************************************************************************
 *  Copyright © 2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  FileUtility.cs
 *  Description  :  Utility for file.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/25/2019
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.IO;
using System.Text;

namespace MGS.Common.IO
{
    /// <summary>
    /// Utility for file.
    /// </summary>
    public sealed class FileUtility
    {
        #region Public Method
        /// <summary>
        /// Calculate page count of file.
        /// </summary>
        /// <param name="filePath">Path of target file.</param>
        /// <param name="pageSize">Size of page (byte).</param>
        /// <returns>Page count of file.</returns>
        public static int CalPageCount(string filePath, int pageSize = 65536)
        {
            using (var sm = new FileStream(filePath, FileMode.Open))
            {
                return sm.Length / pageSize + sm.Length % pageSize == 0 ? 0 : 1;
            }
        }

        /// <summary>
        /// Read the index page of file.
        /// </summary>
        /// <param name="filePath">Path of target file.</param>
        /// <param name="pageSize">Size of page (byte).</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <returns>Index page bytes.</returns>
        public static byte[] ReadPage(string filePath, int pageSize = 65536, int pageIndex = 0)
        {
            using (var sm = new FileStream(filePath, FileMode.Open))
            {
                var pageCount = sm.Length / pageSize + sm.Length % pageSize == 0 ? 0 : 1;
                if (pageIndex > pageCount - 1)
                {
                    var msg = string.Format("The pageIndex {0} is out of range.", pageCount);
                    throw new ArgumentOutOfRangeException(msg);
                }

                if (!sm.CanSeek || !sm.CanRead)
                {
                    var msg = string.Format("File stream can not seek or read for the file {0}", filePath);
                    throw new FileLoadException(msg);
                }

                var start = pageSize * pageIndex;
                var count = Math.Min(pageSize, sm.Length - start);
                var bytesArray = new byte[count];

                sm.Seek(start, SeekOrigin.Begin);
                sm.Read(bytesArray, 0, (int)count);
                return bytesArray;
            }
        }

        /// <summary>
        /// Read all lines of file.
        /// </summary>
        /// <param name="filePath">Path of target file.</param>
        /// <param name="encoding">Encoding of target file.</param>
        /// <returns>All lines from file.</returns>
        public static string[] ReadAllLines(string filePath, Encoding encoding)
        {
            return File.ReadAllLines(filePath, encoding);
        }
        #endregion
    }
}