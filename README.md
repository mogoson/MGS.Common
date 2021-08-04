[TOC]

﻿# MGS.CommonUtility

## Summary

- Common code for C# and Unity project develop.

## Environment

- Unity 5.0 or above.
- .Net Framework 3.5 or above.

## Platform

- Windows

## Implemented

- MGS.Common.dll  [Detail](./UnityProject/Assets/MGS.Packages/Common/Plugins/MGS.Common.md)
- MGS.UCommon.dll  [Detail](./UnityProject/Assets/MGS.Packages/Common/Plugins/MGS.UCommon.md)

## Module

### JsonUtilityPro

- Environment.

  **Unity 5.3 or above**.

- Implemented

  ```C#
  public class ListAvatar<T>{}
  public class DictionaryAvatar<TKey, TValue> : ISerializationCallbackReceiver{}
  public sealed class JsonUtilityPro{}
  ```
  
- Usage

  ```C#
  //Serialize List.
  var list = new List<string>()
  {
      "A","BB","CCC"
  };
  var json = JsonUtilityPro.ToJson(list);
  //The json is:
  //{"source":["A","BB","CCC"]}
  
  var list = JsonUtilityPro.FromJson<string>(json);
  
  //Serialize Dictionary.
  var dic = new Dictionary<int, string>()
  {
      { 0,"A"},{1,"BB" },{2,"CCC" }
  };
  var json = JsonUtilityPro.ToJson(dic);
  //The json is:
  //json is {"keys":[0,1,2],"values":["A","BB","CCC"]}
  
  var dic = JsonUtilityPro.FromJson<int, string>(json);
  ```

### Dispatcher

- Implemented

  ```C#
  //Dispatcher base main thread; Auto create instance run time.
  public sealed class Dispatcher : MonoBehaviour{}
  ```

- Usage

  ```C#
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
  ```
  
## Demo

- Demos in the path "MGS.Packages/Common/Demo/" provide reference to you.

------

Copyright © 2021 Mogoson.	mogoson@outlook.com