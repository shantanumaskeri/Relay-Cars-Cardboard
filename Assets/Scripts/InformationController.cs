using UnityEngine;
using System.Collections;

public class InformationController : MonoBehaviour 
{
	// Singleton instance
	public static InformationController Instance;

	// public variables
	public GameObject infoText1;
	public GameObject infoText2;

	// private variables
	bool isVisible;

	// Use this for initialization
	void Start ()
	{
		Instance = this;
	}

	void OnEnable () 
	{
		isVisible = false;
	}

	public void ExecuteInfoTransition ()
	{
		if (!VehicleController.Instance.isTriggered)
		{
			ControlVehicleInformation ();	
		}
	}

	void ControlVehicleInformation ()
	{
		isVisible = !isVisible;

		switch (ApplicationController.Instance.vehicleName)
		{
		case "Duster":
			infoText1.SetActive (isVisible);
			break;

		case "Kwid":
			infoText2.SetActive (isVisible);
			break;
		}
	}

	public void HideAllInformation ()
	{
		isVisible = false;

		infoText1.SetActive (false);
		infoText2.SetActive (false);
	}
}
