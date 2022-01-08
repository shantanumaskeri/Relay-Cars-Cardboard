using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour 
{
	// Singleton instance
	public static VehicleController Instance;

	// public variables
	public GameObject automobile1;
	public GameObject automobile2;
	public GameObject autoSequence1;
	public GameObject autoSequence2;

	[HideInInspector]
	public bool isTriggered;
	[HideInInspector]
	public bool isLoaded;

	// private variables
	bool isOneRotationComplete;
	int textureCount;
	int tempCount;
	int triggerCount;
	Object[] textures;

	void Start ()
	{
		Instance = this;
	}

	void OnEnable () 
	{
		isTriggered = isLoaded = isOneRotationComplete = false;
		triggerCount = 0;

		StartCoroutine (PrepareInitialization ());
	}

	void OnDisable () 
	{
		StopCoroutine (PrepareInitialization ());
	}

	IEnumerator PrepareInitialization ()
	{
		yield return new WaitForSeconds (0.01f);
		InitializeSequence ();
		yield return new WaitForSeconds (0.01f);
		isLoaded = false;
	}

	public void InitializeSequence ()
	{
		textureCount = triggerCount;
		textures = Resources.LoadAll ("Sequence/"+ApplicationController.Instance.vehicleName+"/"+ApplicationController.Instance.colorName, typeof (Sprite));

		ApplyVehicleTexture ();
	}

	public void DestroySequence ()
	{
		textures = null;
	}

	// Update is called once per frame
	void Update () 
	{
		if (isTriggered)
		{
			InitializeTurntable (3);
		}
		else
		{
			if (isLoaded)
			{
				InitializeTurntable (7);	
			}
		}
	}

	public void ExecuteVehicleTransition ()
	{
		if (!isTriggered)
		{
			if (ApplicationController.Instance.vehicleId < 1)
			{
				ApplicationController.Instance.vehicleId++;
			}
			else
			{
				ApplicationController.Instance.vehicleId = 0;
			}

			automobile1.SetActive (false);
			automobile2.SetActive (false);

			switch (ApplicationController.Instance.vehicleId)
			{
			case 0:
				automobile1.SetActive (true);
				break;

			case 1:
				automobile2.SetActive (true);
				break;
			}

			triggerCount = 0;

			ColorController.Instance.ResetVehicleColor ();
			InformationController.Instance.HideAllInformation ();
			ApplicationController.Instance.AssignVehicleInformation ();
			DestroySequence ();
			InitializeSequence ();
		}
	}

	public void ExecuteVehicleTrigger ()
	{
		isLoaded = false;
		triggerCount = textureCount;
	}

	public void ExeucteTurntableTransition ()
	{
		isTriggered = true;
	}

	void InitializeTurntable (int speed)
	{
		if (ApplicationController.Instance.isLaunched)
		{
			if (!CameraController.Instance.isInsideAuto)
			{
				tempCount++;

				if ((tempCount % speed).Equals (0))
				{
					if (textureCount < textures.Length - 1)
					{
						textureCount += 1;
					}
					else
					{
						textureCount = 0;

						if (isTriggered)
						{
							isOneRotationComplete = true;	
						}
					}

					tempCount = 0;
				}

				if (isOneRotationComplete)
				{
					if (textureCount >= triggerCount)
					{
						isTriggered = isLoaded = isOneRotationComplete = false;
					}
				}
					
				ApplyVehicleTexture ();
			}
		}
	}

	void ApplyVehicleTexture ()
	{
		switch (ApplicationController.Instance.vehicleName)
		{
		case "Duster":
			autoSequence1.GetComponent<SpriteRenderer>().sprite = textures [textureCount] as Sprite;	
			break;

		case "Kwid":
			autoSequence2.GetComponent<SpriteRenderer>().sprite = textures [textureCount] as Sprite;
			break;
		}

		Resources.UnloadUnusedAssets ();
	}
}
