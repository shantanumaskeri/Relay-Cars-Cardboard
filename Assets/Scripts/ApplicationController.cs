using UnityEngine;
using System.Collections;

public class ApplicationController : MonoBehaviour 
{
	// Singleton instance
	public static ApplicationController Instance;

	// public variables
	public GameObject cameraBlackPatch;
	public GameObject introductionManager;
	public GameObject experienceManager;
	public GameObject crosshair;

	[HideInInspector]
	public bool isLaunched;
	[HideInInspector]
	public string vehicleName;
	[HideInInspector]
	public string colorName;
	[HideInInspector]
	public int vehicleId;

	// private variables
	bool isFadingIn;
	bool isFadingOut;
	float alpha;
	MeshRenderer mr;
	GameObject[] ga;

	// Use this for initialization
	void Start () 
	{
		Instance = this;

		isLaunched = isFadingIn = isFadingOut = false;
		alpha = 0.0f;
		mr = cameraBlackPatch.GetComponent<MeshRenderer>();
		//crosshair.SetActive (false);

		StartCoroutine (PrepareTransition ());
	}

	IEnumerator PrepareTransition ()
	{
		yield return new WaitForSeconds (5.0f);
		isFadingIn = true;
	}

	void Update ()
	{
		ControlFadeTransition ();
		QuitApplication ();
	}

	public void ChangeApplicationState (string tag)
	{
		switch (tag)
		{
		case "Intro":
			isFadingIn = true;
			break;

		case "Home":
			isFadingIn = true;

			MenuController.Instance.ResetUIPosition ();
			MenuController.Instance.DisableSubMenuCollision ();
			MenuController.Instance.InitializeSprites ();
			InformationController.Instance.HideAllInformation ();
			break;

		case "Menu":
			MenuController.Instance.ExecuteMenuTransition ();
			break;

		case "Interior":
		case "Exterior":
			CameraController.Instance.ExecuteCameraTransition (tag);
			break;

		case "Lighting":
			CameraController.Instance.ExecuteLightingTransition ();
			break;

		case "Swap":
			VehicleController.Instance.ExecuteVehicleTransition ();
			break;

		case "Info":
			InformationController.Instance.ExecuteInfoTransition ();
			break;

		case "Grid":
		case "Brown":
		case "Green":
		case "Red":
		case "Silver":
			ColorController.Instance.ExecuteColorTransition (tag);
			break;

		case "Video":
			VehicleController.Instance.ExeucteTurntableTransition ();
			break;

		case "Gear":
		case "Window":
			CameraController.Instance.ExecuteCalloutTransition (tag);
			break;
		}
	}

	void ControlFadeTransition ()
	{
		if (isFadingIn)
		{
			mr.enabled = true;

			if (alpha < 1.0f)
			{
				alpha += 0.1f;
				mr.material.color = new Color (mr.material.color.r, mr.material.color.g, mr.material.color.b, alpha);
			}
			else 
			{
				isFadingIn = false;
				alpha = 1.0f;

				Invoke ("SwitchScenes", 0.1f);
			}
		}
		if (isFadingOut)
		{
			if (alpha > 0.0f)
			{
				alpha -= 0.1f;
				mr.material.color = new Color (mr.material.color.r, mr.material.color.g, mr.material.color.b, alpha);
			}
			else 
			{
				isFadingOut = false;
				alpha = 0.0f;

				mr.enabled = false;
			}
		}
	}

	void SwitchScenes ()
	{
		isFadingOut = true;

		if (!experienceManager.activeSelf)
		{
			isLaunched = true;

			introductionManager.SetActive (false);
			experienceManager.SetActive (true);
			//crosshair.SetActive (true);

			AssignVehicleInformation ();
			StopCoroutine (PrepareTransition ());
			MusicController.Instance.PlayBgMusicAudio ();
		}
		else
		{
			isLaunched = false;

			introductionManager.SetActive (true);
			experienceManager.SetActive (false);
			//crosshair.SetActive (false);

			StartCoroutine (PrepareTransition ());
			MusicController.Instance.StopBgMusicAudio ();
		}
	}

	public void AssignVehicleInformation ()
	{
		ga = GameObject.FindGameObjectsWithTag ("Vehicle");
		foreach (GameObject go in ga)
		{
			if (go.activeSelf)
			{
				vehicleName = go.name;

				switch (vehicleName)
				{
				case "Duster":
					vehicleId = 0;
					colorName = "Brown";
					break;

				case "Kwid":
					vehicleId = 1;
					colorName = "Red";
					break;
				}
			}
		}
	}

	private void QuitApplication ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit ();
		}
	}
}
