using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    public AudioSource jumpAudio;
    public AudioSource deathAudio;
    public AudioSource coinAudio;
    public VolumeSlider volumeSlider;

	void OnEnable()
    {
        float volume = PlayerPrefs.GetFloat("VolumeLevel", 0.8f);
        AudioListener.volume = volume;

        PlayerController.OnJump += JumpSound;
        PlayerController.OnDeath += DeathSound;
        PickupPoints.OnPickup += CoinSound;
    }
    void OnDisable ()
    {
        PlayerController.OnJump -= JumpSound;
        PlayerController.OnDeath -= DeathSound;
        PickupPoints.OnPickup -= CoinSound;
    }

    public void OnVolumeSliderChange (float volume)
    {
        PlayerPrefs.SetFloat("VolumeLevel", volume);
        AudioListener.volume = volume;
        volumeSlider.UpdateVolumeText(volume);
    }

    void JumpSound ()
    {
        jumpAudio.Play();
    }
    void DeathSound ()
    {
        deathAudio.Play();
    }
    void CoinSound ()
    {
        if (coinAudio.isPlaying)
            coinAudio.Stop();
        coinAudio.Play();
    }
}
