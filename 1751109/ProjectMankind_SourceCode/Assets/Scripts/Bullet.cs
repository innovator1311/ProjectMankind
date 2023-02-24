using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Sprite[] bulletSprite;
    public Sprite[] destroyBulletSprite;
    public float speed = 3f;
    public int Damage = 1;

    Vector3 startPos;
    float distance;

    [HideInInspector]
    public Transform playerTransform;
    // Start is called before the first frame update
    private void Awake() {
        
        GetComponent<SpriteRenderer>().sprite = bulletSprite[0];
        Debug.Log(GetComponent<SpriteRenderer>().sprite);
    }

    void Start() {
        startPos = transform.position;
    }

    private void Update() {

        if (Vector3.Distance(transform.position,startPos) > distance) 
            Destroy(gameObject);

        Debug.Log(GetComponent<SpriteRenderer>().sprite);

    }

    public void Move(Transform pT, float dis) {

        distance = dis;
        playerTransform = pT;
        
        SetBullet();
        Debug.Log(GetComponent<SpriteRenderer>().sprite);
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        Debug.Log("Happen0");

        if (other.tag == "Player" && playerTransform == null) {
//          
            Debug.Log("Happen2");
            if (other.gameObject.GetComponent<Shield>().isShield)
                other.gameObject.GetComponent<Shield>().UpdateVirtualHealth();
            else
                other.gameObject.GetComponent<Ship>().UpdateHealth(-Damage);
            DestroyBullet();
        }

        if (other.tag == "Enemy" && playerTransform != null) {
//           
            Debug.Log("Happen3");
            other.gameObject.GetComponent<Enemy>().OnTrigger(playerTransform,true);
            DestroyBullet();
        } 

        if (other.gameObject.GetComponent<CanShootObject>() != null 
            && (playerTransform != null || other.tag != "Enemy")) {
                Debug.Log("happen4");
            other.gameObject.GetComponent<CanShootObject>().ChangeHealth(-Damage);
            DestroyBullet();
        }

        if (other.gameObject.layer == 10) {

            Debug.Log("happen5");
            DestroyBullet();
        }
    
    }

    void SetBullet() {

        

        if (playerTransform != null) {
            Debug.Log("Call1");
            GetComponent<SpriteRenderer>().sprite = bulletSprite[0];
        }
        else {
            Debug.Log("Call2");
            GetComponent<SpriteRenderer>().sprite = bulletSprite[1];
        
        }

    }

    void DestroyBullet() {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
        if (playerTransform != null) {
            Debug.Log("Call1");
            GetComponent<SpriteRenderer>().sprite = destroyBulletSprite[0];
        }
        else {
            Debug.Log("Call2");
            GetComponent<SpriteRenderer>().sprite = destroyBulletSprite[1];
        }

        Invoke("CompletedDestroy",0.1f);
    }

    void CompletedDestroy() {
        Destroy(gameObject);
    }

    

}
