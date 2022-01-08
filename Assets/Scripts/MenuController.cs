using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour 
{
	// Singleton instance
	public static MenuController Instance;

	// public variables
	public GameObject swap;
	public GameObject interior;
	public GameObject home;
	public GameObject information;
	public GameObject video;
	public GameObject grid;
	public GameObject menu;

	// private variables
	bool isMenuOpen;
	bool isFadingIn;
	bool isFadingOut;
	float alpha;
	Vector3 pos;
	Vector3 iniPos1;
	Vector3 iniPos2;
	Vector3 iniPos3;
	Vector3 iniPos4;
	Vector3 iniPos5;
	Vector3 iniPos6;

	// Use this for initialization
	void Start ()
	{
		Instance = this;
	}

	void OnEnable () 
	{
		isMenuOpen = false;
		pos = Vector3.zero;
		iniPos1 = swap.transform.position;
		iniPos2 = interior.transform.position;
		iniPos3 = home.transform.position;
		iniPos4 = information.transform.position;
		iniPos5 = video.transform.position;
		iniPos6 = grid.transform.position;

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

			interior.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			swap.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			home.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			information.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			video.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			grid.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, alpha);

			interior.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			swap.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			home.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			information.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			video.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			grid.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
		}

		if (isFadingOut)
		{
			if (alpha > 0.0f)
			{
				alpha -= 0.03f;
			}
			else 
			{
				isFadingOut = false;
				alpha = 0.0f;
			}

			interior.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			swap.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			home.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			information.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			video.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			grid.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, alpha);

			interior.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			swap.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			home.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			information.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			video.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
			grid.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, alpha);
		}
	}

	public void InitializeSprites ()
	{
		isFadingIn = isFadingOut = false;
		alpha = 0.0f;

		interior.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		swap.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		home.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		information.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		video.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		grid.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);

		interior.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		swap.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		home.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		information.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		video.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		grid.GetComponentInChildren<TextMesh>().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
	}

	public void ExecuteMenuTransition ()
	{
		if (!VehicleController.Instance.isTriggered)
		{
			ControlMenuOptions ();
			VehicleController.Instance.ExecuteVehicleTrigger ();
			ColorController.Instance.CheckColorGridStatus ();
		}
	}

	void ControlMenuOptions ()
	{
		DisableSubMenuCollision ();
		InformationController.Instance.HideAllInformation ();

		if (!isMenuOpen)
		{
			isFadingIn = true;

			LeanTween.moveX (interior, interior.transform.position.x + 2.0f, 0.5f).setEase (LeanTweenType.easeOutSine);
			LeanTween.moveX (grid, grid.transform.position.x + 4.0f, 0.5f).setEase (LeanTweenType.easeOutSine);
			LeanTween.moveX (video, video.transform.position.x + 6.0f, 0.5f).setEase (LeanTweenType.easeOutSine);

			LeanTween.moveX (swap, swap.transform.position.x - 2.0f, 0.5f).setEase (LeanTweenType.easeOutSine);
			LeanTween.moveX (home, home.transform.position.x - 4.0f, 0.5f).setEase (LeanTweenType.easeOutSine);
			LeanTween.moveX (information, information.transform.position.x - 6.0f, 0.5f).setEase (LeanTweenType.easeOutSine);

			Invoke ("EnableSubMenuCollision", 1.0f);

			isMenuOpen = true;

			menu.GetComponentInChildren<TextMesh>().text = "Gaze to\nClose";
		}
		else
		{
			isFadingOut = true;

			LeanTween.moveX (interior, interior.transform.position.x - 2.0f, 0.5f).setEase (LeanTweenType.easeOutSine);
			LeanTween.moveX (grid, grid.transform.position.x - 4.0f, 0.5f).setEase (LeanTweenType.easeOutSine);
			LeanTween.moveX (video, video.transform.position.x - 6.0f, 0.5f).setEase (LeanTweenType.easeOutSine);

			LeanTween.moveX (swap, swap.transform.position.x + 2.0f, 0.5f).setEase (LeanTweenType.easeOutSine);
			LeanTween.moveX (home, home.transform.position.x + 4.0f, 0.5f).setEase (LeanTweenType.easeOutSine);
			LeanTween.moveX (information, information.transform.position.x + 6.0f, 0.5f).setEase (LeanTweenType.easeOutSine);

			Invoke ("ResetMainMenu", 1.0f);

			isMenuOpen = false;

			menu.GetComponentInChildren<TextMesh>().text = "Gaze to\nOpen";
		}	
	}

	public void EnableMenuCollision ()
	{
		menu.GetComponent<BoxCollider>().enabled = true;
	}

	public void DisableMenuCollision ()
	{
		menu.GetComponent<BoxCollider>().enabled = false;
	}

	public void EnableSubMenuCollision ()
	{
		interior.GetComponent<BoxCollider>().enabled = true;
		swap.GetComponent<BoxCollider>().enabled = true;
		home.GetComponent<BoxCollider>().enabled = true;
		information.GetComponent<BoxCollider>().enabled = true;
		video.GetComponent<BoxCollider>().enabled = true;
		grid.GetComponent<BoxCollider>().enabled = true;
	}

	public void DisableSubMenuCollision ()
	{
		interior.GetComponent<BoxCollider>().enabled = false;
		swap.GetComponent<BoxCollider>().enabled = false;
		home.GetComponent<BoxCollider>().enabled = false;
		information.GetComponent<BoxCollider>().enabled = false;
		video.GetComponent<BoxCollider>().enabled = false;
		grid.GetComponent<BoxCollider>().enabled = false;
	}

	public void ResetUIPosition ()
	{
		isMenuOpen = false;

		swap.transform.position = iniPos1;
		interior.transform.position = iniPos2;
		home.transform.position = iniPos3;
		information.transform.position = iniPos4;
		video.transform.position = iniPos5;
		grid.transform.position = iniPos6;
	}

	void ResetMainMenu ()
	{
		VehicleController.Instance.isLoaded = false;

		ResetUIPosition ();
		InitializeSprites ();
		EnableMenuCollision ();
	}
}
