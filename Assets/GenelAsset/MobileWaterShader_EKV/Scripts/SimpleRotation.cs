using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace MobileWaterShaderEKV
{

    [ExecuteInEditMode]
    public class SimpleRotation : MonoBehaviour
    {
        public float rotationsPerMinute;
        public float xFactor, yFactor, zFactor;


        // Update is called once per frame
        void Update()
        {
            transform.Rotate(xFactor * rotationsPerMinute * (Time.deltaTime),
                             yFactor * rotationsPerMinute * (Time.deltaTime),
                             zFactor * rotationsPerMinute * (Time.deltaTime));


        }

    }
}
