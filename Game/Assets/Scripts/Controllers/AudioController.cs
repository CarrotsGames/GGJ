using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    public static AudioController instance;

    public AudioSource musicTrack;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if(musicTrack != null)
            musicTrack.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
