using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageBox : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");

        if (other.CompareTag("Hostage"))
        {
            Debug.Log("bye");
            Destroy(other.gameObject);
        }
    }

}
