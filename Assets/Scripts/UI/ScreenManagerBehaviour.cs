using FFF.Player;
using FFF.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace FFF.Behaviours.UI
{
   public class ScreenManagerBehaviour : MonoBehaviour
   {
      [SerializeField] private GameScreenObjectPair[] m_lstScreensInit;

      private Dictionary<EGameScreen, CanvasBehaviour> m_lstScreens;

      private EGameScreen m_lastGameScreen;

      private bool m_bIsResuming;

      public bool IsResuming
      {
         get => m_bIsResuming;
         set => m_bIsResuming = value;
      }

      private bool m_bDidPlayerWin;

      private void Start()
      {
         PlayerStateSingleton.Instance.GameScreen = EGameScreen.TITLE_SCREEN;

         m_lastGameScreen = EGameScreen.TITLE_SCREEN;

         m_lstScreens = new Dictionary<EGameScreen, CanvasBehaviour>();

         foreach(GameScreenObjectPair l_pair in m_lstScreensInit)
         {
            m_lstScreens.Add(l_pair.Key, l_pair.Value);

            if(l_pair.Key != EGameScreen.TITLE_SCREEN)
            {
               l_pair.Value.gameObject.SetActive(false);
            }
         }

         m_bIsResuming = false;

         m_bDidPlayerWin = false;
      }

      private void Update()
      {
        EGameScreen l_currentGameScreen = PlayerStateSingleton.Instance.GameScreen;
      
         if (l_currentGameScreen != m_lastGameScreen)
         {
            switch (l_currentGameScreen)
            {
               case EGameScreen.GAME_SCREEN:
                  m_lstScreens[EGameScreen.TITLE_SCREEN].gameObject.SetActive(false);
                  m_lstScreens[EGameScreen.PAUSE_SCREEN].gameObject.SetActive(false);
                  m_lstScreens[EGameScreen.END_SCREEN].gameObject.SetActive(false);
      
                  break;
      
               case EGameScreen.END_SCREEN:
                  GameCanvasBehaviour l_lastGameCanvas = m_lstScreens[EGameScreen.GAME_SCREEN].GetComponent<GameCanvasBehaviour>();

                  if(l_lastGameCanvas != null)
                  {
                     l_lastGameCanvas.LevelManager.Timer.gameObject.SetActive(false);

                     m_bDidPlayerWin = l_lastGameCanvas.LevelManager.DidPlayerWin;
                  }
      
                  break;
      
               case EGameScreen.CREDITS_SCREEN:
               case EGameScreen.SETTINGS_SCREEN:
                  m_lstScreens[EGameScreen.TITLE_SCREEN].gameObject.SetActive(false);
      
                  break;

               case EGameScreen.TITLE_SCREEN:
                  foreach(KeyValuePair<EGameScreen, CanvasBehaviour> l_pair in m_lstScreens)
                  {
                     l_pair.Value.gameObject.SetActive(false);
                  }
      
                  break;
            }

            CanvasBehaviour l_currentActiveCanvas = m_lstScreens[l_currentGameScreen];

            l_currentActiveCanvas.gameObject.SetActive(true);

            GameCanvasBehaviour l_gameCanvas = l_currentActiveCanvas.GetComponent<GameCanvasBehaviour>();
            if (l_gameCanvas != null)
            {
               l_gameCanvas.LastGameScreen = m_lastGameScreen;
            }

            EndCanvasBehaviour l_endCanvas = l_currentActiveCanvas.GetComponent<EndCanvasBehaviour>();
            if (l_endCanvas != null)
            {
               l_endCanvas.Init(m_bDidPlayerWin);
            }

            l_currentActiveCanvas.Reset();

            if (l_currentActiveCanvas != null && !l_currentActiveCanvas.HasTranslated)
            {
               l_currentActiveCanvas.TranslateCanvas();
            }

            m_bIsResuming = false;

            m_lastGameScreen = l_currentGameScreen;
         }
      }
   }
}
