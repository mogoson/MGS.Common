/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ListAvatar.cs
 *  Description  :  Avatar for List serialize by JsonUtility.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/17/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;

namespace MGS.Common.Serialization
{
    /// <summary>
    /// Avatar for List serialize by JsonUtility.
    /// </summary>
    /// <typeparam name="T">Type of list item.
    /// The T must with SerializableAttribute and public field or private property with SerializeField Attribute if custom type.
    /// </typeparam>
    internal class ListAvatar<T>
    {
        /// <summary>
        /// Source list.
        /// </summary>
        public List<T> source;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="source">Source list.</param>
        public ListAvatar(List<T> source)
        {
            this.source = source;
        }
    }
}