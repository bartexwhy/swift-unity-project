﻿using UnityEngine;
using TMPro;

public class FpsCount : MonoBehaviour
{
    private int frameRate;
    public TextMeshProUGUI frameRateText;

    // Update is called once per frame
    private void Update()
    {
        var current = 0f;
        current = (int)(1f / Time.unscaledDeltaTime);
        frameRate = (int)current;
        frameRateText.text = frameRate.ToString() + "FPS";
    }
}
