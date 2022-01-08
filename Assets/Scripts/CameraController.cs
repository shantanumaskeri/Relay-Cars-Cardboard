using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	// Singleton instance
	public static CameraController Instance;

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
	bool isFadingIn;
	bool isFadingOut;
	float alpha;
	Vector3 initialPosition;
	GameObject hotspot;
	MeshRenderer mr;

	// Use this for initialization
	void Start ()
	{
		Instance = this;
	}

	void OnEnable () 
	{
		isFadingIn = isFadingOut = isInsideAuto = false;
		alpha = 0.0f;
		initialPosition = ovrRigController.position;
		mr = cameraBlackPatch.GetComponent<MeshRenderer>();
		interiorDL.SetActive (false);
		interiorKL.SetActive (false);
	}

	// Update is called once per frame
	void Update () 
	{
		ControlFadeTransition ();
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
		if (alpha.Equals (0.0f))
		{
			if (!VehicleController.Instance.isTriggered)
			{
				if (tag.Equals ("Interior"))
				{
					if (!isInsideAuto)
					{
						isFadingIn = true;

						MenuController.Instance.DisableSubMenuCollision ();
						MenuController.Instance.DisableMenuCollision ();
						InformationController.Instance.HideAllInformation ();
					}
				}
				else if (tag.Equals ("Exterior"))
				{
					if (isInsideAuto)
					{
						isFadingIn = true;
					}
				}	
			}
		}
	}

	void ControlFadeTransition ()
	{
		if (isFadingIn)
		{
			mr.enabled = true;

			if (alpha < 1.0f)
			{
				alpha += 0.05f;
				mr.material.color = new Color (mr.material.color.r, mr.material.color.g, mr.material.color.b, alpha);
			}
			else 
			{
				isFadingIn = false;
				alpha = 1.0f;

				SwitchCameraView ();
			}
		}
		if (isFadingOut)
		{
			if (alpha > 0.0f)
			{
				alpha -= 0.05f;
				mr.material.color = new Color (mr.material.color.r, mr.material.color.g, mr.material.color.b, alpha);
			}
			else 
			{
				isFadingOut = false;
				mr.enabled = false;
				alpha = 0.0f;
			}
		}
	}

	void SwitchCameraView ()
	{
		isFadingOut = true;
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

		MenuController.Instance.ResetUIPosition ();
		MenuController.Instance.InitializeSprites ();
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
		menu.GetComponentInChildren<TextMesh>().text = "Gaze to\nOpen";

		VehicleController.Instance.isTriggered = VehicleController.Instance.isLoaded = false;

		MenuController.Instance.EnableMenuCollision ();
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
