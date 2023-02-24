using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLight : MonoBehaviour
{
    GameObject Enemy;
    public LayerMask layer_mask;
    private void Awake() {
        Enemy = transform.parent.gameObject;
    }
    private void OnTriggerStay2D(Collider2D other) {

        if (other.tag == "Player" && gameObject.tag != "PlayerLight") {

            if (other.gameObject.GetComponent<Invisible>().hide && !other.gameObject.GetComponent<Shield>().isShield) return;
                //Enemy.GetComponent<Enemy>().OnTrigger(other.transform,false);    

            /* RaycastHit2D hit = Physics2D.Linecast(Enemy.GetComponent<Rigidbody2D>().position,
                other.gameObject.GetComponent<Rigidbody2D>().position, layer_mask);

            if (hit.collider != null) {
                if (hit.collider.gameObject.GetComponent<Invisible>() != null && hit.collider.gameObject.GetComponent<Invisible>().hide)
                    // if ship is hide
                    Enemy.GetComponent<Enemy>().OnTrigger(other.transform,false);
                else
                    Enemy.GetComponent<Enemy>().OnTrigger(other.transform,true);
            }
            else Enemy.GetComponent<Enemy>().OnTrigger(other.transform,false);*/

            if (other.gameObject.GetComponent<Invisible>().hide)
                Enemy.GetComponent<Enemy>().OnTrigger(other.transform,false);                
            else 
                Enemy.GetComponent<Enemy>().OnTrigger(other.transform,true); 
        }

        if (gameObject.tag == "PlayerLight" && other.gameObject.tag == "Enemy") {
            // trigger enemy
            GameObject player = gameObject.transform.parent.transform.gameObject;
            other.gameObject.GetComponent<Enemy>().OnTrigger(gameObject.transform.parent.transform,false);
        }

    }

}
