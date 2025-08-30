using FFF.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXBehavior : MonoBehaviour
{
  private AudioSource m_audioSource;

  public AudioClip m_furnitureDropped;
  public AudioClip m_win;

  public void PlayFX(ESoundFX p_soundType)
  {
    switch (p_soundType)
    {
      case ESoundFX.FURNITURE_DROPPED:
        Play(m_furnitureDropped);
        break;
      case ESoundFX.WIN:
        Play(m_win);
        break;
    }
  }

  private void Play(AudioClip audioClip)
  {
    m_audioSource.PlayOneShot(audioClip);
  }

  // Start is called before the first frame update
  void Start()
  {
    m_audioSource = GetComponent<AudioSource>();
  }
}
