﻿/*************************************************************************
 *  Copyright © 2015-2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Texture2DExtention.cs
 *  Description  :  Extention for UnityEngine.Texture2D.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  10/24/2015
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Common.Extension
{
    /// <summary>
    /// Extention for UnityEngine.Texture2D.
    /// </summary>
    public static class Texture2DExtention
    {
        /// <summary>
        /// Update the pixels of Texture2D.
        /// </summary>
        /// <param name="texture2D">Base Texture2D.</param>
        /// <param name="colorArray">Color array for pixels.</param>
        /// <param name="mipLevel">The mip level of the texture to write to.</param>
        /// <param name="updateMipmaps">When set to true, mipmap levels are recalculated.</param>
        /// <param name="makeNointerReadable">When set to true, system memory copy of a texture is released.</param>
        public static void UpdatePixels(this Texture2D texture2D, Color[] colorArray,
            int mipLevel = 0, bool updateMipmaps = false, bool makeNointerReadable = false)
        {
            if (colorArray == null || colorArray.Length != texture2D.width * texture2D.height)
            {
                return;
            }

            texture2D.SetPixels(colorArray, mipLevel);
            texture2D.Apply(updateMipmaps, makeNointerReadable);
        }

        /// <summary>
        /// Update the pixels of Texture2D.
        /// </summary>
        /// <param name="texture2D">Base Texture2D.</param>
        /// <param name="colorArray">Color array for pixels.</param>
        /// <param name="mipLevel">The mip level of the texture to write to.</param>
        /// <param name="updateMipmaps">When set to true, mipmap levels are recalculated.</param>
        /// <param name="makeNointerReadable">When set to true, system memory copy of a texture is released.</param>
        public static void UpdatePixels(this Texture2D texture2D, Color32[] colorArray,
            int mipLevel = 0, bool updateMipmaps = false, bool makeNointerReadable = false)
        {
            if (colorArray == null || colorArray.Length != texture2D.width * texture2D.height)
            {
                return;
            }

            texture2D.SetPixels32(colorArray, mipLevel);
            texture2D.Apply(updateMipmaps, makeNointerReadable);
        }
    }
}