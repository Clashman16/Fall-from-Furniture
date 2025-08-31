using FFF.Utils;
using UnityEngine;
using FFF.Player;
using FFF.Managers;
using FFF.Sounds;

namespace FFF.Behaviours.UI
{
   public class GameCanvasBehaviour : CanvasBehaviour
   {
      private AudioSource m_backgroundMusic;
      private CatNoisesBehavior m_catNoises;

      private EGameScreen m_lastGameScreen;

      public EGameScreen LastGameScreen
      {
         set => m_lastGameScreen = value;
      }

      private LevelManager m_levelManager;

      public LevelManager LevelManager
      {
         get => m_levelManager;
      }

      private ScreenManagerBehaviour m_screenManager;

      public override void ResetData()
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

         if (m_catNoises == null)
         {
            m_catNoises = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.SFXPlayerTag).GetComponent<CatNoisesBehavior>();
         }

         m_catNoises.Play();

         if (m_screenManager == null)
         {
            m_screenManager = FindObjectOfType<ScreenManagerBehaviour>();
         }

         if (m_lastGameScreen != EGameScreen.PAUSE_SCREEN || !m_screenManager.IsResuming)
         {
            if (m_levelManager == null)
            {
               m_levelManager = GetComponent<LevelManager>();
            }

            m_levelManager.LaunchLevel();
         } 
      }
   }
}
