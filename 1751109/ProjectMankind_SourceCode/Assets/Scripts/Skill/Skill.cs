using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected int skillID;

    private void Awake() {
        skillID = -1;
    }

    virtual public void TriggerSkill() {}

    virtual public void InitSkillPoint(int id) {}

    public void escapeState() {

        
    }


}
