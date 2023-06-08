using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MobileWaterShaderEKV
{
    public class SetValueOnStart : MonoBehaviour
    {
        public MobileWaterShader_EKV waterScript;
        public WaterControl_EKV waterControl;
        public Slider shininessSlider, brightnessSlider, attenuationSlider;
        public Toggle lightingToggle, texturesToggle, transparencyToggle, wavesToggle, reflectionToggle, movementToggle;

        private void Start()
        {
            shininessSlider.value = waterScript.GetShininess();
            brightnessSlider.value = waterScript.GetBrightness();
            attenuationSlider.value = waterScript.GetAttenuation();

            lightingToggle.isOn = waterScript.IsLightingEnabled();
            texturesToggle.isOn = waterScript.IsTexturesEnabled();
            transparencyToggle.isOn = waterScript.IsTransparencyEnabled();
            wavesToggle.isOn = waterScript.IsWavesEnabled();
            reflectionToggle.isOn = waterScript.IsReflectionEnabled();
            movementToggle.isOn = waterControl.moveTextures;
        }
    }
}
