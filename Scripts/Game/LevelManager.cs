using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    #region assetPath
    const string c_assetPath_player = "player";

    #endregion

    #region 关卡预设置
    public Vector3 v3_startPoint; //开始点
    #endregion


    #region 缓存的对象
    public GameObject go_mainCamera; //主摄像机
    public CameraFollow comp_cameraFollow; //摄像机跟随组件

    public GameObject go_player; //玩家
    public PlayerMove comp_playerMove;//玩家移动组件

    public FightWindow ui_fightWindow;//战斗界面
    #endregion



    // Use this for initialization
    void Start () {

        go_player = GameObjectManager.CreatGameObjectByPool("Player", c_assetPath_player, true);
        go_player.transform.position = v3_startPoint;
        comp_playerMove = go_player.GetComponent<PlayerMove>();

        go_mainCamera = GameObject.Find("Main Camera");
        comp_cameraFollow = GetComponent<CameraFollow>();
        comp_cameraFollow.Tran_FollowTarget = go_player.transform;

        //Timer.DelayPlay(2, ShowFightWindow);
    }

    private void ShowFightWindow(params object[] arg)
    {
        ui_fightWindow = UIManager.OpenUI<FightWindow>(FightWindow.c_assetPath);
        ui_fightWindow.comp_rocker.Init(comp_playerMove, comp_cameraFollow);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(v3_startPoint, Vector3.one);
    }

}
