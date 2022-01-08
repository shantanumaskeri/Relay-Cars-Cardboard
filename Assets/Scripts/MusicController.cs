using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour 
{
	// Singleton instance
	public static MusicController Instance;

	// public variables
	public AudioClip bgMusic;

	// private variables
	AudioSource audioSrc;
	bool isPlayingBgMusic;

	// Use this for initialization
	void Start () 
	{
		Instance = this;

		audioSrc = GetComponent<AudioSource>();
		isPlayingBgMusic = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void CheckForBgMusicAudio ()
	{
		if (Instance != null)
		{
			StopBgMusicAudio ();	
		}	
	}

	public void PlayBgMusicAudio ()
	{
		if (!isPlayingBgMusic)
		{
			audioSrc.clip = bgMusic;
			audioSrc.time = 0.0f;
			audioSrc.loop = true;
			audioSrc.Play ();

			isPlayingBgMusic = true;
		}
	}

	public void StopBgMusicAudio ()
	{
		if (isPlayingBgMusic)
		{
			audioSrc.Stop ();

			isPlayingBgMusic = false;
		}
	}
}
