using UnityEngine;
using System.Collections;
using UnityEngine.VR;
using UnityEngine.UI;

public class EyeGazeController : MonoBehaviour 
{
	// Singleton instance
	public static EyeGazeController Instance;

	// public variables
	public Transform cameraCrosshair;
	public Image fillerCrosshair;

	// private variables
	bool isActive;

	// Use this for initialization
	IEnumerator Start () 
	{
		Instance = this;

		isActive = false;

		yield return new WaitForSeconds (2.0f);
		Initialize ();
	}
		
	void Initialize ()
	{
		isActive = true;
	}

	// Update is called once per frame
	void Update () 
	{
		CheckRaycastCollisions ();
	}

	void CheckRaycastCollisions ()
	{
		Ray ray = new Ray (cameraCrosshair.position, -cameraCrosshair.forward);
		RaycastHit hit = new RaycastHit ();

		if (Physics.Raycast (ray, out hit, Mathf.Infinity))
		{
			if (isActive)
			{
				if (fillerCrosshair.fillAmount < 1.0f)
				{
					fillerCrosshair.fillAmount += 0.02f;

					SFXController.Instance.PlayGazeAudio ();
				}
				else
				{
					ApplicationController.Instance.ChangeApplicationState (hit.collider.gameObject.tag);

					fillerCrosshair.fillAmount = 0.0f;
					isActive = false;

					SFXController.Instance.StopGazeAudio ();
					SFXController.Instance.PlaySelectionAudio ();
					SFXController.Instance.ResetAudio ();

					Invoke ("Initialize", 2.0f);
				}
			}
			else
			{
				fillerCrosshair.fillAmount -= 0.02f;

				if (fillerCrosshair.fillAmount <= 0.0f)
				{
					SFXController.Instance.CheckForGazeAudio ();
				}
			}
		}
		else
		{
			fillerCrosshair.fillAmount -= 0.02f;

			if (fillerCrosshair.fillAmount <= 0.0f)
			{
				SFXController.Instance.CheckForGazeAudio ();
			}
		}
	}
}
