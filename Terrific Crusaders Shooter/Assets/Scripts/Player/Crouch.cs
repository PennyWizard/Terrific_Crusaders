using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    CapsuleCollider playerCol;
    float originalHeight;
    public float reducedHeight;

    // Start is called before the first frame update
    void Start()
    {
        playerCol = GetComponent<CapsuleCollider>();
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
        playerCol.height = reducedHeight;
    }

    void GetUp()
    {
        playerCol.height = originalHeight;
    }
}
