using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFHC_Shader_Samples
{

	public class highlightAnimated : MonoBehaviour
    {

        private Material mat;

        void Start()
        {
            mat = GetComponent<Renderer>().material;
            switchhighlighted(true);
        }

        void OnTriggerStay(Collider other)
        {
            if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                switchhighlighted(false);
            }
        }

        void switchhighlighted(bool highlighted)
        {
            mat.SetFloat("_Highlighted", (highlighted ? 1.0f : 0.0f));
        }

    }

}