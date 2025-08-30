using FFF.Behaviours.UI;
using FFF.Utils;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class PauseCanvasBehaviour : CanvasBehaviour
   {
      private AudioSource m_backgroundMusic;
      private CatNoisesBehavior m_catNoises;

      public override void Reset()
      {
         if (m_backgroundMusic == null)
         {
            m_backgroundMusic = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.BackgroundMusicPlayerTag).GetComponent<AudioSource>();
         }

         m_backgroundMusic.Pause();

         if (m_catNoises == null)
         {
            m_catNoises = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.SFXPlayerTag).GetComponent<CatNoisesBehavior>();
         }
         m_catNoises.Pause();

         base.Reset();
      }
   }
}
