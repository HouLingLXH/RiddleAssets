using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AllFloorInfoEditor : Editor {

    List<FloorItem> allFlooeItemInScene = new List<FloorItem>();

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawGameObjectName(Transform transform, GizmoType gizmoType)
    {
        if (FloorInfoEditor.b_showItemInfo)
        {
            if (transform.GetComponent<FloorItem>() == null)
            {
                Handles.Label(transform.position,transform.gameObject.name);
            }

        }

        
    }
}
