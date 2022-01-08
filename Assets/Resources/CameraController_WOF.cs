using UnityEngine;
using System.Collections;

public class CameraController_WOF : MonoBehaviour 
{
	// Singleton instance
	public static CameraController_WOF Instance;

	// public variables
	public Transform ovrRigController;
	public Transform interiorReference1;
	public Transform interiorReference2;
	public Transform experienceManager;
	public GameObject cameraBlackPatch;
	public GameObject interior1;
	public GameObject interior2;
	public GameObject exterior1;
	public GameObject exterior2;
	public GameObject interiorD;
	public GameObject interiorDL;
	public GameObject interiorK;
	public GameObject interiorKL;
	public GameObject gearHotspot_D;
	public GameObject windowHotspot_D;
	public GameObject gearHotspot_K;
	public GameObject windowHotspot_K;
	public GameObject menu;

	[HideInInspector]
	public bool isInsideAuto;

	// private variables
	Vector3 initialPosition;
	GameObject hotspot;

	// Use this for initialization
	void Start ()
	{
		Instance = this;
	}

	void OnEnable () 
	{
		isInsideAuto = false;
		initialPosition = ovrRigController.position;
		interiorDL.SetActive (false);
		interiorKL.SetActive (false);
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	public void ExecuteLightingTransition ()
	{
		switch (ApplicationController.Instance.vehicleName)
		{
		case "Duster":
			if (interiorD.activeSelf)
			{
				interiorD.SetActive (false);
				interiorDL.SetActive (true);
			}
			else
			{
				interiorD.SetActive (true);
				interiorDL.SetActive (false);
			}
			break;

		case "Kwid":
			if (interiorK.activeSelf)
			{
				interiorK.SetActive (false);
				interiorKL.SetActive (true);
			}
			else
			{
				interiorK.SetActive (true);
				interiorKL.SetActive (false);
			}
			break;
		}
	}

	public void ExecuteCameraTransition (string tag)
	{
		if (!VehicleController.Instance.isTriggered)
		{
			if (tag.Equals ("Interior"))
			{
				if (!isInsideAuto)
				{
					MenuController.Instance.ResetUIPosition ();
					MenuController.Instance.DisableSubMenuCollision ();
					MenuController.Instance.DisableMenuCollision ();
					MenuController.Instance.InitializeSprites ();
					InformationController.Instance.HideAllInformation ();
					SwitchCameraView ();
				}
			}
			else if (tag.Equals ("Exterior"))
			{
				if (isInsideAuto)
				{
					MenuController.Instance.EnableMenuCollision ();
					SwitchCameraView ();
				}
			}	
		}
	}

	void SwitchCameraView ()
	{
		isInsideAuto = !isInsideAuto;

		if (isInsideAuto)
		{
			PrepareInteriorView ();
		}
		else
		{
			PrepareExteriorView ();
		}
	}

	void PrepareInteriorView ()
	{
		VehicleController.Instance.DestroySequence ();

		switch (ApplicationController.Instance.vehicleName)
		{
		case "Duster":
			interior1.SetActive (true);
			exterior1.SetActive (false);

			ovrRigController.position = interiorReference1.position;
			ovrRigController.rotation = interiorReference1.rotation;
			break;

		case "Kwid":
			interior2.SetActive (true);
			exterior2.SetActive (false);

			ovrRigController.position = interiorReference2.position;
			ovrRigController.rotation = interiorReference2.rotation;
			break;
		}

		gearHotspot_D.SetActive (false);
		gearHotspot_K.SetActive (false);
		windowHotspot_D.SetActive (false);
		windowHotspot_K.SetActive (false);
		menu.SetActive (false);
	}

	void PrepareExteriorView ()
	{
		gearHotspot_D.SetActive (false);
		gearHotspot_K.SetActive (false);
		windowHotspot_D.SetActive (false);
		windowHotspot_K.SetActive (false);

		switch (ApplicationController.Instance.vehicleName)
		{
		case "Duster":
			interiorD.SetActive (true);
			interiorDL.SetActive (false);

			interior1.SetActive (false);
			exterior1.SetActive (true);
			break;

		case "Kwid":
			interiorK.SetActive (true);
			interiorKL.SetActive (false);

			interior2.SetActive (false);
			exterior2.SetActive (true);
			break;
		}
			
		ovrRigController.position = initialPosition;
		ovrRigController.rotation = Quaternion.identity;

		menu.SetActive (true);

		VehicleController.Instance.isTriggered = VehicleController.Instance.isLoaded = false;

		MenuController.Instance.InitializeSprites ();
		VehicleController.Instance.InitializeSequence ();
	}

	public void ExecuteCalloutTransition (string tag)
	{
		hotspot = GameObject.FindGameObjectWithTag (tag);

		switch (hotspot.name)
		{
		case "icon-gear":
			switch (ApplicationController.Instance.vehicleName)
			{
			case "Duster":
				SwitchHotspotView (gearHotspot_D, windowHotspot_D);
				break;

			case "Kwid":
				SwitchHotspotView (gearHotspot_K, windowHotspot_K);
				break;
			}
			break;

		case "icon-window":
			switch (ApplicationController.Instance.vehicleName)
			{
			case "Duster":
				SwitchHotspotView (windowHotspot_D, gearHotspot_D);
				break;

			case "Kwid":
				SwitchHotspotView (windowHotspot_K, gearHotspot_K);
				break;
			}
			break;
		}
	}

	void SwitchHotspotView (GameObject hotspot1, GameObject hotspot2)
	{
		if (hotspot1.activeSelf)
		{
			hotspot1.SetActive (false);
		}
		else
		{
			hotspot1.SetActive (true);
		}
		hotspot2.SetActive (false);
	}
}
