using System;
using UnityEngine;

[ExecuteInEditMode]
public class Linescript : MonoBehaviour
{
	public Transform ConnectedObj;

	private LineRenderer linerend;

	private void Start()
	{
		this.linerend = base.GetComponent<LineRenderer>();
	}

	private void Update()
	{
		this.linerend.SetPosition(0, base.transform.position);
		this.linerend.SetPosition(1, this.ConnectedObj.position);
	}
}
