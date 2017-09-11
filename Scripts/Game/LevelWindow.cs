using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelWindow : UIBase {

    public GameObject btn;
    private void Start()
    {

    }

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnOpen()
    {
        base.OnOpen();

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnClose()
    {
        base.OnClose();
    }

    public void Btn_LevelSelect(int index)
    {
        Debug.Log("选择关卡" + index);
        AnimSystem.Move(btn,from:null,to: Vector3.one * 10000,time:1);
    }

}
