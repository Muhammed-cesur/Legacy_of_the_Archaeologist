using System;
using UnityEngine;

namespace MobileWaterShaderEKV
{
    public class DisplayControl : MonoBehaviour
    {

        public Canvas mainCanvas, secondaryCanvas;
        public Camera mainCamera, secondaryCamera;

        // Update is called once per frame
        public void ToggleCam()
        {

            if (!mainCamera.gameObject.activeSelf)
            {
                secondaryCanvas.gameObject.SetActive(false);
                mainCanvas.gameObject.SetActive(true);
                mainCamera.gameObject.SetActive(true);
                secondaryCamera.gameObject.SetActive(false);
            }
            else
            {
                secondaryCanvas.gameObject.SetActive(true);
                mainCanvas.gameObject.SetActive(false);
                mainCamera.gameObject.SetActive(false);
                secondaryCamera.gameObject.SetActive(true);
            }

        }

    }
}
