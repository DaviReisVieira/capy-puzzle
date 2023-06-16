using System;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject particlePrefab;

    private bool failed;

    private bool finish;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!this.failed && !this.finish && other.gameObject.CompareTag("Man"))
        {
            this.finish = true;
            UnityEngine.Object.FindObjectOfType<GameController>().onLevelComplete();
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.particlePrefab);
            gameObject.transform.position = other.transform.position;
            SoundManager.Instance.playSound(SoundManager.Sound.jumpinwater);
        }
    }

    public void onFail()
    {
        this.failed = true;
    }
}
