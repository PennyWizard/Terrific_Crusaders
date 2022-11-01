using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGot : MonoBehaviour
{
    public GameObject key;
    public GameObject key2;

    // Update is called once per frame
    void Update()
    {
        CheckForKey();
        CheckForKey2();
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

    void CheckForKey2()
    {
        if (!GameManager.instance.playerScript.hasKey2)
        {
            key2.SetActive(false);
        }
        else if (GameManager.instance.playerScript.hasKey2)
        {
            key2.SetActive(true);
        }
    }
}
