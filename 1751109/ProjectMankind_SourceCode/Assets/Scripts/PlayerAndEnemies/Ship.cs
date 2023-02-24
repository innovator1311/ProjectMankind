using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public static Ship instance;
    public int presentHealth = 5;
    public float invisibleTime = 1f;
    public float speed;
    [Range(0,6)]
    public int[] skillNumber;
    public AudioClip hurtClip;
    protected Skill[] skillSet;

    [HideInInspector]
    public Rigidbody2D rb2D;
    [HideInInspector]
    public bool isCurrentControl;

    float time = -1;
    float goTime = 1;
    bool run = false;

    Animator animator;
    SpriteRenderer spriteRenderer;
    [HideInInspector]
    public Vector2 pos,des,newpos;

    float push = 0;

    public void Awake() {

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        skillSet = GetComponents<Skill>();
    }

    public void Start()
    {
        // change health and make the main ship the current control
        isCurrentControl = true;
        //pos = 
        newpos = (Vector2)transform.position;;
    }


    // Update is called once per frame
    public void Update()
    {
        if (time >= float.Epsilon) time -= Time.deltaTime;

        if (presentHealth <= 0) {
            CameraManagement.instance.ChangeBack(gameObject);  
        }
        
        if (!isCurrentControl) return;

        if (Input.GetMouseButton(0)) {
            
            run = true;
            // Debug.Log("Work");
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.up = (pos - rb2D.position).normalized;
            

            if ((pos - rb2D.position).magnitude > 1f)
                newpos = rb2D.position + (Vector2) transform.up;

            // des = rb2D.position + (pos - rb2D.position).normalized;
           // goTime = (pos - rb2D.position).magnitude;
           
        }

        if (Input.GetMouseButtonUp(0)) {
            
            run = false;
            //pos = rb2D.position;
        }

        if (Input.GetKeyDown(KeyCode.Q) && skillNumber[0] >= 0) {
            skillSet[skillNumber[0]].TriggerSkill();
        }

        if (Input.GetKeyDown(KeyCode.W) && skillNumber[1] >= 0) {
            skillSet[skillNumber[1]].TriggerSkill();
        }

        if (Input.GetKeyDown(KeyCode.E) && skillNumber[2] >= 0) {
            skillSet[skillNumber[2]].TriggerSkill();
        }
 
    }

    private void FixedUpdate() {

        //if (gameObject.GetComponent<Speed>().isBlackHole) return;
        if (run)
            MoveBody(rb2D.position,newpos,speed * Time.deltaTime);
/* 
        if (push != 0) {

            rb2D.AddForce(-transform.up * speed * push, ForceMode2D.Impulse);
            rb2D.velocity = Vector2.zero;
            push = 0;
        }*/
       //transform.position = Vector3.MoveTowards(transform.position,pos,speed * Time.deltaTime/ goTime);
    
    }

    public void UpdateHealth(int health) {

        if (time >= float.Epsilon && health <= 0) return;

        
        if (health < 0) { 
            AudioManagement.instance.PlayOneShot(hurtClip);
            time = invisibleTime;
        }
        
//        Debug.Log(presentHealth);
        presentHealth += health;
        UIManagement.instance.ChangeHealth(presentHealth);

        
        
    }

    public void MoveBody (Vector2 from, Vector2 to, float time) {

        rb2D.MovePosition (Vector2.Lerp (from, to, time));
    }

    public void InitSkill() {

        for (int i = 0; i < 3; i++) {
            if (skillNumber[i] >= 0)
            skillSet[skillNumber[i]].InitSkillPoint(i);
        }
    }

    public void PushBack(float pushBackForce) {
        
        push = pushBackForce;

        
    }


}
