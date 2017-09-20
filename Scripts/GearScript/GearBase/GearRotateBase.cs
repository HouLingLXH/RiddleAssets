using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotateBase : GearBase {

    public Vector3[] v3s_rotateFrom;
    public Vector3[] v3s_rotateTo;
    public float[] ns_rotateTime;


    protected void ChildRotate()
    {
        for (int i = 0; i < gos_child.Length; i++)
        {
            AnimSystem.StopAnim(gos_child[i]);
            AnimSystem.Rotate(gos_child[i], gos_child[i].transform.eulerAngles, v3s_rotateTo[i],  ns_rotateTime[i], 0, interp: InterpType.OutQuad);
        }
    }

    protected void ChildRehome()
    {
        for (int i = 0; i < gos_child.Length; i++)
        {
            AnimSystem.StopAnim(gos_child[i]);
            AnimSystem.Rotate(gos_child[i], null, v3s_rotateFrom[i],  ns_rotateTime[i], 0, interp: InterpType.InoutQuad);
        }
    }

}
