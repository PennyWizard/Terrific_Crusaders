using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walls : MonoBehaviour, IDamage
{
    [SerializeField] bool penetrable;
    [SerializeField] GameObject wall;
    int HP = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(int DMG)
    {
        if (penetrable)
        {
            HP -= DMG;
            if (HP <= 0)
            {
                Destroy(wall);
            }
        }
    }
}
