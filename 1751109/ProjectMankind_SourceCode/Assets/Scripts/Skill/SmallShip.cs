using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallShip : Skill
{
    public int amount = 3;
    public GameObject smallShip;
    GameObject presentShip;

    override public void TriggerSkill() {

        if (smallShip == null) { 
            CameraManagement.instance.ChangeBack(gameObject);
            return; 
        }
        
        if (presentShip == null && amount > 0) {
            amount--;
            presentShip = Instantiate(smallShip,transform.position, Quaternion.identity);
            presentShip.GetComponent<Ship>().skillNumber = GetComponent<Ship>().skillNumber;
            presentShip.GetComponent<Ship>().InitSkill();
            CameraManagement.instance.ChangeCamera(presentShip.transform);
        }

        UIManagement.instance.UpdateSkill(skillID,amount,true);

    }

    override public void InitSkillPoint(int id) {
        skillID = id;
        UIManagement.instance.UpdateSkill(skillID,amount,true);
    }


}
