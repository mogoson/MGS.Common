/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Dispatcher.cs
 *  Description  :  Dispatcher base main thread.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/5/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace MGS.Common.Threading
{
    /// <summary>
    /// Dispatcher base main thread;
    /// Auto create instance run time.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Dispatcher : MonoBehaviour
    {
        /// <summary>
        /// Queue to cache custom actions.
        /// </summary>
        private static Queue<Action> actions = new Queue<Action>();

        /// <summary>
        /// Locker for thread synchronization.
        /// </summary>
        private static int locker = 0;

        /// <summary>
        /// Initialize dispatcher.
        /// </summary>
#if UNITY_5_3_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#else
        [RuntimeInitializeOnLoadMethod]
#endif
        static void Initialize()
        {
            var instance = new GameObject("Dispatcher").AddComponent<Dispatcher>();
            DontDestroyOnLoad(instance);
        }

        /// <summary>
        /// Update to invoke actions.
        /// </summary>
        private void Update()
        {
            //Requre the locker is free.
            if (Interlocked.Exchange(ref locker, 1) == 0)
            {
                while (actions.Count > 0)
                {
                    try
                    {
                        actions.Dequeue().Invoke();
                    }
                    catch (Exception ex)
                    {
                        Debug.LogErrorFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                    }
                }

                //Release locker.
                Interlocked.Exchange(ref locker, 0);
            }
        }

        /// <summary>
        /// Begin invoke the action by main thread.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public static void BeginInvoke(Action action)
        {
            if (action == null)
            {
                return;
            }

            //Wait the locker is free.
            while (true)
            {
                if (Interlocked.Exchange(ref locker, 1) == 0)
                {
                    actions.Enqueue(action);

                    //Release locker.
                    Interlocked.Exchange(ref locker, 0);
                    break;
                }
            }
        }
    }
}