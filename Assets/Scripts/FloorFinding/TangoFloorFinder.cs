using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tango;

public class TangoFloorFinder : MonoBehaviour {

	[SerializeField] private GameObject m_marker;
	[SerializeField] private TangoPointCloud m_pointCloud;
	[SerializeField] private TangoPointCloudFloor m_pointCloudFloor;
	[SerializeField] private TangoApplication m_tangoApplication;

	private bool m_findingFloor = false;

	// Use this for initialization
	void Start () {
		m_marker.SetActive(false);
		m_pointCloud = FindObjectOfType<TangoPointCloud>();
		m_pointCloudFloor = FindObjectOfType<TangoPointCloudFloor>();
		m_tangoApplication = FindObjectOfType<TangoApplication>();

		EventBroadcaster.Instance.AddObserver (EventNames.ON_FLOOR_FIND_TRIGGERED, this.OnFloorFindTriggered);	
	}

	void OnDestroy() {
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_FLOOR_FIND_TRIGGERED);
	}

	void Update() {
		if (!m_findingFloor)
		{
			return;
		}

		// If the point cloud floor has found a new floor, place the marker at the found y position.
		if (m_pointCloudFloor.m_floorFound && m_pointCloud.m_floorFound)
		{
			m_findingFloor = false;

			// Place the marker at the center of the screen at the found floor height.
			m_marker.SetActive(true);
			Vector3 target;
			RaycastHit hitInfo;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f)), out hitInfo))
			{
				// Limit distance of the marker position from the camera to the camera's far clip plane. This makes sure that the marker
				// is visible on screen when the floor is found.
				Vector3 cameraBase = new Vector3(Camera.main.transform.position.x, hitInfo.point.y, Camera.main.transform.position.z);
				target = cameraBase + Vector3.ClampMagnitude(hitInfo.point - cameraBase, Camera.main.farClipPlane * 0.9f);
			}
			else
			{
				// If no raycast hit, place marker in the camera's forward direction.
				Vector3 dir = new Vector3(Camera.main.transform.forward.x, 0.0f, Camera.main.transform.forward.z);
				target = dir.normalized * (Camera.main.farClipPlane * 0.9f);
				target.y = m_pointCloudFloor.transform.position.y;
			}

			m_marker.transform.position = target;
			AndroidHelper.ShowAndroidToastMessage(string.Format("Floor found. Unity world height = {0}", m_pointCloudFloor.transform.position.y.ToString()));
		}
	}
	
	private void OnFloorFindTriggered() {

		Debug.Log ("Attempting to find floor");
		if (this.m_findingFloor == true) {
			return;
		}

		this.m_findingFloor = true;
		if (m_pointCloud == null)
		{
			Debug.LogError("TangoPointCloud required to find floor.");
			return;
		}

		m_findingFloor = true;
		m_marker.SetActive(false);
		m_tangoApplication.SetDepthCameraRate(TangoEnums.TangoDepthCameraRate.MAXIMUM);
		m_pointCloud.FindFloor();
	}
}
