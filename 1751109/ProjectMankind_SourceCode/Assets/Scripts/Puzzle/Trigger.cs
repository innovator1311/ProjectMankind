using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Exit exit;
    public AudioClip smallTrigger;

    private void Awake() {
        exit.numberOfTrigger++;
    }


    private void OnTriggerEnter2D(Collider2D other) {

       if (other.tag == "Player" || other.tag == "Bullet") {

            AudioManagement.instance.PlayOneShot(smallTrigger);
            exit.SolveTrigger();
            Destroy(gameObject);
       }

       if (other.tag == "Bullet") Destroy(other.gameObject);
    }




}
