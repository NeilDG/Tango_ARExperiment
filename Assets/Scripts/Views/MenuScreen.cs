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

	public void OnFloorFinderClicked() {
		LoadManager.Instance.LoadScene (SceneNames.FLOOR_FINDING_SCENE);
	}

	public void MyFirstARClicked() {
		LoadManager.Instance.LoadScene (SceneNames.MY_FIRST_AR_SCENE);
	}

	public void ExperimentARClicked() {
		LoadManager.Instance.LoadScene (SceneNames.TEST_AR_SCENE);
	}

	public override void OnRootScreenBack ()
	{
		base.OnRootScreenBack ();
		this.OnExitClicked ();
	}
}
