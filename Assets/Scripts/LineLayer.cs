using System;
using UnityEngine;

[ExecuteInEditMode]
public class LineLayer : MonoBehaviour
{
	public int sortingOrder;

	private void Start()
	{
		base.GetComponent<LineRenderer>().sortingOrder = this.sortingOrder;
	}
}
