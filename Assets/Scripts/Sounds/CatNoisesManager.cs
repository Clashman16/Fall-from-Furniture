using System.Collections;
using UnityEngine;

public class CatNoisesManager : MonoBehaviour
{
  private AudioSource m_AudioSource;

  public AudioClip[] m_lstCatSounds;

  private readonly float m_fMinCountdown = 3f;
  private readonly float m_fMaxCountdown = 8f;

  private RandomIndexWithoutRepetition m_randomIndexWithoutRepetition;

  private IEnumerator m_coroutine;

  private float RandomCountdown
  {
    get => Random.Range(m_fMinCountdown, m_fMaxCountdown);
  }

  private void PlayRandomSound()
  {
    m_AudioSource.PlayOneShot(m_lstCatSounds[m_randomIndexWithoutRepetition.Next]);
  }

  private IEnumerator CoPlayDelayedClip(float p_delay)
  {
    yield return new WaitForSeconds(p_delay);
    PlayRandomSound();
    Play();
  }

  public void Play()
  {
    m_coroutine = CoPlayDelayedClip(RandomCountdown);
    StartCoroutine(m_coroutine);
  }

  public void Stop()
  {
    StopCoroutine(m_coroutine);
  }

  public void Pause()
  {
    Stop();
    PlayRandomSound();
  }

  // Start is called before the first frame update
  void Start()
  {
    m_AudioSource = GetComponent<AudioSource>();
    m_randomIndexWithoutRepetition = new RandomIndexWithoutRepetition(m_lstCatSounds.Length);
  }
}
