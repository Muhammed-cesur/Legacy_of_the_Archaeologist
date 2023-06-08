using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileWaterShaderEKV
{

    [ExecuteInEditMode]
    public class WaterControl_EKV : MonoBehaviour
    {
        [Header("Texture control")]
        [Tooltip("Enable shader textures displacement on the plane")]
        public bool moveTextures;
        [Tooltip("Name of the secondary texture (default: \"_DiffTex\")")]
        public string texturePropertyName;
        [Tooltip("Texture A (_MainTex) displacement on the axis")]
        public float textureA_X, textureA_Y;
        [Tooltip("Texture B (_DiffTex) displacement on the axis")]
        public float textureB_X, textureB_Y;
        [Tooltip("Texture displacement speed (times the axis values)")]
        public float speedMultiplier;

        [Header("Color control")]
        [Tooltip("Enable material-based color")]
        public bool activeColor;
        [Tooltip("Reference material for color")]
        public Material colorMaterial;
        [Tooltip("Color name of the referenced material shader (default: \"_Color\")")]
        public string colorPropertyName;
        [Tooltip("RGBA multipliers")]
        [Range(0f, 2f)]
        public float RMult, GMult, BMult, AMult;



        // Update is called once per frame
        void Update()
        {
            if (moveTextures)
            {
                SetMain();
                SetDiff();
            }


            if (activeColor)
                SetColor();


        }

        public void EnableMovement(bool b)
        {
            moveTextures = b ? true : false;
        }

        public void EnableColor(bool b)
        {
            activeColor = b ? true : false;
        }

        void SetColor()
        {
            if (colorPropertyName == null)
                colorPropertyName = "_Color";

            float r = colorMaterial.GetColor(colorPropertyName).r * RMult;
            float g = colorMaterial.GetColor(colorPropertyName).g * GMult;
            float b = colorMaterial.GetColor(colorPropertyName).b * BMult;
            float a = colorMaterial.GetColor(colorPropertyName).a * AMult;

            GetComponent<Renderer>().sharedMaterial.color = new Color(r, g, b, a);


        }

        void SetMain()
        {
            float offsetX = Time.time * textureA_X * speedMultiplier;
            float offsetY = Time.time * textureA_Y * speedMultiplier;
            GetComponent<Renderer>().sharedMaterial.mainTextureOffset = new Vector2(offsetX, offsetY);
        }
        void SetDiff()
        {
            float offsetX = Time.time * textureB_X * speedMultiplier;
            float offsetY = Time.time * textureB_Y * speedMultiplier;

            string[] mats = GetComponent<Renderer>().sharedMaterial.GetTexturePropertyNames();
            if (texturePropertyName == null)
                texturePropertyName = "_DiffTex";

            foreach (var mat in mats)
            {
                if (mat == texturePropertyName)
                    GetComponent<Renderer>().sharedMaterial.SetTextureOffset(texturePropertyName, new Vector2(offsetX, offsetY));
            }
        }


    }
}
