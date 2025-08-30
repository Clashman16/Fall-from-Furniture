using UnityEngine;

public class CatNoisesManager : MonoBehaviour
{
  private AudioSource m_AudioSource;

  public AudioClip[] m_lstCatSounds;

  private readonly float m_fMinCountdown = 3f;
  private readonly float m_fMaxCountdown = 8f;
  private float m_fCountdown = 0f;


  private RandomIndexWithoutRepetition m_randomIndexWithoutRepetition;

  private void ResetRandomCountdown()
  {
    m_fCountdown = Random.Range(m_fMinCountdown, m_fMaxCountdown);
  }

  private void PlayRandomSound()
  {
    m_AudioSource.PlayOneShot(m_lstCatSounds[m_randomIndexWithoutRepetition.Next]);
    ResetRandomCountdown();
  }

  // Start is called before the first frame update
  void Start()
  {
    m_AudioSource = GetComponent<AudioSource>();
    m_randomIndexWithoutRepetition = new RandomIndexWithoutRepetition(m_lstCatSounds.Length);
  }

  // Update is called once per frame
  void Update()
  {
    if (!m_AudioSource.isPlaying)
    {
      if (m_fCountdown < 0f)
      {
        PlayRandomSound();
      }
      else
      {
        m_fCountdown -= Time.deltaTime;
      }
    }
  }
}
