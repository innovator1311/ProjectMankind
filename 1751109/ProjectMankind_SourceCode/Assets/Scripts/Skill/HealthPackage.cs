using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackage : MonoBehaviour
{
    public int health;
    private void OnTriggerStay2D(Collider2D other) {
        
        if (other.gameObject.tag == "Player") {

            other.gameObject.GetComponent<Ship>().UpdateHealth(health);
            Destroy(gameObject);
        }


    }
}
