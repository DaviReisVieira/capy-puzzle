using System;
using UnityEngine;

public class ButtonGoToScene : ButtonBase
{
	public string scene;

	public bool acceptKeyBack;

	private void Update()
	{
		if (this.acceptKeyBack && UnityEngine.Input.GetKeyDown(KeyCode.Escape) && !DialogController.instance.IsDialogShowing())
		{
			this.OnClick();
		}
	}

	public override void OnClick()
	{
		SceneAnimCoverManager.Instance.loadScene(this.scene);
	}
}
