using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotMovement : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public float timerForNewPath;
    bool inCoRoutine;
    Vector3 target;
    NavMeshPath path;
    bool validPath;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
      navMeshAgent = GetComponent<NavMeshAgent>();
      path = new NavMeshPath();
      speed = Random.Range(1, 4);
      navMeshAgent.speed = speed;

    }

    // Update is called once per frame
    void Update()
    {
        if(!inCoRoutine)
        {
          StartCoroutine(DoSomething());
        }

    }

    Vector3 getNewRandomPosition ()
    {
      float x = Random.Range(-40, 40);
      float z = Random.Range(-40, 40);

      Vector3 pos = new Vector3(x, 0, z);
      return pos;
    }


    IEnumerator DoSomething ()
    {
      inCoRoutine = true;
      yield return new WaitForSeconds(timerForNewPath);
      GetNewPath();
      validPath = navMeshAgent.CalculatePath(target, path);
      if(!validPath) Debug.Log("Found an invalid path");
      while(!validPath)
      {
        yield return new WaitForSeconds(0.01f);
        GetNewPath();
        validPath = navMeshAgent.CalculatePath(target, path);
      }
      inCoRoutine = false;
    }

    void GetNewPath()
    {
      target = getNewRandomPosition();
      navMeshAgent.SetDestination(target);
    }


}
