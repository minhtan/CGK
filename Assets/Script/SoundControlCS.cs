using UnityEngine;
using System.Collections;

public class SoundControlCS : MonoBehaviour {

    public AudioClip soundWinCoin;
    public static SoundControlCS sound;
    private AudioSource soundEfct;

    void Awake(){
	    if(sound == null){
		    DontDestroyOnLoad(gameObject);
		    sound = this;
		    getSource();
	    }else if(sound != this){
		    Destroy(gameObject);
	    }
    }
	
    private void getSource(){
	    AudioSource[] sources = GetComponents<AudioSource>();
	    soundEfct = sources[0];
	    loadSoundPref();
    }

    public void playWinCoin(){
	    soundEfct.clip = soundWinCoin;
        Debug.Log("-------------------------");
	    soundEfct.Play();
    }

    public void adjustVol(bool musicState){
	    int valueEf;
	    if(musicState){
		    valueEf = 1;
	    }else{
		    valueEf = 0;
	    }
	    soundEfct.volume = valueEf;
	    PlayerPrefs.SetInt("EfctVol", valueEf);
	
    }

    private void loadSoundPref(){
	    soundEfct.volume = PlayerPrefs.GetInt("EfctVol", 1);
    }

    private float getSoundVol(){
	    return soundEfct.volume;
    }

    public void stopWinCoin() {
        soundEfct.Stop();
    }
}
