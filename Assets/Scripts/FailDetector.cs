using System;
using UnityEngine;

public class FailDetector : MonoBehaviour
{
    public Rigidbody2D[] rigid;

    public int clickableCount;

    protected Man man;

    public bool checkManInScreen = true;

    private bool isConfirming;

    protected virtual void Start()
    {
        ClickDismiss expr_05 = UnityEngine.Object.FindObjectOfType<ClickDismiss>();
        expr_05.onDismiss = (Action<GameObject>)Delegate.Combine(expr_05.onDismiss, new Action<GameObject>(this.onItemClick));
        this.man = UnityEngine.Object.FindObjectOfType<Man>();
    }

    private void Update()
    {
        if (!this.isConfirming && this.isFail())
        {
            this.isConfirming = true;
            Timer.Register(0.2f, delegate
            {
                if (this.isFail())
                {
                    UnityEngine.Object.FindObjectOfType<GameController>().onFailDetected();
                    UnityEngine.Object.Destroy(base.gameObject);
                }
                else
                {
                    this.isConfirming = false;
                }
            }, null, false, false, this);
        }
    }

    public virtual bool isFail()
    {
        if (this.clickableCount > 0)
        {
            return false;
        }
        Rigidbody2D[] array = this.rigid;
        for (int i = 0; i < array.Length; i++)
        {
            Rigidbody2D rigidbody2D = array[i];
            if (FailDetector.isInScreen(rigidbody2D.transform.position) && FailDetector.isMoving(rigidbody2D))
            {
                return false;
            }
        }
        return (this.checkManInScreen && !this.man.isInScreen()) || !this.man.isMoving();
    }

    public static bool isMoving(Rigidbody2D rb)
    {
        return rb.velocity.magnitude >= 0.1f || Mathf.Abs(rb.angularVelocity) >= 5f;
    }

    public static bool isInScreen(Vector3 pos)
    {
        Vector3 vector = Camera.main.WorldToViewportPoint(pos);
        return vector.x > 0f && vector.x < 1f && vector.y > 0f && vector.y < 1f;
    }

    private void onItemClick(GameObject obj)
    {
        this.clickableCount--;
    }
}
