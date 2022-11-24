/*************************************************************************
 *  Copyright © 2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DirectoryUtility.cs
 *  Description  :  Utility for directory.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/25/2019
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Common.Utility;
using System;
using System.Collections.Generic;
using System.IO;

namespace MGS.Common.IO
{
    /// <summary>
    /// Utility for directory.
    /// </summary>
    public sealed class DirectoryUtility
    {
        #region Public Method
        /// <summary>
        /// Require the directory of path exist.
        /// </summary>
        /// <param name="path">Directory or file path.</param>
        public static void RequireDirectory(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (Directory.Exists(dir))
            {
                return;
            }

            Directory.CreateDirectory(dir);
        }

        /// <summary>
        /// Copy the children entries of source to dest directory.
        /// </summary>
        /// <param name="sourceDir">Source dir.</param>
        /// <param name="destDir">Dest dir.</param>
        /// <param name="ignores">Ignore files or directories.</param>
        /// <param name="progressCallback">Progress callback.</param>
        /// <param name="finishedCallback">finished callback.</param>
        public static void CopyChildrenEntries(string sourceDir, string destDir, IEnumerable<string> ignores = null,
            Action<float> progressCallback = null, Action<bool, Exception> finishedCallback = null)
        {
            var entries = Directory.GetFileSystemEntries(sourceDir);
            if (entries == null || entries.Length == 0)
            {
                ActionUtility.Invoke(progressCallback, 1.0f);
                ActionUtility.Invoke(finishedCallback, true, null);
                return;
            }

            if (!Directory.Exists(destDir))
            {
                try
                {
                    Directory.CreateDirectory(destDir);
                }
                catch (Exception ex)
                {
                    ActionUtility.Invoke(finishedCallback, false, ex);
                    return;
                }
            }

            var ignoreList = new List<string>();
            if (ignores != null)
            {
                ignoreList.AddRange(ignores);
            }

            var finishCount = 0;
            foreach (var entrie in entries)
            {
                try
                {
                    //Sub directory.
                    if (Directory.Exists(entrie))
                    {
                        var dirInfo = new DirectoryInfo(entrie);
                        if (!ignoreList.Contains(dirInfo.Name))
                        {
                            var cloneDirName = destDir + "/" + dirInfo.Name;
                            if (!Directory.Exists(cloneDirName))
                            {
                                Directory.CreateDirectory(cloneDirName);
                            }

                            CopyChildrenEntries(entrie, cloneDirName, ignores,
                                progress =>
                                {
                                    var childProgress = (finishCount + progress) / entries.Length;
                                    ActionUtility.Invoke(progressCallback, childProgress);
                                },
                                (succeed, error) =>
                                {
                                    if (!succeed)
                                    {
                                        ActionUtility.Invoke(finishedCallback, succeed, error);
                                        return;
                                    }
                                    finishCount++;
                                });
                            continue;
                        }
                    }
                    else  //File.
                    {
                        var fileName = Path.GetFileName(entrie);
                        if (!ignoreList.Contains(fileName))
                        {
                            var cloneFileName = destDir + "/" + Path.GetFileName(entrie);
                            File.Copy(entrie, cloneFileName, true);
                        }
                    }

                    finishCount++;
                    var totalProgress = (float)finishCount / entries.Length;
                    ActionUtility.Invoke(progressCallback, totalProgress);
                }
                catch (Exception ex)
                {
                    ActionUtility.Invoke(finishedCallback, false, ex);
                }
            }
            ActionUtility.Invoke(finishedCallback, true, null);
        }

        /// <summary>
        /// Delete the children entries of the directory.
        /// </summary>
        /// <param name="destDir">Dest dir.</param>
        /// <param name="ignores">Ignore files or directories.</param>
        /// <param name="progressCallback">Progress callback.</param>
        /// <param name="finishedCallback">finished callback.</param>
        public static void DeleteChildrenEntries(string destDir, IEnumerable<string> ignores = null,
            Action<float> progressCallback = null, Action<bool, Exception> finishedCallback = null)
        {
            var entries = Directory.GetFileSystemEntries(destDir);
            if (entries == null || entries.Length == 0)
            {
                ActionUtility.Invoke(progressCallback, 1.0f);
                ActionUtility.Invoke(finishedCallback, true, null);
                return;
            }

            var ignoreList = new List<string>();
            if (ignores != null)
            {
                ignoreList.AddRange(ignores);
            }

            var finishCount = 0;
            foreach (var entrie in entries)
            {
                try
                {
                    //Sub directory.
                    if (Directory.Exists(entrie))
                    {
                        var dirInfo = new DirectoryInfo(entrie);
                        if (!ignoreList.Contains(dirInfo.Name))
                        {
                            dirInfo.Delete(true);
                        }
                    }
                    else  //File.
                    {
                        var fileName = Path.GetFileName(entrie);
                        if (!ignoreList.Contains(fileName))
                        {
                            File.Delete(entrie);
                        }
                    }

                    finishCount++;
                    var progress = (float)finishCount / entries.Length;
                    ActionUtility.Invoke(progressCallback, progress);
                }
                catch (Exception ex)
                {
                    ActionUtility.Invoke(finishedCallback, false, ex);
                }
            }
            ActionUtility.Invoke(finishedCallback, true, null);
        }
        #endregion
    }
}