using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFloorScreen : View {

	// Use this for initialization
	void Start () {
		
	}
	
	public void OnMenuButtonClicked() {
		LoadManager.Instance.LoadScene (SceneNames.MAIN_SCENE);
	}

	public void OnFindFloorClicked() {
		EventBroadcaster.Instance.PostEvent (EventNames.ON_FLOOR_FIND_TRIGGERED);
	}
}
