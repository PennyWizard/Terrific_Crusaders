using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyKey : MonoBehaviour
{
    public Target target;

    private void Start()
    {
        if (!target.hasKey)
        {
            Destroy(gameObject);
        }
    }
}
