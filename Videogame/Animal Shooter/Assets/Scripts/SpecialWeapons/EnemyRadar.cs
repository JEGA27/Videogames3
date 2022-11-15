using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadar : MonoBehaviour
{
    public GameObject marker;
    public int y;
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
            GameObject temp = Instantiate(marker, new Vector3(enemy.transform.position.x, enemy.transform.position.y + y, enemy.transform.position.z) , Quaternion.identity);
            temp.transform.LookAt(this.transform);
        }
        StartCoroutine(DestroyMarkers());
    }

    IEnumerator DestroyMarkers()
    {
        yield return new WaitForSeconds(timer);
        GameObject[] markers = GameObject.FindGameObjectsWithTag("Marker");
        foreach (GameObject marker in markers)
        {
            Destroy(marker);
        }
    }

}
