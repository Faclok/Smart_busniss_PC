using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettings : MonoBehaviour
{
    public GameObject settings;
    public RectTransform place;
    public void OpenSettingsPrefab()
    {
        Instantiate(settings, place, false);
    }
}
