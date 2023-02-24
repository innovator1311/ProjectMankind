using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Skill
{   
    public AudioClip shootClip;
    public GameObject bullet;
    public int amount = 10;
    public float bulletDistance = 10;

    // Update is called once per frame
    override public void TriggerSkill() {

        if (amount == 0) return;

        AudioManagement.instance.PlayOneShot(shootClip);
        
        GameObject newBullet = Instantiate(bullet,transform.position,Quaternion.identity);
        newBullet.transform.up = transform.up;
        newBullet.GetComponent<Bullet>().Move(transform,bulletDistance);
        amount--;

        UIManagement.instance.UpdateSkill(skillID,amount,true);
    }

    override public void InitSkillPoint(int id) {
        skillID = id;
        UIManagement.instance.UpdateSkill(skillID,amount,true);
    }

   
    public void UpdateAmount(int upAmount) {
        amount += upAmount;
        if (skillID != -1) UIManagement.instance.UpdateSkill(skillID,amount,true);
    }

}
