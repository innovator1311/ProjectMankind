using UnityEngine;
using System.Collections;
using Pathfinding;
public class WanderingDestinationSetter : MonoBehaviour {
   
    public Transform[] points;
    int index = 0;
    IAstarAI ai;
    void Start () {
        ai = GetComponent<IAstarAI>();
    }

    void Update () {
        
        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath)) {
            FindRoutine();
        }
    }

    public void FindRoutine() {
        if (index == points.Length) index = 0;
        ai.destination = points[index++].position;
        ai.SearchPath();
    }




}