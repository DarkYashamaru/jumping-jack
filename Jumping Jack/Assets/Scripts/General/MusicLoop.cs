using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicLoop : MonoBehaviour {
	AudioSource Audio;
	static MusicLoop Instance;

	void Awake ()
	{
		if (Instance == null) 
		{
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} 
		if(Instance!=this)
		{
			Destroy (gameObject);
		}
		Audio = GetComponent<AudioSource> ();
		Audio.loop = true;
		Audio.playOnAwake = true;
	}
}
