/*************************************************************************
 *  Copyright © 2015-2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GameObjectExtention.cs
 *  Description  :  Extention for UnityEngine.GameObject.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  09/03/2015
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Common.Extension
{
    /// <summary>
	/// Extention for UnityEngine.GameObject.
	/// </summary>
	public static class GameObjectExtention
    {
        /// <summary>
        /// Set layer include it's children.
        /// </summary>
        public static void BroadcastLayer(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            foreach (Transform trans in gameObject.transform)
            {
                BroadcastLayer(trans.gameObject, layer);
            }
        }
    }
}