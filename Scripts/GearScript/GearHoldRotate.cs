using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearHoldRotate : GearRotateBase {

    public override void TriggerEnter(Collider other)
    {
        base.TriggerEnter(other);
        ChildRotate();
    }

    public override void TriggerExit(Collider other)
    {
        base.TriggerExit(other);
        ChildRehome();
    }

}
