/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  EnumeratorUtility.cs
 *  Description  :  Utility for enumerator.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  5/1/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Common.Threading;
using MGS.Common.Utility;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MGS.Common.Collection
{
    /// <summary>
    /// Utility for enumerator.
    /// </summary>
    public sealed class EnumeratorUtility
    {
        /// <summary>
        /// Collect progress states and Finished event.
        /// </summary>
        /// <param name="enumerator">Source enumerator.</param>
        /// <param name="progress">Progress event.</param>
        /// <param name="finished">Finished event.</param>
        /// <returns>IEnumerator.</returns>
        public static IEnumerator Collect(IEnumerator enumerator,
            Action<object> progress = null, Action finished = null)
        {
            while (enumerator.MoveNext())
            {
                if (progress != null)
                {
                    progress.Invoke(enumerator.Current);
                }
                yield return enumerator.Current;
            }
            if (finished != null)
            {
                finished.Invoke();
            }
        }

        /// <summary>
        /// Collect progress states and Finished event.
        /// </summary>
        /// <param name="enumerators">IEnumerable of source enumerator.</param>
        /// <param name="progress">Progress event.</param>
        /// <param name="finished">Finished event.</param>
        /// <returns>IEnumerator.</returns>
        public static IEnumerator Collect(IEnumerable<IEnumerator> enumerators,
            Action<object> progress = null, Action finished = null)
        {
            foreach (var enumerator in enumerators)
            {
                while (enumerator.MoveNext())
                {
                    ActionUtility.Invoke(progress, enumerator.Current);
                    yield return enumerator.Current;
                }
            }
            if (finished != null)
            {
                finished.Invoke();
            }
        }

        /// <summary>
        /// Collect results of the enumerator that run in a background thread.
        /// </summary>
        /// <param name="enumerator">Source enumerator will run in a background thread.</param>
        /// <returns>IEnumerator.</returns>
        public static IEnumerator CollectAsync(IEnumerator enumerator)
        {
            var isDone = false;
            var results = new Queue<object>();
            ThreadUtility.RunAsync(() =>
            {
                while (enumerator.MoveNext())
                {
                    results.Enqueue(enumerator.Current);
                }

                isDone = true;
            });

            while (!isDone || results.Count > 0)
            {
                if (results.Count > 0)
                {
                    yield return results.Dequeue();
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}