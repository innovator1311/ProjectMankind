using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : CanShootObject
{
    Vector3 pos;
    public float speed = 4f;
    public int Damage;

    private void Awake() {
        pos = transform.position;
    }
    public void Move() {
        
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    private void Update() {
        if (Vector3.Distance(transform.position,pos) > 100) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.tag == "Player") {

             Debug.Log("Damage");
            
            if (other.gameObject.GetComponent<Shield>().isShield) {
                other.gameObject.GetComponent<Shield>().UpdateVirtualHealth();
                return;
            }
            
            other.gameObject.GetComponent<Ship>().UpdateHealth(-Damage);
           
        }
    }

    /* 
    private void OnCollisionStay2D(Collision2D other) {
        
        if (other.gameObject.tag == "Player") {

            if (other.gameObject.GetComponent<Shield>().isShield) {
                other.gameObject.GetComponent<Shield>().UpdateVirtualHealth();
                return;
            }

            if (speed == 6)
                other.gameObject.GetComponent<Ship>().UpdateHealth(-1);
        }

    }*/
}
