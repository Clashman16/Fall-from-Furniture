using FFF.Sounds;
using FFF.Utils;
using TMPro;
using UnityEngine;

namespace FFF.Behaviours.UI
{
   public class EndCanvasBehaviour : CanvasBehaviour
   {
      private AudioSource m_backgroundMusic;

      private CatNoisesBehavior m_catNoises;

      private TextMeshProUGUI m_feedbackDisplay;

      private NextLevelButtonBehaviour m_nextLevelBtn;

      public override void ResetData()
      {
         if (m_backgroundMusic == null)
         {
            m_backgroundMusic = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.BackgroundMusicPlayerTag).GetComponent<AudioSource>();
         }

         m_backgroundMusic.Stop();

         if (m_catNoises == null)
         {
            m_catNoises = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.SFXPlayerTag).GetComponent<CatNoisesBehavior>();
         }
         m_catNoises.Stop();

         //if (m_sfx == null)
         //{
         //   m_sfx = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.SFXPlayerTag).GetComponent<AudioSource>();
         //}

         //m_sfx.volume = PlayerSaveSingleton.Instance.SFXVolume;

         //m_sfx.clip = SFXDatabaseSingleton.Instance.Database.EndSound;

         base.ResetData();
      }

      public void Init(bool p_bDidPlayerWin)
      {
         if (m_feedbackDisplay == null)
         {
            m_feedbackDisplay = GetComponentInChildren<TextMeshProUGUI>();
         }

         m_feedbackDisplay.text = p_bDidPlayerWin ? "Bravo!": "You loose!" ;

         if(m_nextLevelBtn == null)
         {
            m_nextLevelBtn = GetComponentInChildren<NextLevelButtonBehaviour>();
         }

         m_nextLevelBtn.gameObject.SetActive(p_bDidPlayerWin);
      }
   }
}
