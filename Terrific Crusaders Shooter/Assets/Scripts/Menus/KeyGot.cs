using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGot : MonoBehaviour
{
    public GameObject key;

    // Update is called once per frame
    void Update()
    {
        CheckForKey();
    }

    void CheckForKey()
    {
        if (!GameManager.instance.playerScript.hasKey)
        {
            key.SetActive(false);
        }
        else if (GameManager.instance.playerScript.hasKey)
        {
            key.SetActive(true);
        }
    }
}
