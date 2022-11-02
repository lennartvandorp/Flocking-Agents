using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AverageFrameCounter : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (text == null)
        {
            Debug.LogError("There is no textMeshPro component on this object");
            this.enabled = false;
        }
    }

    int framesPassed = 0;
    private void Update()
    {
        framesPassed++; 

        text.text = (1f / (Time.timeSinceLevelLoad / (float)framesPassed)).ToString();
    }
}
