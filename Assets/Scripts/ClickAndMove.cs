using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAndMove : MonoBehaviour
{
    public GameObject particles;

    public Action<GameObject> onDismiss;

    private GameObject downObj;

    private Vector3 downPos;

    private bool move;

    private Vector3 start;

    private Vector3 end;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            this.move = false;
            this.downObj = this.getPointerObj();
            if (this.downObj != null)
            {
                this.downPos = this.downObj.transform.position;
                this.start = this.downObj.GetComponent<MoveTrail>().start.position;
                this.end = this.downObj.GetComponent<MoveTrail>().end.position;
            }
        }
        else if (Input.GetMouseButton(0) && !Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (this.downObj != null)
            {
                Vector3 point = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                point.z = 0f;
                this.downObj.transform.position = ClickAndMove.Point2LinePoint(point, this.start, this.end);
                if (Vector3.Distance(this.downPos, this.downObj.transform.position) > 0.5f)
                {
                    this.move = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject() && this.downObj != null)
        {
            if (!this.move)
            {
                this.downObj.SetActive(false);
                UnityEngine.Object.Instantiate<GameObject>(this.particles, this.downObj.transform.position, Quaternion.identity);
                if (this.onDismiss != null)
                {
                    this.onDismiss(this.downObj);
                }
            }
            this.downObj = null;
        }
    }

    private GameObject getPointerObj()
    {
        Vector2 origin = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, Vector2.zero);
        if (raycastHit2D.collider == null)
        {
            return null;
        }
        if (raycastHit2D.collider.tag == "clickable_movable")
        {
            return raycastHit2D.collider.gameObject;
        }
        return null;
    }

    public static Vector3 Point2LinePoint(Vector3 point, Vector3 linePoint1, Vector3 linePoint2)
    {
        Vector3 vector = point - linePoint1;
        Vector3 onNormal = linePoint2 - linePoint1;
        Vector3 a = Vector3.Project(vector, onNormal);
        Vector3 vector2 = a + linePoint1;
        float num = Vector3.Distance(linePoint1, linePoint2);
        float num2 = Vector3.Distance(vector2, linePoint2);
        float num3 = Vector3.Distance(linePoint1, vector2);
        if (Math.Abs(num - (num3 + num2)) <= 0.1f)
        {
            return vector2;
        }
        return (num3 <= num2) ? linePoint1 : linePoint2;
    }
}
