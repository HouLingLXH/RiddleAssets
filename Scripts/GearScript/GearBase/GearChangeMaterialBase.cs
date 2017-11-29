using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearChangeMaterialBase : GearBase {

    public Material fromMaterial;
    public Material toMaterial;

    List<Material> fromMat = new List<Material>();
    List<Material> toMat = new List<Material>();
    List<Collider> col_Colliders = new List<Collider>();


    private void Start()
    {
        for (int i = 0; i < gos_child.Length; i++)
        {
            col_Colliders.Add(gos_child[i].GetComponent<Collider>());
        }
        SetCollider(false);
        ReHome();
    }

    //将材质设置为 toMaterial
    protected void ChangeTo()
    {
        for (int i = 0; i < gos_child.Length; i++)
        {
            if (toMat.Count < (i + 1))
            {
                Debug.Log("修改材质");
                Material newToMat = new Material(toMaterial);
                toMat.Add(newToMat);
            }
            gos_child[i].GetComponent<Renderer>().material = toMat[i];
        }

    }

    //将材质设置回 fromMaterial
    protected void ReHome()
    {
        for (int i = 0; i < gos_child.Length; i++)
        {
            if (fromMat.Count < (i + 1))
            {
                Material newFromMat = new Material(fromMaterial);
                fromMat.Add(newFromMat);
            }
            gos_child[i].GetComponent<Renderer>().material = fromMat[i];
        }
    }

    //设置碰撞盒状态
    protected void SetCollider(bool enable)
    {
        for (int i = 0; i < col_Colliders.Count; i++)
        {
            col_Colliders[i].enabled = enable;
        }
    }


}
