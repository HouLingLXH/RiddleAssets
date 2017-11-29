using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSwitchChangeMaterial : GearChangeMaterialBase
{
    //是否可以回到之前状态
    public bool b_canOff;

    //当前开关状态
    bool b_switch = false;
   

    public override void TriggerEnter(Collider other)
    {
        base.TriggerEnter(other);

        if (b_switch == false)
        {
            b_switch = true;
            ChangeTo();
            SetCollider(true);
        }
        else
        {
            if (b_canOff)
            {
                b_switch = false;
                ReHome();
                SetCollider(false);
            }
        }
    }
}
