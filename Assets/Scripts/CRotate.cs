using System;
using UnityEngine;

public class CRotate : MonoBehaviour
{
    public float speed;

    public bool useUnscaledTime;

    private void Update()
    {
        base.transform.Rotate(Vector3.forward * ((!this.useUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime) * this.speed);
    }
}
