using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//占领移动机关 ： 进入时触发移动 ，退出时还原移动
public class GearMoveBase : GearBase {

    public Vector3[] v3s_moveFrom;
    public Vector3[] v3s_moveTo;
    public float[] ns_moveTime;

}
