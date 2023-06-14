using System;
using System.Collections.Generic;
using UnityEngine;

public class ButtonVideo : ButtonBase
{
    public string VideoFrom = string.Empty;

    public override void OnClick()
    {

        AdsInitializer.Instance.LoadRewardedAd(this.VideoFrom);

    }
}
