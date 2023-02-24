using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    public float rotateSpeed = 10f;
    public float escapeForce = 10f;
    public float blackholeForce = 100f;
    
    List<GameObject> attractObjs = new List<GameObject>();
    public CircleCollider2D triggerOne;
    float magnitude = 0;
    Vector2 firstDirection = Vector2.zero;
    // Update is called once per frame
    private void Awake() {
        magnitude = triggerOne.radius * transform.localScale.x;
    }

    void Update()
    {
        transform.Rotate (0,0,rotateSpeed*Time.deltaTime); 
        
    }

    private void FixedUpdate() {

        //Debug.Log(attractObjs.Count);

        foreach (GameObject attractObj in attractObjs) {       

            if (attractObj.GetComponent<Speed>() != null) {
                attractObj.GetComponent<Speed>().isBlackHole = true;
                if (attractObj.GetComponent<Speed>().isSpeed) {
                    continue;
                }
            }
    
            Vector2 direction = (Vector2)(transform.position - attractObj.transform.position);
          //  if (magnitude == 0) magnitude = direction.magnitude;
            firstDirection = magnitude * direction.normalized;
            Vector2 force = (firstDirection - direction) * blackholeForce;

          //  Debug.Log(force.magnitude);

            //if 
               

/*             if (Input.GetMouseButton(0) && attractObj.GetComponent<Speed>().isBlackHole) {

                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 newForce = (Vector2)(pos - transform.position) * escapeForce;

                Debug.Log(force + newForce);
                attractObj.GetComponent<Rigidbody2D>().AddForce(-direction * blackholeForce);
            }*/

            if (firstDirection.magnitude > direction.magnitude) {
               // Debug.Log("Addforce");
                attractObj.GetComponent<Rigidbody2D>().AddForce(force);
            }

            if (Input.GetMouseButton(0)) {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 newForce = ((Vector2)(pos - attractObj.transform.position)).normalized * direction.magnitude * escapeForce;

               // Debug.Log((((Vector2)(pos - attractObj.transform.position)).normalized).magnitude);
                attractObj.GetComponent<Rigidbody2D>().AddForce(newForce);
            }

           // else attractObj.GetComponent<Rigidbody2D>().AddForce(force);

        }
        

    }

    private void OnTriggerStay2D(Collider2D other) {

        if (attractObjs.Contains(other.gameObject)) return;

        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") {
            attractObjs.Add(other.gameObject);
        }
        

        //else attractObj = null;

    }

    private void OnTriggerExit2D(Collider2D other) {
        //attractObj = null;
        attractObjs.Remove(other.gameObject);
        if (other.tag == "Player") {
             other.gameObject.GetComponent<Speed>().isBlackHole = false;
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {

        if (other.gameObject.GetComponent<CanShootObject>() != null) 
            other.gameObject.GetComponent<CanShootObject>().ChangeHealth(-1);

        if (other.gameObject.tag == "Player") {

            if (other.gameObject.GetComponent<Shield>().isShield) {
                other.gameObject.GetComponent<Shield>().UpdateVirtualHealth();
                return;
            }

           other.gameObject.GetComponent<Ship>().UpdateHealth(-1);
        }

       
    }


}
