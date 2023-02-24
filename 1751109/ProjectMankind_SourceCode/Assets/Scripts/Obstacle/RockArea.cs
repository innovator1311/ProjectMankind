using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockArea : MonoBehaviour
{
    public static int number = 0;
    BoxCollider2D BoxCollider2D;
    public GameObject rock;
    public int amount = 10;
    public float minDistance = 0.5f;
    List<Vector3> hadPos = new List<Vector3>();
    private void Awake() {

        number += 1;

        BoxCollider2D = GetComponent<BoxCollider2D>();

        float minX = BoxCollider2D.bounds.min.x;
        float minY = BoxCollider2D.bounds.min.y;
        float maxX = BoxCollider2D.bounds.max.x;
        float maxY = BoxCollider2D.bounds.max.y;
        
        // spawn rock
        for (int i = 0; i < amount; i++) {
            
            Vector3 pos;
            do {
                pos = new Vector3(
                    Random.Range(minX,maxX),
                    Random.Range(minY,maxY),
                    0
                );
            } while (CheckPos(pos));

            hadPos.Add(pos);
            GameObject newRock = Instantiate(rock,pos,Quaternion.identity);
            newRock.transform.parent = transform;
        }

        number -= 1;

    }

    void Start()
    {
        //if (number == 0) {
          //  Debug.Log("Do");
            FindObjectOfType<AstarPath>().GetComponent<AstarPath>().Scan();
        //}
    }

    bool CheckPos(Vector3 newPos) {

        // check if meteor is at least minDistance away from other

        foreach (Vector3 pos in hadPos) {

            if (Vector3.Distance(newPos,pos) < minDistance) 
            return true;
        }

        return false;
    }



}
