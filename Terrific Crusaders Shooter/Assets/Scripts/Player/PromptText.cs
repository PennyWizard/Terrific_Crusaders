using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PromptText : MonoBehaviour
{
    public TextMeshProUGUI promptText;

    public void TextUpdate(string toolTip)
    {
        promptText.SetText(toolTip);
    }

    public void ResetUI()
    {
        promptText.SetText("");
    }
}
