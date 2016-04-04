using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {

    public Text volumeText;

	void OnEnable ()
    {
        float volume = PlayerPrefs.GetFloat("VolumeLevel", 0.8f);
        gameObject.GetComponent<Slider>().normalizedValue = volume;
        UpdateVolumeText(volume);
    }

    public void UpdateVolumeText (float volume)
    {
        volumeText.text = "Volume: " + Mathf.Round(volume * 100) + "%";
    }
}
