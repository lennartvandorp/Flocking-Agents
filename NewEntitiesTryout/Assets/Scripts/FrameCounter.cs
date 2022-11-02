using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class FrameCounter : MonoBehaviour
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
    // Update is called once per frame
    void Update()
    {
        text.text = (1f / Time.deltaTime).ToString();
    }
}
