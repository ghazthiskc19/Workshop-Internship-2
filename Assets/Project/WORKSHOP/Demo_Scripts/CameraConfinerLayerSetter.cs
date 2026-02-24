using System;
using UnityEngine;
    /// <summary>
    /// DEMO purpose only, will set the confiner layer as we need to keep the layer setting empty for the tutorial
    /// </summary>
    public class CameraConfinerLayerSetter : MonoBehaviour
    {
        private void OnEnable()
        {
            Helpers.RecursiveLayerSet(transform, Helpers.ConfinerLayer);
        }
    }