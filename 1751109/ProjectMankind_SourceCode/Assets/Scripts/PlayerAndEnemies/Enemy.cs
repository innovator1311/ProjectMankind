using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : CanShootObject
{
    public GameObject bullet;
    public float bulletDistance = 10f;
    public float followTime = 5f;
    public float followSpeed = 4f;
    public float shootTime = 0.5f;
    public WanderingDestinationSetter wds;

    float shoot = 0;
    bool isAttack = false;
    GameObject followObject;
    Vector3 initUp,newDes;

    [HideInInspector]public float time = -1;
    private void Start() {
        initUp = transform.up;
        newDes = Vector3.zero;
    }

    
    private void Update() {  

        if (time > 0) { 
            // start follow
            time -= Time.deltaTime;

            if (time <= 0 || followObject == null) { // check if object had leave or object had die
                ExitTrigger();
                return;
            }

            shoot -= Time.deltaTime;
            ShootBullet(isAttack);
        }

        // rotate to initial in case of static enemy
        if (wds == null) transform.up = Vector3.Lerp(transform.up, newDes, 0.1f);  
    }

    

    public void OnTrigger(Transform playerTransform, bool attack) {

        // there are two kind of trigger: follow without shoot and follow with shoot
        AudioManagement.instance.BattleMusic();
        
        followObject = playerTransform.gameObject;
        newDes = playerTransform.position - transform.position;
       
        // check static enemy
        if (wds != null) {
            GetComponent<AIDestinationSetter>().target = playerTransform;
            GetComponent<AIPath>().maxSpeed = followSpeed;
            wds.enabled = false;
        }
       
        time = followTime;
        isAttack = attack;
    }

    void ExitTrigger() {
        
        // comback to initial state
        AudioManagement.instance.UniverseMusic();
        
        GetComponent<AIDestinationSetter>().target = null;
        GetComponent<AIPath>().maxSpeed = 2;
        Invoke("WanderAgain",2);

        isAttack = false;
        newDes = initUp;
    }

    void ShootBullet(bool isAttack) {

        if (!isAttack) return;
        if (shoot > 0) return;
        shoot = shootTime;

        GameObject newBullet = Instantiate(bullet,transform.position,Quaternion.identity);
        newBullet.transform.up = transform.up;

        // shoot special kind of bullet
        if (wds == null) newBullet.layer = 14;

        newBullet.GetComponent<Bullet>().Move(null,bulletDistance);
    }

    void WanderAgain() { 
        if (wds != null) {
            wds.enabled = true;
            wds.FindRoutine();
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            OnTrigger(other.transform,false);

    }

    void OnDestroy()
    {
        if (AudioManagement.instance != null)
            AudioManagement.instance.UniverseMusic();
    }

}
