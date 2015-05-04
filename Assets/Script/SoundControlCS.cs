using UnityEngine;
using System.Collections;

public class SoundControlCS : MonoBehaviour {

    public AudioClip soundWinCoin;
    public static SoundControlCS sound;
    private AudioSource soundEfct;
	private AudioSource soundBG;

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
	    soundEfct = sources[1];
		soundBG = sources[0];
	    loadSoundPref();
    }

    public void playWinCoin(){
	    soundEfct.clip = soundWinCoin;
	    soundEfct.Play();
    }

    public void adjustVol(bool musicState){
	    int valueEf;
		int valueBG;
	    if(musicState){
		    valueEf = 1;
			valueBG = 1;
	    }else{
		    valueEf = 0;
			valueBG = 0;
	    }
	    soundEfct.volume = valueEf;
		soundBG.volume = valueBG;
	    PlayerPrefs.SetInt("EfctVol", valueEf);
		PlayerPrefs.SetInt("BgVol", valueBG);
	
    }

    private void loadSoundPref(){
	    soundEfct.volume = PlayerPrefs.GetInt("EfctVol", 1);
		soundBG.volume = PlayerPrefs.GetInt("BgVol", 1);
    }

    private float getSoundVol(){
	    return soundEfct.volume;
    }

    public void stopWinCoin() {
        soundEfct.Stop();
    }

	public void stopBgSound(){
		soundBG.Stop();
	}
}
