using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorItem : MonoBehaviour {

    //用于编辑器，通过输入修改逻辑位置
    [SerializeField]
    private int x;
    public int X
    {
        get
        {
            return x;
        }

        set
        {
            x = value;
            ApplyPosFromInput();
            ApplyPositionFromPos();
        }
    }

    //用于编辑器，通过输入修改逻辑位置
    [SerializeField]
    private int z;
    public int Z
    {
        get
        {
            return z;
        }

        set
        {
            z = value;
            ApplyPosFromInput();
            ApplyPositionFromPos();
        }
    }


    //真正的当前地板信息
    public FloorInfo floorInfo;


    #region 静态方法
    //从世界坐标转逻辑坐标
    static Vector3Int GetPosFromPosition(FloorItem l_floorItem)
    {
        Vector3Int pos = GetPosFromPosition(l_floorItem.transform.position);
       
        return pos;
    }

    //从世界坐标转逻辑坐标 （直接传直接坐标）
    static Vector3Int GetPosFromPosition(Vector3 position )
    {
        Vector3Int pos = new Vector3Int();

        pos.x = FlootExpand.Round45(position.x); //忘了转换了 /2
        pos.x = PositionToPos(pos.x);
        pos.z = FlootExpand.Round45(position.z);
        pos.z = PositionToPos(pos.z);
        pos.y = 0;
        //Debug.Log("=GetPosFromXZ==" + pos);
        return pos;

    }

    //从逻辑坐标获取世界坐标
    static Vector3 GetPositionFromPos(FloorInfo l_floorInfo)
    {
        Vector3 position = new Vector3();
        position.x = PosToPosition( l_floorInfo.x);
        position.z = PosToPosition( l_floorInfo.z);
        position.y = 0;

        return position;
    }

    const int c_floorSize = 2;//地板砖的边长
    const int c_floorSize_half = 1; //地板砖的 半 边长


    //逻辑坐标 到 实际坐标转换
    static int PosToPosition(int pos)
    {
        return pos * c_floorSize + c_floorSize_half;
    }

    //实际坐标 到 逻辑坐标转换
    static int PositionToPos(int pos)
    {
        return (pos - c_floorSize_half) / c_floorSize ;
    }


    #endregion

    #region 编辑模式下使用的方法
    //debug floorInfo 信息
    public void DebugFloorInfo()
    {
        Debug.Log("DebugFloorInfo: x=" + floorInfo.x + ",z=" + floorInfo.z);
    }

    //根据 键入内容 修改 逻辑坐标
    public void ApplyPosFromInput()
    {
        floorInfo.SetValue(x,z);
    }

    //根据 所在位置 修改 逻辑坐标
    public void ApplyPosFromPosition()
    {
        Vector3Int pos = GetPosFromPosition(this);
        floorInfo.SetValue(pos);
        x = pos.x;
        z = pos.z;
    }

    //根据 逻辑位置 对齐 世界坐标
    [ContextMenu("根据逻辑位置 对齐世界坐标")]
    public void ApplyPositionFromPos()
    {
        transform.position = GetPositionFromPos(floorInfo);
    }

    //拾取 世界坐标 为 逻辑坐标，并且对齐
    [ContextMenu("拾取当前世界坐标为逻辑坐标，并且对齐")]
    public void ApplyAndAlignFromPosition()
    {
        ApplyPosFromPosition();
        ApplyPositionFromPos();
    }

    [ContextMenu("四方向随机旋转")]
    public void RandomRotate()
    {
        transform.localEulerAngles = new Vector3(0, 1, 0) * Random.Range(0, 4) * 90;
    }


    public List<GameObject> bitchCreats = new List<GameObject>(); //批量创建列表
    //批量创建工具 - 给定起点与终点，平铺当前item
    public void BatchCreat(Vector3Int from, Vector3Int to)
    {
        bitchCreats.Clear();
        for (int x = from.x; x < to.x; x++)
        {
            for (int z = from.z; z < to.z; z++)
            {
                GameObject go = Instantiate(gameObject);
                go.name = gameObject.name + "(" + x + "," + z + ")";
                FloorItem item = go.GetComponent<FloorItem>();
                item.X = x;
                item.Z = z;
                item.ApplyPosFromInput();
                bitchCreats.Add(go);
            }
        }
    }
    //清空批量创建列表
    public void ClearBitchCreat()
    {
        for (int i = 0; i < bitchCreats.Count; i++)
        {
            DestroyImmediate(bitchCreats[i]);
        }
    }




    #endregion

}


//地板信息结构体，便于传递
public struct FloorInfo
{
    public int x; //与3d世界坐标相同， x轴水平向右， z 轴水平向前
    public int z;

    //构造方法
    public FloorInfo(int l_x,int l_z)
    {
        x = l_x;
        z = l_z;
    }

    //修改值方法
    public void SetValue(int l_x, int l_z)
    {
        x = l_x;
        z = l_z;
    }

    //修改值方法
    public void SetValue(Vector3Int v3Pos)
    {
        x = v3Pos.x;
        z = v3Pos.z;
    }


}