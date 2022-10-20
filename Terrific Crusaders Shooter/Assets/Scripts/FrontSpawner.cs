using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontSpawner : MonoBehaviour, IDamage
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

                Instantiate(enemy, new Vector3(-132.43f, 3, 123.77f), Quaternion.identity);
                Instantiate(enemy1, new Vector3(-144.88f, 3, 123.77f), Quaternion.identity);
                Instantiate(enemy2, new Vector3(-150.91f, 3, 117.15f), Quaternion.identity);
                Instantiate(enemy, new Vector3(-159.14f, 12, 121.43f), Quaternion.identity);

                enemiesSpawned += 4;


                Destroy(wall);
            }
        }
    }
}
