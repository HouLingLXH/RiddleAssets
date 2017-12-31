using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FloorItem))]
public class FloorInfoEditor :Editor{

    readonly Vector3 floorInfoShowOffset = new Vector3(0,1,0);

    public static bool b_showItemInfo = true;
    public static List<FloorItem> allFloorInfo = new List<FloorItem>();


    private void OnSceneGUI()
    {
        Handles.BeginGUI();
        GUILayout.BeginVertical();
        b_showItemInfo = GUILayout.Toggle(b_showItemInfo,"是否显示地板格信息",GUILayout.Width(150));

        if (GUILayout.Button("寻找所有地板格 floorItem", GUILayout.Width(150)))
        {
            FindAllFloorItem();
        }

        GUILayout.EndVertical();
        Handles.EndGUI();

        FloorItem floorItem = (FloorItem)target;
        if (floorItem == null || !b_showItemInfo)
        {
            return;
        }

        ShowAllFloorItemInfo();



    }


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
        for (int i = 0; i < allFloorInfo.Count; i++)
        {
            FloorInfo floorInfo = allFloorInfo[i].floorInfo;
            Vector3 v3_pos = allFloorInfo[i].transform.position;
            GUI.color = Color.green;
            Handles.Label(v3_pos + floorInfoShowOffset, "x: " + floorInfo.x + ",z: " + floorInfo.z);
        }
    }
}
