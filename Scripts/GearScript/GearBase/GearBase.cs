using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBase : MonoBehaviour {

    public int n_id;

    public GameObject[] gos_child; //控制的物体

    public Color col_gizmoColor = Color.blue; //绘制颜色

    private Collider collider;//碰撞盒


    public readonly Vector3 v3_drawUp = new Vector3(0, 5, 0);

    public  virtual void OnDrawGizmos()
    {
        if (collider == null)
        {
            collider = GetComponent<Collider>();
        }

        if (collider == null)
        {
            return;
        }

        Gizmos.color = col_gizmoColor;

        

        if (collider is BoxCollider)
        {
           
            BoxCollider boxCollider = new BoxCollider();
            Gizmos.DrawCube(transform.position ,Vector3.one * 0.5f);
        }

        if (collider is SphereCollider)
        {
            SphereCollider sphereCollider = new SphereCollider();
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }

        if (gos_child == null)
        {
            return;
        }

        for (int i = 0; i < gos_child.Length; i++)
        {
            Gizmos.DrawLine(transform.position, gos_child[i].transform.position);
            Gizmos.DrawWireSphere(gos_child[i].transform.position, 0.5f);
        }


    }

    //进入碰撞器
    public virtual void TriggerEnter(Collider other)
    {
        Debug.Log(other.name +  "进入碰撞器:" + this.name + "  ID: " + n_id);
    }

    //退出碰撞器
    public virtual void TriggerExit(Collider other)
    {
        Debug.Log(other.name + "退出碰撞器:" + this.name + "  ID: " + n_id);
    }

    //进入碰撞器
    public void OnTriggerEnter(Collider other)
    {
        TriggerEnter(other);
    }

    //出碰撞器
    public void OnTriggerExit(Collider other)
    {
        TriggerExit(other);
    }




}
