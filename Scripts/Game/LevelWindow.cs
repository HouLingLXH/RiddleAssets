using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelWindow : UIBase {

    public GameObject btn;
    public InputField input;
    public const string s_assetPath = "ui/levelwindow";

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
            Debug.Log(AssetBundleManager.s_async_operation.progress);
        }
        

    }

    public override void OnClose()
    {
        base.OnClose();
    }

    public void Btn_LevelSelect(int index)
    {
        Debug.Log("选择关卡" + index);
        AnimSystem.Move(btn,from:null,to: Vector3.one * 10000,time:1,callBack:(o)=>
        {
           
        });

        StartCoroutine(AssetBundleManager.LoadScene("scene/level/level_" + input.text, "level_" + input.text, callBack: () =>
        {
            AnimSystem.StopAnim(btn);
        }));




    }

    //private IEnumerator LoadScene(string bundlePath, string sceneName)
    //{
    //    yield return AssetBundleManager.LoadScene<Object>(bundlePath, sceneName);

        
    //}


}
