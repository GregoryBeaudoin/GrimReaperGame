using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
 
public class OptionsMenu : MonoBehaviour {
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
	public AudioMixer mixer;
 
    void Start() {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
    }
    public void updateMusicVolume() {
		PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
		mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolumeSlider.value) * 20);
		if (musicVolumeSlider.value <= 0)
			mixer.SetFloat("MusicVolume", -80);
	}
 
	public void updateEffectsVolume() {
		PlayerPrefs.SetFloat("EffectsVolume", effectsVolumeSlider.value);
		mixer.SetFloat("EffectsVolume", Mathf.Log10(effectsVolumeSlider.value) * 20);
		if (effectsVolumeSlider.value <= 0)
			mixer.SetFloat("EffectsVolume", -80);
	}
}