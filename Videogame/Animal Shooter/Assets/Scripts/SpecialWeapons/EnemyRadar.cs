using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadar : MonoBehaviour
{
    public int timer;
    GameObject[] enemies;

    public bool active = false;

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            MarkEnemies();
            active = false;
        }
    }

    // Start is called before the first frame update
    void MarkEnemies()
    {
        if(gameObject.tag == "RedPlayer")
        {
            enemies = GameObject.FindGameObjectsWithTag("BluePlayer");
        }
        else
        {
            enemies = GameObject.FindGameObjectsWithTag("RedPlayer");
        }

        foreach (GameObject enemy in enemies)
        {
            int layer = gameObject.tag == "RedPlayer" ? 13 : 12; 
            enemy.layer = layer;
        }
        StartCoroutine(EndMark());
    }

    IEnumerator EndMark()
    {
        yield return new WaitForSeconds(timer);
        foreach (GameObject enemy in enemies)
        {
            enemy.layer = 0;
        }
    }

}
