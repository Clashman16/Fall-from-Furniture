using FFF.Managers;
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

      private LevelManager m_levelManager;

      private void Start()
      {
         m_timerDisplay = GetComponentInChildren<TextMeshProUGUI>();

         m_fTime = PlayerSaveSingleton.Instance.TimerMax * 60f;

         m_levelManager = GetComponentInParent<LevelManager>();
      }

      private void Update()
      {
         if (PlayerStateSingleton.Instance.GameScreen == EGameScreen.GAME_SCREEN && !m_levelManager.IsCheckingResult)
         {
            m_fTime -= Time.deltaTime;

            if (m_fTime <= 0)
            {
               m_fTime = 0;

               m_levelManager.DidPlayerWin = false;

               PlayerStateSingleton.Instance.GameScreen = EGameScreen.END_SCREEN;
            }
         }

         UpdateDisplay();
      }

      private void UpdateDisplay()
      {
         m_timerDisplay.text = string.Format("{0:0}:{1:00}", Mathf.FloorToInt(m_fTime / 60f), Mathf.FloorToInt(m_fTime % 60f));
      }

      public void ResetData()
      {
         m_fTime = PlayerSaveSingleton.Instance.TimerMax * 60f;
      }
   }
}
