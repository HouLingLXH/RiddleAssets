using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelWindow : UIBase {

    public GameObject btn;
    public InputField input;
    public const string c_assetPath = "ui/levelwindow";//UI assetbundle 所在位置
    public const string c_levelAssetPathHead = "scene/level/level_"; //所有普通关卡的assetbundle所在位置

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
        if (AssetBundleManager.s_async_operation != null)
        {
            //Debug.Log(AssetBundleManager.s_async_operation.progress);
        }
        

    }

    public override void OnClose()
    {
        base.OnClose();
    }

    public void Btn_LevelSelect()
    {
        Debug.Log("选择关卡" + input.text);
        AnimSystem.Move(btn,from:null,to: Vector3.one * 10000,time:1,callBack:(o)=>
        {
           
        });

        StartCoroutine(AssetBundleManager.LoadScene(c_levelAssetPathHead + input.text, "level_" + input.text, callBack: () =>
        {
            AnimSystem.StopAnim(btn);
        }));




    }

    //private IEnumerator LoadScene(string bundlePath, string sceneName)
    //{
    //    yield return AssetBundleManager.LoadScene<Object>(bundlePath, sceneName);

        
    //}


}
