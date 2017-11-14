using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : View {

	// Use this for initialization
	void Start () {
		
	}

	public void OnExitClicked() {
		TwoChoiceDialog choiceDialog = (TwoChoiceDialog) DialogBuilder.Create (DialogBuilder.DialogType.CHOICE_DIALOG);
		choiceDialog.SetMessage ("Quit the application?");
		choiceDialog.SetOnConfirmListener (() => {
			Application.Quit();
		});
	}

	public override void OnRootScreenBack ()
	{
		base.OnRootScreenBack ();
		this.OnExitClicked ();
	}
}
