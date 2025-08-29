using FFF.Behaviours.UI;
using FFF.Player;
using FFF.Utils;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class EndCanvasBehaviour : CanvasBehaviour
   {
      private AudioSource m_backgroundMusic;

      private AudioSource m_sfx;

      public override void Reset()
      {
         if (m_backgroundMusic == null)
         {
            m_backgroundMusic = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.BackgroundMusicPlayerTag).GetComponent<AudioSource>();
         }

         m_backgroundMusic.Stop();

         if (m_sfx == null)
         {
            m_sfx = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.SFXPlayerTag).GetComponent<AudioSource>();
         }

         m_sfx.volume = PlayerSaveSingleton.Instance.SFXVolume;

         //m_sfx.clip = SFXDatabaseSingleton.Instance.Database.EndSound;

         base.Reset();
      }
   }
}
