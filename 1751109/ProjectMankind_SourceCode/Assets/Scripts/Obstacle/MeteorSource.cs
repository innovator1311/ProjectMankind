using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSource : MonoBehaviour
{
    public GameObject smallMeteor;
    public GameObject bigMeteor;
    public float timeInterval = 2f;
    public float range = 3f;
    public int chaneSmall;
    public int chaneBig;
    float time = -1;

    private void Update() {
        
        time -= Time.deltaTime;
        
        if (time >= Mathf.Epsilon) return;
        time = timeInterval;
        
        float rd = Random.Range(-range,range);
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y + rd, transform.position.z);

        int type = Random.Range(0,chaneBig + chaneSmall);
        
        GameObject newMeteor = null;
        if (type < chaneSmall) newMeteor = Instantiate(smallMeteor,newPos,Quaternion.identity);
        else newMeteor = Instantiate(bigMeteor,newPos,Quaternion.identity);

        newMeteor.transform.up = transform.up;
        newMeteor.GetComponent<Meteor>().Move();

    }


}
