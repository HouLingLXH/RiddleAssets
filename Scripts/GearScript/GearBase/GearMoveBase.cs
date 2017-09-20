using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//占领移动机关 ： 进入时触发移动 ，退出时还原移动
public class GearMoveBase : GearBase {

    public Vector3[] v3s_moveFrom;
    public Vector3[] v3s_moveTo;
    public float[] ns_moveTime;


    protected void ChildMove()
    {
        for (int i = 0; i < gos_child.Length; i++)
        {
            AnimSystem.StopAnim(gos_child[i]);
            AnimSystem.Move(gos_child[i], null, v3s_moveTo[i], 0, ns_moveTime[i], interp: InterpType.OutQuad);
        }
    }

    protected void ChildRehome()
    {
        for (int i = 0; i < gos_child.Length; i++)
        {
            AnimSystem.StopAnim(gos_child[i]);
            AnimSystem.Move(gos_child[i], null, v3s_moveFrom[i], 0, ns_moveTime[i], interp: InterpType.InoutQuad);
        }
    }

}
