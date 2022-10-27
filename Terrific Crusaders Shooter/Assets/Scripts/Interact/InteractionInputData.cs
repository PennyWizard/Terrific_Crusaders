using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractionInputData", menuName = "InterationSystem/InputData")]

public class InteractionInputData : ScriptableObject
{
    private bool m_interactedClicked;

    private bool m_interactedReleased;

    public bool InteractedClicked { get => m_interactedClicked; set => m_interactedClicked = value; }
    public bool Interactedreleased { get => m_interactedClicked; set => m_interactedClicked = value; }

    public void Reset()
    {
        m_interactedClicked = false;
        m_interactedReleased = false;
    }
}
