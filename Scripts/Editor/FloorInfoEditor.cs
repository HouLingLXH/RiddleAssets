using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FloorItem))]
public class FloorInfoEditor :Editor{

    //地板展示信息的位置偏移 
    readonly Vector3 floorInfoShowOffset = new Vector3(0,1,0);
    //地板信息展示 开关
    public static bool b_showItemInfo = true;
    //场景中所有地板item
    public static List<FloorItem> allFloorInfo = new List<FloorItem>();
    //当前选中的 地板格
    public FloorItem floorInfoItem;

    private void OnSceneGUI()
    {
        floorInfoItem = (FloorItem)target;
        SceneGUI();
        ShowAllFloorItemInfo();

    }
    #region OnSceneGUI

    //scene 窗口上的GUI 方法
    private void SceneGUI()
    {
        Handles.BeginGUI();

        ShowAllFloorInfo();
        AdjustFloorPos();
        AdjustFloorPosWSAD();

        Handles.EndGUI();
    }

    //展示所有地板格
    private void ShowAllFloorInfo()
    {
        AllFloorInfoEditor.b_showOthersInfo = GUILayout.Toggle(AllFloorInfoEditor.b_showOthersInfo, "显示地板以外物体的信息", GUILayout.Width(150));
        b_showItemInfo = GUILayout.Toggle(b_showItemInfo, "显示地板格信息", GUILayout.Width(150));

        if (GUILayout.Button("寻找所有地板格 floorItem", GUILayout.Width(150)))
        {
            FindAllFloorItem();
        }
    }

    // 通过上下左右按键，按格数调整坐标
    private void AdjustFloorPos()
    {
        GUILayoutOption[] style = new GUILayoutOption[] { GUILayout.Width(50), GUILayout.Height(30) };

        GUILayout.BeginHorizontal();
        GUILayout.Space(50);
        if (GUILayout.Button("↑", style))
        {
            floorInfoItem.Z++;

        }
        GUILayout.Space(50);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("←", style))
        {
            floorInfoItem.X--;
        }
        GUILayout.Space(50);

        if (GUILayout.Button("→", style))
        {
            floorInfoItem.X++;
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(50);
        if (GUILayout.Button("↓", style))
        {
            floorInfoItem.Z--;
        }

        GUILayout.Space(50);

        GUILayout.EndHorizontal();


    }

    //快捷键开关
    static bool b_openShortcutKey = false;
    //根据wsad 按键，进行微调
    private void AdjustFloorPosWSAD()
    {
        GUI.color = b_openShortcutKey == true ? Color.green : Color.white;  //如果开启快捷键，那么要提示出来，因为跟unity快捷键冲突

        b_openShortcutKey = GUILayout.Toggle(b_openShortcutKey, "打开快捷键调整功能", GUILayout.Width(150));

        if (b_openShortcutKey == false)
        {
            return;
        }

        Event e = Event.current;
        if (e.isKey)
        {
            if (e.type == EventType.keyUp )
            {
                switch (e.keyCode)
                {
                    case KeyCode.W: floorInfoItem.Z++; break;
                    case KeyCode.S: floorInfoItem.Z--; break;
                    case KeyCode.A: floorInfoItem.X--; break;
                    case KeyCode.D: floorInfoItem.X++; break;
                }



            }
        }

    }

    #endregion


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (floorInfoItem == null) //避免选中prefab时的报错
        {
            return;
        }
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("应用当前输入坐标: ("+ floorInfoItem.X + "," + floorInfoItem.Z +")为逻辑坐标"))
        {
            floorInfoItem.ApplyPosFromInput();
        }
        if (GUILayout.Button("打印本地板的信息"))
        {
            floorInfoItem.DebugFloorInfo();
        }
        GUILayout.EndHorizontal();
        if (GUILayout.Button("应用当前输入的逻辑坐标，并且对齐"))
        {
            floorInfoItem.ApplyPosFromInput();
            floorInfoItem.ApplyPositionFromPos();
        }
        if (GUILayout.Button("拾取当前世界坐标为逻辑坐标，并且对齐"))
        {
            floorInfoItem.ApplyAndAlignFromPosition();
        }
        BitchCreat();





        GUILayout.EndVertical();
    }
    #region OnInspectorGUI
    //寻找场景中所有 地板格
    private void FindAllFloorItem()
    {
        allFloorInfo.Clear();
        FloorItem[] allFloorItem =  GameObject.FindObjectsOfType<FloorItem>();
        for (int i = 0; i < allFloorItem.Length; i++)
        {
            allFloorInfo.Add(allFloorItem[i]);
        }

    }

    //展示所有地板格信息
    private void ShowAllFloorItemInfo()
    {
        if (floorInfoItem == null || !b_showItemInfo)
        {
            return;
        }
        for (int i = 0; i < allFloorInfo.Count; i++)
        {
            if (allFloorInfo[i] == null)
            {
                continue;
            }
            FloorInfo floorInfo = allFloorInfo[i].floorInfo;
            Vector3 v3_pos = allFloorInfo[i].transform.position;
            GUI.color = Color.black;
            Handles.Label(v3_pos + floorInfoShowOffset, "(" + floorInfo.x + "," + floorInfo.z + ")");
        }
    }


    private Vector3Int v3_bitchCreat_from;
    private Vector3Int v3_bitchCreat_to;
    //批量创建
    private void BitchCreat()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("From:", GUILayout.Width(50));
        v3_bitchCreat_from.x = int.Parse( GUILayout.TextField(v3_bitchCreat_from.x.ToString(), GUILayout.Width(50)));
        v3_bitchCreat_from.z = int.Parse(GUILayout.TextField(v3_bitchCreat_from.z.ToString(), GUILayout.Width(50)));
        if (GUILayout.Button("清空"))
        {
            if (EditorUtility.DisplayDialog("清空批量创建", "确定要清空批量创建嘛？ 数量：" + floorInfoItem.bitchCreats.Count, "是", "取消"))
            {
                floorInfoItem.ClearBitchCreat();
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("To:", GUILayout.Width(50));
        v3_bitchCreat_to.x = int.Parse(GUILayout.TextField(v3_bitchCreat_to.x.ToString(), GUILayout.Width(50)));
        v3_bitchCreat_to.z = int.Parse(GUILayout.TextField(v3_bitchCreat_to.z.ToString(), GUILayout.Width(50)));

        if (GUILayout.Button("批量创建"))
        {
            if (EditorUtility.DisplayDialog("批量创建", "确定要批量创建嘛？ " + floorInfoItem.name, "是", "取消"))
            {
                floorInfoItem.BatchCreat(v3_bitchCreat_from, v3_bitchCreat_to);
            }
        }
        GUILayout.EndHorizontal();

    }

    #endregion

}
