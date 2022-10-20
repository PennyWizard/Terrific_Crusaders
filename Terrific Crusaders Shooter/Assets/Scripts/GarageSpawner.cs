using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageSpawner : MonoBehaviour, IDamage
{
    [SerializeField] bool penetrable;
    [SerializeField] GameObject wall;

    [SerializeField] GameObject enemy;
    [SerializeField] int maxEnemies;

    int HP = 1;
    int enemiesSpawned;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.enemyAmount += maxEnemies;
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

                Instantiate(enemy, new Vector3(-163.93f, 3, 108.78f), Quaternion.identity);
                Instantiate(enemy, new Vector3(-163.93f, 3, 98.59f), Quaternion.identity);
                Instantiate(enemy, new Vector3(-156.93f, 3, 98.59f), Quaternion.identity);
                Instantiate(enemy, new Vector3(-156.93f, 3, 104.77f), Quaternion.identity);

                enemiesSpawned += 4;


                Destroy(wall);
            }
        }
    }
}
