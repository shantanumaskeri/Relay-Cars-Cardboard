using UnityEngine;
using System.Collections;

public class SFXController : MonoBehaviour 
{
	// Singleton instance
	public static SFXController Instance;

	// public variables
	public AudioClip gazeSfx;
	public AudioClip selectionSfx;

	// private variables
	AudioSource audioSrc;
	bool isPlayingGazeSfx;
	bool isPlayingSelectionSfx;

	// Use this for initialization
	void Start () 
	{
		Instance = this;

		audioSrc = GetComponent<AudioSource>();

		ResetAudio ();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void CheckForGazeAudio ()
	{
		if (Instance != null)
		{
			StopGazeAudio ();	
		}	
	}

	public void PlayGazeAudio ()
	{
		if (!isPlayingGazeSfx)
		{
			audioSrc.clip = gazeSfx;
			audioSrc.time = 1.0f;
			audioSrc.Play ();

			isPlayingGazeSfx = true;
		}
	}

	public void StopGazeAudio ()
	{
		if (isPlayingGazeSfx)
		{
			audioSrc.Stop ();

			isPlayingGazeSfx = false;
		}
	}

	public void CheckForSelectionAudio ()
	{
		if (Instance != null)
		{
			StopSelectionAudio ();	
		}	
	}

	public void PlaySelectionAudio ()
	{
		if (!isPlayingSelectionSfx)
		{
			audioSrc.clip = selectionSfx;
			audioSrc.time = 0.0f;
			audioSrc.Play ();

			isPlayingSelectionSfx = true;
		}
	}

	public void StopSelectionAudio ()
	{
		if (isPlayingSelectionSfx)
		{
			audioSrc.Stop ();

			isPlayingSelectionSfx = false;
		}
	}

	public void ResetAudio ()
	{
		isPlayingGazeSfx = isPlayingSelectionSfx = false;
	}
}
