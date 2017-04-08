using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour 
{
	public AudioSource musicSource;
	public static MusicManager instanceMM = null;

	public static MusicManager Instance
	{
		get { return instanceMM; }
	}

	// Use this for initialization
	void Awake () {
		if (instanceMM != null && instanceMM != this) {
			Destroy (this.gameObject);
			return;
		} else
		{
			instanceMM = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (!(GetComponent<AudioSource> ().isPlaying))
		{
			GetComponent<AudioSource> ().Play ();
		}
	}
}
