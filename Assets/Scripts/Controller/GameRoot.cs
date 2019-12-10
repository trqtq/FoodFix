using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameRoot : MonoBehaviour
{
    public UpdateService updateService;
    void Awake()
    {
        //首先应该启动更新程序，等待更新程序检查后的反馈
        updateService.CheckUpdate(LunchEnd);
    }
    //更新完成后的回调，在这里启动
    void LunchEnd()
    {
        Debug.LogError("更新完成，进入游戏");
    }
}
