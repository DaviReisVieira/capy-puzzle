using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Man : MonoBehaviour
{
    private sealed class _startCountdown_c__AnonStorey0
    {
        internal float timeLimitTotal;

        internal Man _this;

        internal void __m__0(float x)
        {
            this._this.controller.updateTime(x);
        }
    }

    public GameObject head;

    public GameObject link;


    public GameObject smokeWaterParticle;

    private Tweener tweener;

    private GameController controller;

    private Rigidbody2D headRb;

    private bool hasStartCountdown;

    private static DOGetter<float> __f__am_cache0;

    private void Start()
    {
        this.headRb = this.head.GetComponent<Rigidbody2D>();
    }

    public void startCountdown()
    {
        if (this.hasStartCountdown)
        {
            return;
        }
        this.hasStartCountdown = true;
        TimeLimit timeLimit = UnityEngine.Object.FindObjectOfType<TimeLimit>();
        float timeLimitTotal = timeLimit.Time1 + timeLimit.Time2 + timeLimit.Time3;
        this.controller = UnityEngine.Object.FindObjectOfType<GameController>();
        this.tweener = DOTween.To(() => 0f, delegate (float x)
        {
            this.controller.updateTime(x);
        }, timeLimitTotal, timeLimitTotal);
        this.tweener.SetUpdate(false);
        this.tweener.OnComplete(new TweenCallback(this.explode));
        this.tweener.SetEase(Ease.Linear);
    }

    public void onWin()
    {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.smokeWaterParticle);
        gameObject.transform.position = this.head.transform.position;
        this.tweener.Kill(false);
        this.prevendHingeJointBreak();
    }

    public void explode()
    {
        this.tweener.Kill(false);
        // SoundManager.Instance.playSound(SoundManager.Sound.boom);
        Water water = UnityEngine.Object.FindObjectOfType<Water>();
        if (water != null)
        {
            water.onFail();
        }
        this.controller.onFail();
    }

    private void prevendHingeJointBreak()
    {
        for (int i = 0; i < base.transform.childCount; i++)
        {
            HingeJoint2D component = base.transform.GetChild(i).GetComponent<HingeJoint2D>();
            if (component != null)
            {
                component.breakForce = 1000000f;
            }
        }
    }

    public bool isInScreen()
    {
        return FailDetector.isInScreen(this.headRb.transform.position);
    }

    public bool isMoving()
    {
        return FailDetector.isMoving(this.headRb);
    }
}
