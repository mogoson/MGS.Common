/*************************************************************************
 *  Copyright (c) 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CompressDemo.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/5/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.UCommon.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Compress
{
    //[AddComponentMenu("")]
    //[RequireComponent(typeof())]
    public class CompressDemo : MonoBehaviour
    {
        #region Field and Property
        //  [Tooltip("")]
        [SerializeField]
        InputField inputField;

        [SerializeField]
        Button button;

        [SerializeField]
        Scrollbar scrollbar;
        #endregion

        #region Private Method
        // Use this for initialization.
        void Start()
        {
            button.onClick.AddListener(OnBtn_Click);
        }

        // Update is called once per frame.
        //void Update()
        //{

        //}

        void OnBtn_Click()
        {
            var zipDir = inputField.text.Trim();
            var zipFile = string.Format("{0}/NewZip.zip", zipDir);

            button.interactable = false;
            scrollbar.size = 0;

            CompressManager.Instance.CompressAsync(new string[] { zipDir }, zipFile, true,
                progress =>
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        scrollbar.size = progress;
                    });
                },
                (isSucceed, info) =>
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        button.interactable = true;
                        if (isSucceed)
                        {
                            Debug.Log(info);
                        }
                        else
                        {
                            Debug.LogError(info);
                        }
                    });
                });
        }
        #endregion

        #region Public Method
        #endregion
    }
}