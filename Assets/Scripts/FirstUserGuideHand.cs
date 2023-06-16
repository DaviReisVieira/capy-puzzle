using System;
using UnityEngine;

public class FirstUserGuideHand : MonoBehaviour
{
    private void Start()
    {
        ClickDismiss expr_05 = UnityEngine.Object.FindObjectOfType<ClickDismiss>();
        expr_05.onDismiss = (Action<GameObject>)Delegate.Combine(expr_05.onDismiss, new Action<GameObject>(this.onItemClick));
    }

    private void onItemClick(GameObject clickable)
    {
        base.gameObject.SetActive(false);
    }
}
