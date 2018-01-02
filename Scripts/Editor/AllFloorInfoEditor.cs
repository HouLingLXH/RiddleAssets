using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AllFloorInfoEditor : Editor {
    //是否展示地板以外物体的信息
    public static bool b_showOthersInfo = false;

    //scene视图下，始终执行
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawGameObjectName(Transform transform, GizmoType gizmoType)
    {
        if (FloorInfoEditor.b_showItemInfo && b_showOthersInfo)
        {
            ShowOthersInfo(transform);
        }
    }

    //展示地板以外物体的信息
    static private void ShowOthersInfo(Transform transform)
    {
        if (transform.GetComponent<FloorItem>() == null)
        {
            Handles.Label(transform.position, transform.gameObject.name);
        }
    }

}
