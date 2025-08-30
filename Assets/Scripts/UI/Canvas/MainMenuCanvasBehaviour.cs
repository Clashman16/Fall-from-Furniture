using FFF.Utils;
using UnityEngine;

namespace FFF.Behaviours.UI
{
   public class MainMenuCanvasBehaviour : CanvasBehaviour
   {
      private AudioSource m_backgroundMusic;
      private CatNoisesManager m_catNoises;

      public override void Reset()
      {
         if (m_backgroundMusic == null)
         {
            m_backgroundMusic = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.BackgroundMusicPlayerTag).GetComponent<AudioSource>();
         }
         
         m_backgroundMusic.Stop();

         if (m_catNoises == null)
         {
            m_catNoises = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.SFXPlayerTag).GetComponent<CatNoisesManager>();
         }
         m_catNoises.Stop();

         base.Reset();
      }
   }
}
