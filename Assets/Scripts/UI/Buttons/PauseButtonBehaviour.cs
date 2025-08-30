using FFF.Managers;
using FFF.Player;
using FFF.Utils;
using UnityEngine.UI;

namespace FFF.Behaviours.UI
{
   public class PauseButtonBehaviour : OneActionButtonBehaviour
   {
      private LevelManager m_levelManager;

      private Button m_button;

      public override void Act()
      {
         base.Act();

         PlayerStateSingleton.Instance.GameScreen = EGameScreen.PAUSE_SCREEN;
      }

      private void Update()
      {
         if(m_levelManager == null)
         {
            m_levelManager = GetComponentInParent<LevelManager>();
         }

         if(m_button == null)
         {
            m_button = GetComponent<Button>();
         }

         m_button.interactable = PlayerStateSingleton.Instance.GameScreen == EGameScreen.GAME_SCREEN && !m_levelManager.IsCheckingResult;
      }
   }
}
