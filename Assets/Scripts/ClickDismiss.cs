using System;
using UnityEngine;

public class ClickDismiss : MonoBehaviour
{
    // public UselessClickHint uselessClickHint;

    public GameObject particles;

    public Action<GameObject> onDismiss;

    // private HintHelpter hintHelper;

    private int uselessClickCount;

    private void Start()
    {
        // this.hintHelper = UnityEngine.Object.FindObjectOfType<HintHelpter>();
    }

    private void Update()
    {
#if !UNITY_EDITOR
        Debug.Log("AMIGO ESTOU AQUI");
        if (Input.touches != null)
        {
            Touch[] touches = Input.touches;
            for (int i = 0; i < touches.Length; i++)
            {
                Touch touch = touches[i];
                if (touch.phase == TouchPhase.Began)
                {
                    Vector2 origin = Camera.main.ScreenToWorldPoint(touch.position);
                    RaycastHit2D[] array = Physics2D.RaycastAll(origin, Vector2.zero);
                    if (array.Length != 0)
                    {
                        RaycastHit2D[] array2 = array;
                        for (int j = 0; j < array2.Length; j++)
                        {
                            RaycastHit2D raycastHit2D = array2[j];
                            if (!(raycastHit2D.collider == null))
                            {
                                if (raycastHit2D.collider.tag == "clickable")
                                {
                                    // if (!this.hintHelper.isInHint || this.hintHelper.check(raycastHit2D.collider.gameObject))
                                    // {
                                    UnityEngine.Object.Instantiate<GameObject>(this.particles, raycastHit2D.transform.position, Quaternion.identity);
                                    if (raycastHit2D.collider.gameObject.GetComponent<TimeDismissAppear>() != null)
                                    {
                                        raycastHit2D.collider.gameObject.GetComponent<TimeDismissAppear>().onClick();
                                    }
                                    else
                                    {
                                        raycastHit2D.collider.gameObject.SetActive(false);
                                        // if (raycastHit2D.collider.gameObject.GetComponent<Balloon>() != null)
                                        // {
                                        //     SoundManager.Instance.playSound(SoundManager.Sound.ballon);
                                        // }
                                        // else
                                        // {
                                        // }
                                        SoundManager.Instance.playSound(SoundManager.Sound.redClick);
                                        if (this.onDismiss != null)
                                        {
                                            this.onDismiss(raycastHit2D.collider.gameObject);
                                        }
                                    }
                                    UnityEngine.Object.FindObjectOfType<Man>().startCountdown();
                                    break;
                                    // }
                                }
                                else if (!DialogController.instance.IsDialogShowing())
                                {
                                    this.uselessClickCount++;
                                    // Timer.Register(2f, delegate
                                    // {
                                    //     this.uselessClickCount--;
                                    // }, null, false, false, this);
                                    if (this.uselessClickCount >= 3)
                                    {
                                        // this.uselessClickHint.showHint();
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }
#endif

#if UNITY_EDITOR


				Debug.Log("AMIGO ESTOU AQUI 2");
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D[] array = Physics2D.RaycastAll(origin, Vector2.zero);
                    if (array.Length != 0)
                    {
                        RaycastHit2D[] array2 = array;
                        for (int j = 0; j < array2.Length; j++)
                        {
                            RaycastHit2D raycastHit2D = array2[j];
                            if (!(raycastHit2D.collider == null))
                            {
                                if (raycastHit2D.collider.tag == "clickable")
                                {
                                    // if (!this.hintHelper.isInHint || this.hintHelper.check(raycastHit2D.collider.gameObject))
                                    // {
                                        UnityEngine.Object.Instantiate<GameObject>(this.particles, raycastHit2D.transform.position, Quaternion.identity);
                                        if (raycastHit2D.collider.gameObject.GetComponent<TimeDismissAppear>() != null)
                                        {
                                            raycastHit2D.collider.gameObject.GetComponent<TimeDismissAppear>().onClick();
                                        }
                                        else
                                        {
                                            raycastHit2D.collider.gameObject.SetActive(false);
                                            // if (raycastHit2D.collider.gameObject.GetComponent<Balloon>() != null)
                                            // {
                                            //     SoundManager.Instance.playSound(SoundManager.Sound.ballon);
                                            // }
                                            // else
                                            // {
                                            // }
                                                SoundManager.Instance.playSound(SoundManager.Sound.redClick);
                                            if (this.onDismiss != null)
                                            {
                                                this.onDismiss(raycastHit2D.collider.gameObject);
                                            }
                                        }
                                        UnityEngine.Object.FindObjectOfType<Man>().startCountdown();
                                        break;
                                    // }
                                }
                                else if (!DialogController.instance.IsDialogShowing())
                                {
                                    this.uselessClickCount++;
                                    // Timer.Register(2f, delegate
                                    // {
                                    //     this.uselessClickCount--;
                                    // }, null, false, false, this);
                                    // if (this.uselessClickCount >= 3)
                                    {
                                        // this.uselessClickHint.showHint();
                                    }
                                }
                            }
                        }
                    }
                }

#endif
    }
}
