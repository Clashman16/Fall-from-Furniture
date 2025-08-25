using FFF.Player;
using FFF.Utils;
using TMPro;
using UnityEngine;

namespace FFF.Behaviours.UI
{
   public class TimerBehaviour : MonoBehaviour
   {
      private TextMeshProUGUI m_timerDisplay;

      private float m_fTime;

      private void Start()
      {
         m_timerDisplay = GetComponentInChildren<TextMeshProUGUI>();

         m_fTime = PlayerSaveSingleton.Instance.TimerMax * 60f;
      }

      private void Update()
      {
         if (PlayerStateSingleton.Instance.GameScreen == EGameScreen.GAME_SCREEN)
         {
            m_fTime -= Time.deltaTime;

            if (m_fTime <= 0)
            {
               m_fTime = 0;

               PlayerStateSingleton.Instance.GameScreen = EGameScreen.END_SCREEN;
            }
         }

         UpdateDisplay();
      }

      private void UpdateDisplay()
      {
         m_timerDisplay.text = string.Format("{0:0}:{1:00}", Mathf.FloorToInt(m_fTime / 60f), Mathf.FloorToInt(m_fTime % 60f));
      }

      public void Reset()
      {
         m_fTime = PlayerSaveSingleton.Instance.TimerMax * 60f;
      }
   }
}
