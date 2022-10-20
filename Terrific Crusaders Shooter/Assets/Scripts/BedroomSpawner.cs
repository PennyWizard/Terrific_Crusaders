using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomSpawner : MonoBehaviour, IDamage
{
    [SerializeField] bool penetrable;
    [SerializeField] GameObject wall;

    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
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

                Instantiate(enemy, new Vector3(-161.28f, 3, 120.93f), Quaternion.identity);
                Instantiate(enemy1, new Vector3(-155.04f, 3, 102.92f), Quaternion.identity);
                Instantiate(enemy2, new Vector3(-164.42f, 3, 126.29f), Quaternion.identity);
                Instantiate(enemy2, new Vector3(-157.31f, 3, 124.41f), Quaternion.identity);

                enemiesSpawned += 4;


                Destroy(wall);
            }
        }
    }
}
