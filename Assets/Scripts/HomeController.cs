using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HomeController : BaseController
{
    private void Start()
    {
    }
    public override void Awake()
    {
        base.Awake();
    }

    private void OnDestroy()
    {
    }


    public void onPlay()
    {
        Debug.Log("onPlay");
        // LevelManager.continueGamePlay();
    }

    public void onCoinClick()
    {
        if (ChannelController.channel == 1)
        {
            return;
        }
        DialogController.instance.ShowDialog(DialogType.shop, DialogShow.OVER_CURRENT, "click");
    }

}
