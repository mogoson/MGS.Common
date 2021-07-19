/*************************************************************************
 *  Copyright (c) #COPYRIGHTYEAR# Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DispatcherDemo.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  #CREATEDATE#
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Common.Threading.Demo
{
    public class DispatcherDemo : MonoBehaviour
    {
        [SerializeField]
        Image image;

        bool isLoop = true;

        void Start()
        {
            var thread = new Thread(() =>
            {
                while (isLoop)
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        //Run code on main thread.
                        var r = Random.Range(0, 1.0f);
                        var g = Random.Range(0, 1.0f);
                        var b = Random.Range(0, 1.0f);
                        image.color = new Color(r, g, b);
                    });
                    Thread.Sleep(1000);
                }
            })
            { IsBackground = true };
            thread.Start();
        }

        void OnDestroy()
        {
            isLoop = false;
        }
    }
}