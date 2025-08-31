using FFF.Sounds;
using FFF.Utils;
using UnityEngine;

namespace FFF.Behaviours.UI
{
   public class MainMenuCanvasBehaviour : CanvasBehaviour
   {
      private AudioSource m_backgroundMusic;
      private CatNoisesBehavior m_catNoises;

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

         base.ResetData();
      }
   }
}
