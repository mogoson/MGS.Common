/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ProcessUtility.cs
 *  Description  :  Utility for process.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/23/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using System.Diagnostics;

namespace MGS.Common.Diagnostics
{
    /// <summary>
    /// Utility for process.
    /// </summary>
    public sealed class ProcessUtility
    {
        /// <summary>
        /// Start process from file.
        /// </summary>
        /// <param name="fileName">Path of process file.</param>
        public static void StartProcess(string fileName)
        {
            Process.Start(fileName);
        }

        /// <summary>
        /// Kill process by name.
        /// </summary>
        /// <param name="processName">Name of process.</param>
        public static void KillProcess(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            if (processes == null || processes.Length == 0)
            {
                return;
            }

            foreach (var process in processes)
            {
                if (process.HasExited)
                {
                    continue;
                }

                process.Kill();
            }
        }

        /// <summary>
        /// Kill processes by names.
        /// </summary>
        /// <param name="processNames">Names of processes.</param>
        public static void KillProcess(IEnumerable<string> processNames)
        {
            foreach (var processName in processNames)
            {
                KillProcess(processName);
            }
        }
    }
}