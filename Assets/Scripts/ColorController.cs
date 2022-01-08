using UnityEngine;
using System.Collections;

public class ColorController : MonoBehaviour 
{
	// Singleton instance
	public static ColorController Instance;

	// public variables
	public GameObject brown;
	public GameObject green;
	public GameObject red;
	public GameObject silver;
	public GameObject menu;

	// private variables
	bool isGridOpen;
	bool isFadingIn;
	float alpha;
	Vector3 iniPos1;
	Vector3 iniPos2;
	Vector3 iniPos3;
	Vector3 iniPos4;

	// Use this for initialization
	void Start ()
	{
		Instance = this;
	}

	void OnEnable () 
	{
		isGridOpen = false;
		iniPos1 = brown.transform.position;
		iniPos2 = green.transform.position;
		iniPos3 = red.transform.position;
		iniPos4 = silver.transform.position;

		InitializeSprites ();
	}

	void Update ()
	{
		if (isFadingIn)
		{
			if (alpha < 1.0f)
			{
				alpha += 0.03f;
			}
			else 
			{
				isFadingIn = false;
				alpha = 1.0f;
			}

			switch (ApplicationController.Instance.vehicleName)
			{
			case "Duster":
				brown.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
				green.GetComponent<SpriteRenderer>().color = new Color (0.5f, 0.5f, 0.5f, alpha);

				brown.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
				green.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
				break;

			case "Kwid":
				red.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
				silver.GetComponent<SpriteRenderer>().color = new Color (0.75f, 0.75f, 0.75f, alpha);

				red.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
				silver.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
				break;
			}
		}
	}

	public void InitializeSprites ()
	{
		isFadingIn = false;
		alpha = 0.0f;

		brown.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		green.GetComponent<SpriteRenderer>().color = new Color (0.5f, 0.5f, 0.5f, 0.0f);
		red.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		silver.GetComponent<SpriteRenderer>().color = new Color (0.75f, 0.75f, 0.75f, 0.0f);

		brown.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		green.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		red.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		silver.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
	}

	public void ExecuteColorTransition (string tag)
	{
		if (!VehicleController.Instance.isTriggered)
		{
			if (tag.Equals ("Grid"))
			{
				isGridOpen = true;
				menu.GetComponentInChildren<TextMesh>().text = "Back";

				MenuController.Instance.ResetUIPosition ();
				MenuController.Instance.DisableSubMenuCollision ();
				MenuController.Instance.InitializeSprites ();
				ShowColorOptions ();
			}
			else
			{
				ApplyVehicleColor (GameObject.FindGameObjectWithTag (tag).name);
			}
		}
	}

	void ShowColorOptions ()
	{
		switch (ApplicationController.Instance.vehicleName)
		{
		case "Duster":
			isFadingIn = true;

			LeanTween.moveX (brown, brown.transform.position.x + 2.5f, 0.5f).setEase (LeanTweenType.easeOutSine);
			LeanTween.moveX (green, green.transform.position.x - 2.5f, 0.5f).setEase (LeanTweenType.easeOutSine);

			switch (ApplicationController.Instance.colorName)
			{
			case "Brown":
				GameObject.Find ("b_selected").GetComponent<MeshRenderer>().enabled = true;
				GameObject.Find ("g_selected").GetComponent<MeshRenderer>().enabled = false;
				break;

			case "Green":
				GameObject.Find ("b_selected").GetComponent<MeshRenderer>().enabled = false;
				GameObject.Find ("g_selected").GetComponent<MeshRenderer>().enabled = true;
				break;
			}
			break;

		case "Kwid":
			isFadingIn = true;

			LeanTween.moveX (red, red.transform.position.x + 2.5f, 0.5f).setEase (LeanTweenType.easeOutSine);
			LeanTween.moveX (silver, silver.transform.position.x - 2.5f, 0.5f).setEase (LeanTweenType.easeOutSine);

			switch (ApplicationController.Instance.colorName)
			{
			case "Red":
				GameObject.Find ("s_selected").GetComponent<MeshRenderer>().enabled = false;
				GameObject.Find ("r_selected").GetComponent<MeshRenderer>().enabled = true;
				break;

			case "Silver":
				GameObject.Find ("s_selected").GetComponent<MeshRenderer>().enabled = true;
				GameObject.Find ("r_selected").GetComponent<MeshRenderer>().enabled = false;
				break;
			}
			break;
		}

		Invoke ("EnableColorCollision", 1.0f);
	}

