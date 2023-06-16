using System;
using UnityEngine;

public class TimeDismissAppear : MonoBehaviour
{
    public float disAppearTime = 2f;

    public void onClick()
    {
        base.gameObject.SetActive(false);
        Timer.Register(this.disAppearTime, delegate
        {
            base.gameObject.SetActive(true);
        }, null, false, false, this);
    }
}
