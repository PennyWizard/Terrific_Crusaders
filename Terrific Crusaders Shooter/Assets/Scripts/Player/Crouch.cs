using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    CapsuleCollider playerCol;
    CharacterController charactorCon;
    float originalHeight;
    public float reducedHeight;

    // Start is called before the first frame update
    void Start()
    {
        playerCol = GetComponent<CapsuleCollider>();
        charactorCon = GetComponent<CharacterController>();
        originalHeight = playerCol.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            GetDown();
        }
        else if(Input.GetButtonUp("Crouch"))
        {
            GetUp();
        }
    }

    void GetDown()
    {
        GameManager.instance.playerScript.isCruched = true;

        playerCol.height = reducedHeight;
        charactorCon.height = reducedHeight;
    }

    void GetUp()
    {
        GameManager.instance.playerScript.isCruched = false;

        playerCol.height = originalHeight;
        charactorCon.height = originalHeight;
    }
}