	void ApplyVehicleColor (string colorName)
	{
		ApplicationController.Instance.colorName = colorName;

		switch (colorName)
		{
		case "Brown":
			green.GetComponent<BoxCollider>().enabled = true;
			brown.GetComponent<BoxCollider>().enabled = false;

			GameObject.Find ("b_selected").GetComponent<MeshRenderer>().enabled = true;
			GameObject.Find ("g_selected").GetComponent<MeshRenderer>().enabled = false;
			break;

		case "Green":
			brown.GetComponent<BoxCollider>().enabled = true;
			green.GetComponent<BoxCollider>().enabled = false;

			GameObject.Find ("g_selected").GetComponent<MeshRenderer>().enabled = true;
			GameObject.Find ("b_selected").GetComponent<MeshRenderer>().enabled = false;
			break;

		case "Red":
			silver.GetComponent<BoxCollider>().enabled = true;
			red.GetComponent<BoxCollider>().enabled = false;

			GameObject.Find ("r_selected").GetComponent<MeshRenderer>().enabled = true;
			GameObject.Find ("s_selected").GetComponent<MeshRenderer>().enabled = false;
			break;

		case "Silver":
			red.GetComponent<BoxCollider>().enabled = true;
			silver.GetComponent<BoxCollider>().enabled = false;

			GameObject.Find ("s_selected").GetComponent<MeshRenderer>().enabled = true;
			GameObject.Find ("r_selected").GetComponent<MeshRenderer>().enabled = false;
			break;
		}

		VehicleController.Instance.DestroySequence ();
		VehicleController.Instance.InitializeSequence ();
	}

	public void ResetVehicleColor ()
	{
		switch (ApplicationController.Instance.vehicleName)
		{
		case "Duster":
			ApplicationController.Instance.colorName = "Brown";
			break;

		case "Kwid":
			ApplicationController.Instance.colorName = "Red";
			break;
		}
	}

	void EnableColorCollision ()
	{
		switch (ApplicationController.Instance.vehicleName)
		{
		case "Duster":
			switch (ApplicationController.Instance.colorName)
			{
			case "Brown":
				green.GetComponent<BoxCollider>().enabled = true;
				brown.GetComponent<BoxCollider>().enabled = false;
				break;

			case "Green":
				brown.GetComponent<BoxCollider>().enabled = true;
				green.GetComponent<BoxCollider>().enabled = false;
				break;
			}
			break;

		case "Kwid":
			switch (ApplicationController.Instance.colorName)
			{
			case "Red":
				silver.GetComponent<BoxCollider>().enabled = true;
				red.GetComponent<BoxCollider>().enabled = false;
				break;

			case "Silver":
				red.GetComponent<BoxCollider>().enabled = true;
				silver.GetComponent<BoxCollider>().enabled = false;
				break;
			}
			break;
		}
	}

	void DisableColorCollision ()
	{
		brown.GetComponent<BoxCollider>().enabled = false;
		green.GetComponent<BoxCollider>().enabled = false;
		red.GetComponent<BoxCollider>().enabled = false;
		silver.GetComponent<BoxCollider>().enabled = false;
	}

	public void CheckColorGridStatus ()
	{
		if (isGridOpen)
		{
			ResetColorGrid ();

			isGridOpen = isFadingIn = false;
			alpha = 0.0f;
			menu.GetComponentInChildren<TextMesh>().text = "Gaze to\nClose";
		}
	}

	void ResetColorGrid ()
	{
		brown.transform.position = iniPos1;
		green.transform.position = iniPos2;
		red.transform.position = iniPos3;
		silver.transform.position = iniPos4;

		GameObject.Find ("b_selected").GetComponent<MeshRenderer>().enabled = false;
		GameObject.Find ("g_selected").GetComponent<MeshRenderer>().enabled = false;
		GameObject.Find ("r_selected").GetComponent<MeshRenderer>().enabled = false;
		GameObject.Find ("s_selected").GetComponent<MeshRenderer>().enabled = false;

		DisableColorCollision ();
		InitializeSprites ();
	}
}