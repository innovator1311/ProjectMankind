using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : Skill
{
    public float maxSpeed;
    public float collisionSpeed;
    public float burstTime;
    public float pushBackForce = 30f;
    public float scale = 1f;
    public float delayTime = 2f;
    public float maxCollisionSpeed = 6f;
    public Animator engineAnim;

    float time = -1, speed, minSpeed;
    Vector2 direction = Vector2.zero;
    public AudioClip fastClip;
    [HideInInspector]
    public bool isSpeed = false;
    [HideInInspector]
    public bool isBlackHole = false;
 
    private void Start() {
        minSpeed = gameObject.GetComponent<Ship>().speed;
        speed = minSpeed;
    }

    override public void TriggerSkill() {
        
        if (isSpeed || time >= float.Epsilon) return;
        
        isSpeed = true;
        engineAnim.SetBool("Fast", true);  
        AudioManagement.instance.PlayOneShot(fastClip);
    }

    // Update is called once per frame
    void Update()
    {  
        if (time >= float.Epsilon) {
            time -= Time.deltaTime;
            UIManagement.instance.UpdateSkill(skillID,(int)collisionSpeed,true);
            gameObject.GetComponent<Ship>().speed = collisionSpeed;
            return;
        }

        if (speed <= maxSpeed && isSpeed) {
            speed += (maxSpeed - minSpeed)/burstTime * Time.deltaTime;   
        }
        else { 
            isSpeed = false;
            speed = minSpeed;
            engineAnim.SetBool("Fast", false); 
        }

        if (skillID != -1) UIManagement.instance.UpdateSkill(skillID,(int)speed,true);

        if (isBlackHole) gameObject.GetComponent<Ship>().speed = minSpeed;
        else gameObject.GetComponent<Ship>().speed = speed; 
    }

    private void FixedUpdate() {
        GetComponent<Rigidbody2D>().AddForce(-direction * pushBackForce);
    }

    override public void InitSkillPoint(int id) {
        skillID = id;
        UIManagement.instance.UpdateSkill(skillID,(int)speed,true);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (isBlackHole) return;

        if (direction == Vector2.zero) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (!isSpeed) return;
        
        isSpeed = false;
        AudioManagement.instance.StopOneShoot(fastClip);

        if (speed <= maxCollisionSpeed) return;
        time = delayTime;

        /* if (gameObject.GetComponent<Shield>().isShield) 
            gameObject.GetComponent<Shield>().UpdateVirtualHealth();
        else 
            gameObject.GetComponent<Ship>().UpdateHealth(-1);*/
        
        direction = (other.contacts[0].point - GetComponent<Rigidbody2D>().position); 
    }

    private void OnCollisionExit2D(Collision2D other) {
        direction = Vector2.zero;
        GetComponent<Ship>().pos = GetComponent<Rigidbody2D>().position;
    }

}
