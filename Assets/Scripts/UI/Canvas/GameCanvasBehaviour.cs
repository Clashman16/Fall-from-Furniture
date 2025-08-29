using FFF.Utils;
using UnityEngine;
using FFF.Player;

namespace FFF.Behaviours.UI
{
   public class GameCanvasBehaviour : CanvasBehaviour
   {
      private AudioSource m_backgroundMusic;

      private EGameScreen m_lastGameScreen;

      public EGameScreen LastGameScreen
      {
         set => m_lastGameScreen = value;
      }

      public override void Reset()
      {
         if(m_backgroundMusic == null)
         {
            m_backgroundMusic = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.BackgroundMusicPlayerTag).GetComponent<AudioSource>();
         }
         
         m_backgroundMusic.volume = PlayerSaveSingleton.Instance.BackgroundMusicVolume;
         
         if (!m_backgroundMusic.isPlaying)
         {
            if (m_lastGameScreen != EGameScreen.PAUSE_SCREEN)
            {
               m_backgroundMusic.Play();
            }
            else
            {
               m_backgroundMusic.UnPause();
            }
         }
      }
   }
}
