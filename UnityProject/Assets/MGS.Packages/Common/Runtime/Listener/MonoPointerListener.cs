﻿/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoPointerListener.cs
 *  Description  :  Event Listener for pointer.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/14/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Common.Utility;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MGS.Common.Listener
{
    /// <summary>
    /// Event Listener for pointer.
    /// </summary>
    public class MonoPointerListener : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        #region
        /// <summary>
        /// On pointer enter event.
        /// </summary>
        public event Action<PointerEventData> OnPointerEnterEvent;

        /// <summary>
        /// On pointer exit event.
        /// </summary>
        public event Action<PointerEventData> OnPointerExitEvent;

        /// <summary>
        /// On pointer down event.
        /// </summary>
        public event Action<PointerEventData> OnPointerDownEvent;

        /// <summary>
        /// On pointer up event.
        /// </summary>
        public event Action<PointerEventData> OnPointerUpEvent;

        /// <summary>
        /// On pointer click event.
        /// </summary>
        public event Action<PointerEventData> OnPointerClickEvent;
        #endregion

        #region
        /// <summary>
        /// On pointer enter.
        /// </summary>
        /// <param name="eventData">Pointer event data.</param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            ActionUtility.Invoke(OnPointerEnterEvent, eventData);
        }

        /// <summary>
        /// On pointer exit.
        /// </summary>
        /// <param name="eventData">Pointer event data.</param>
        public void OnPointerExit(PointerEventData eventData)
        {
            ActionUtility.Invoke(OnPointerExitEvent, eventData);
        }

        /// <summary>
        /// On pointer down.
        /// </summary>
        /// <param name="eventData">Pointer event data.</param>
        public void OnPointerDown(PointerEventData eventData)
        {
            ActionUtility.Invoke(OnPointerDownEvent, eventData);
        }

        /// <summary>
        /// On pointer up.
        /// </summary>
        /// <param name="eventData">Pointer event data.</param>
        public void OnPointerUp(PointerEventData eventData)
        {
            ActionUtility.Invoke(OnPointerUpEvent, eventData);
        }

        /// <summary>
        /// On pointer click.
        /// </summary>
        /// <param name="eventData">Pointer event data.</param>
        public void OnPointerClick(PointerEventData eventData)
        {
            ActionUtility.Invoke(OnPointerClickEvent, eventData);
        }
        #endregion
    }
}