﻿/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Reflector.cs
 *  Description  :  Reflector for reflection.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/30/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Reflection;

namespace MGS.Common.Reflection
{
    /// <summary>
    /// Reflector for reflection.
    /// </summary>
    public sealed class Reflector
    {
        /// <summary>
        /// Sets the value of the field supported by the given object.
        /// </summary>
        /// <param name="obj">The object whose field value will be set.</param>
        /// <param name="name">The string containing the name of the data field to get.</param>
        /// <param name="value">The value to assign to the field.</param>
        /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
        public static void SetField(object obj, string name, object value,
            BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic)
        {
            var fieldInfo = obj.GetType().GetField(name, bindingAttr);
            if (fieldInfo == null)
            {
                return;
            }

            fieldInfo.SetValue(obj, value);
        }

        /// <summary>
        /// Sets the value of the property supported by the given object.
        /// </summary>
        /// <param name="obj">The object whose property value will be set.</param>
        /// <param name="name">The string containing the name of the property to get.</param>
        /// <param name="value">The value to assign to the property.</param>
        /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
        /// <param name="index">Optional index values for indexed properties.</param>
        public static void SetProperty(object obj, string name, object value,
            BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic, object[] index = null)
        {
            var propertyInfo = obj.GetType().GetProperty(name, bindingAttr);
            if (propertyInfo == null)
            {
                return;
            }

            propertyInfo.SetValue(obj, value, index);
        }

        /// <summary>
        /// Invokes the method or constructor represented by the given object, using the specified parameters.
        /// </summary>
        /// <param name="obj">The object on which to invoke the method or constructor.</param>
        /// <param name="name">The string containing the name of the method to get.</param>
        /// <param name="parameters">An argument list for the invoked method or constructor.</param>
        /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
        /// <returns></returns>
        public static object InvokeMethod(object obj, string name,
            BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic, object[] parameters = null)
        {
            var methodInfo = obj.GetType().GetMethod(name, bindingAttr);
            if (methodInfo == null)
            {
                return null;
            }

            return methodInfo.Invoke(obj, parameters);
        }
    }
}