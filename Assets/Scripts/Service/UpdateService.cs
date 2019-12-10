using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UpdateService 
{
    public void CheckUpdate(Action endCallback)
    {
        //检查是否需要更新，如果需要进行更新，更新完成后回调，如果不需要直接回调 TODO
        if (endCallback!=null)
        {
            endCallback();
        }
    }
}
