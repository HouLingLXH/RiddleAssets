using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSwitchMove : GearMoveBase {

    bool b_switch = false;

    public bool canOff = true; //是否可以关闭

    public override void TriggerEnter(Collider other)
    {
        base.TriggerEnter(other);

        if (b_switch)
        {
            SwitchOn();
        }
        else
        {
            if (canOff)
            {
                SwitchOff();
            } 
        }
    }

    //开
    private void SwitchOn()
    {
        b_switch = true;
        ChildMove();

    }

    //关
    private void SwitchOff()
    {
        b_switch = false;
        ChildRehome();
    }
}
