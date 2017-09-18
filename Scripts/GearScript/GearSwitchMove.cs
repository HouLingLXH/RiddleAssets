using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSwitchMove : GearMoveBase {

    bool b_switch = false;

    public override void TriggerEnter(Collider other)
    {
        base.TriggerEnter(other);

        if (b_switch)
        {
            SwitchOn();
        }
        else
        {
            SwitchOff();
        }

    }

    private void SwitchOn()
    {
        b_switch = true;
    }

    private void SwitchOff()
    {
        b_switch = false;
    }
}
