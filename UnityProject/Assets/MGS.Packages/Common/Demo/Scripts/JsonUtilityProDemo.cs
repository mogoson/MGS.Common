/*************************************************************************
 *  Copyright (c) #COPYRIGHTYEAR# Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  JsonUtilityProDemo.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  #CREATEDATE#
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Common.Serialization.Demo
{
    public class JsonUtilityProDemo : MonoBehaviour
    {
        void Start()
        {
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
            var testList = new List<string>()
            {
                "A","BB","CCC"
            };

            var json = JsonUtilityPro.ToJson(testList);
            Debug.LogFormat("List to json is {0}", json);

            var list = JsonUtilityPro.FromJson<string>(json);
            Debug.LogFormat("List from json, item count is {0}", list.Count);

            var testDic = new Dictionary<int, string>()
            {
                { 0,"A"},{1,"BB" },{2,"CCC" }
            };

            var dicJson = JsonUtilityPro.ToJson(testDic);
            Debug.LogFormat("Dictionary to json is {0}", dicJson);

            var dictionary = JsonUtilityPro.FromJson<int, string>(dicJson);
            Debug.LogFormat("Dictionary from json, item count is {0}", dictionary.Count);
#else
            Debug.LogError("JsonUtilityPro can be supported in Unity 5.3 or above.");
#endif
        }
    }
}