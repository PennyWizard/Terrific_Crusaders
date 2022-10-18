using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostage : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.hostageAmount++;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.hostageCurrent++;
            Destroy(gameObject);
        }
            
    }
}
