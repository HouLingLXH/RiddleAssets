using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSwitchRotate : GearRotateBase {

    bool b_switch = false;

    public bool b_canOff = true;// 是否可以关闭

    public override void TriggerEnter(Collider other)
    {
        base.TriggerEnter(other);
        if (b_switch == false)
        {
            ChildRotate();
            b_switch = true;
        }
        else
        {
            if (b_canOff)
            {
                ChildRehome();
                b_switch = false;
            }
        }
    }

    

}
