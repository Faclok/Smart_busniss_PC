using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.View.Body.Setting
{
    public class Settings : MonoBehaviour
    {
        public Toggle toggle;
        public TMP_InputField widthField;
        public TMP_InputField heightField;

        private void Awake()
        {
            widthField.text = Screen.width.ToString();
            heightField.text = Screen.height.ToString();
            if(Screen.fullScreen)
            {
                toggle.isOn = true;
            }
            else
            {
                toggle.isOn = false;
            }
        }
        public void ToggleOnChange()
        {
            if (toggle.isOn)
            {
                Screen.SetResolution(1920,1080,true);
            }
            else
            {
                Screen.fullScreen = false;
            }
        }
        public void ChangeResolution()
        {
            Screen.fullScreen = false;
            Screen.SetResolution(Convert.ToInt32(widthField.text), Convert.ToInt32(heightField.text), false);

            widthField.text = "";
            heightField.text = "";
            toggle.isOn = false;
        }
    }
}