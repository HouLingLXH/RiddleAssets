using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWindow : UIBase {

    public const string c_assetPath ="ui/loadingwindow";

    public Slider loadProgress;

    public override void OnInit()
    {
        base.OnInit();

    }


    public override void OnOpen()
    {
        base.OnOpen();
        Debug.Log("Open LoadingWindow!");

        loadProgress.value = 0;

    }


    public override void OnUpdate()
    {
        base.OnUpdate();

        if (loadProgress.value < 1)
        {
            loadProgress.value += Time.deltaTime;
        }
        else
        {
            UIManager.OpenUI<LevelWindow>(LevelWindow.c_assetPath);
            OnClose();
        }
        


    }

    public override void OnClose()
    {
        base.OnClose();
    }

}
