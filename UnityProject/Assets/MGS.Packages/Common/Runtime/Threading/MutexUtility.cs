/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MutexUtility.cs
 *  Description  :  Utility for mutex..
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/2/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Common.Crypto;
using System;
using System.IO;
using System.Threading;

namespace MGS.Common.Threading
{
    /// <summary>
    /// Utility for mutex.
    /// </summary>
    public sealed class MutexUtility
    {
        /// <summary>
        /// Prefix of global mutex name.
        /// </summary>
        public const string MUTEX_PREFIX_GLOBAL = "Global/";

        /// <summary>
        /// Prefix of local mutex name.
        /// </summary>
        public const string MUTEX_PREFIX_LOCAL = "Local/";

        /// <summary>
        /// Get a name for mutex.
        /// </summary>
        /// <param name="name">Custom name for mutex.</param>
        /// <param name="isGlobal">The name is for global mutex?</param>
        /// <returns>A name for mutex.</returns>
        public static string GetMutexName(string name, bool isGlobal = true)
        {
            var prefix = isGlobal ? MUTEX_PREFIX_GLOBAL : MUTEX_PREFIX_LOCAL;
            return string.Format("{0}{1}", prefix, name);
        }

        /// <summary>
        /// Get a name for mutex base on the path.
        /// </summary>
        /// <param name="path">Custom file or directory path.</param>
        /// <param name="isGlobal">The name is for global mutex?</param>
        /// <returns>A name for mutex base on the path.</returns>
        public static string GetMutexNameForPath(string path, bool isGlobal = true)
        {
            var fullPath = Path.GetFullPath(path);
            var pathMd5 = MD5CryptoUtility.ComputeHash(fullPath);
            return GetMutexName(pathMd5, isGlobal);
        }

        /// <summary>
        /// Wait mutex for the name;
        /// This method will block thread until hold mutex or timeout;
        /// You should release the mutex as soon as possible after does not need it.
        /// </summary>
        /// <param name="mutexName">Name of the target mutex.</param>
        /// <param name="millisecondsTimeout">Timeout of wait mutex.</param>
        /// <returns>The target mutex or null if timeout.</returns>
        public static Mutex WaitMutex(string mutexName, int millisecondsTimeout = 1000)
        {
            var interval = 200;
            var timer = 0;
            while (true)
            {
                Mutex mutex = null;
                var isHold = false;
                try
                {
                    mutex = new Mutex(false, mutexName);

                    //If can not hold the mutex, this operate will block this thread interval milliseconds.
                    isHold = mutex.WaitOne(interval);
                    timer += interval;
                }
                catch (AbandonedMutexException) { }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (isHold)
                {
                    return mutex;
                }

                if (timer >= millisecondsTimeout)
                {
                    //Can not hold the mutex after timeout.
                    return null;
                }
            }
        }

        /// <summary>
        /// Wait mutex for the name to do custom work;
        /// This method will block thread until hold mutex or timeout.
        /// </summary>
        /// <param name="mutexName">Name of the target mutex.</param>
        /// <param name="work">Custom work to do.</param>
        /// <param name="millisecondsTimeout">Timeout of wait mutex.</param>
        /// <returns>Do work is succeed?</returns>
        public static bool WaitMutex(string mutexName, Action work, int millisecondsTimeout = 1000)
        {
            var mutex = WaitMutex(mutexName, millisecondsTimeout);
            if (mutex == null)
            {
                return false;
            }

            try
            {
                work.Invoke();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}