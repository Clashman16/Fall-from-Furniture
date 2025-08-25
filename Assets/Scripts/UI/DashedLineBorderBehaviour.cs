using FFF.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace FFF.Behaviours.UI
{
   public class DashedLineBorderBehaviour : MonoBehaviour
   {
      LevelManager m_levelManager;

      public LevelManager LevelManager
      {
         get => m_levelManager;
      }

      private LineRenderer m_border;

      private Image m_imgFurnitureDisplay;

      private bool m_isWaitingSelection;

      private Button m_button;

      public bool IsWaitingSelection
      {
         get => m_isWaitingSelection;
         set
         {
            m_isWaitingSelection = value;

            m_button.interactable = m_isWaitingSelection;

            if (m_isWaitingSelection == false)
            {
               m_border.material.SetColor("_MainColor", Color.grey);
            }
         }
      }

      #region Gradient feedback

      private Gradient m_selectionGradient;

      private float m_gradientTimer;
      private int m_timerCoef;

      #endregion

      public virtual void Init(LevelManager p_manager)
      {
         DrawBorder();

         GradientAlphaKey[] l_lstAlpha = new GradientAlphaKey[] { new GradientAlphaKey(1, 0)};

         GradientColorKey[] l_lstColor = new GradientColorKey[] { 
            new GradientColorKey(Color.yellow, 0) ,
            new GradientColorKey(new Color32(255, 106, 0, 1), 1)
         };

         m_selectionGradient = new Gradient();
         m_selectionGradient.SetKeys(l_lstColor, l_lstAlpha);

         m_gradientTimer = 0;

         m_timerCoef = 1;

         m_button = GetComponent<Button>();

         m_levelManager = p_manager;
      }

      private void DrawBorder()
      {
         m_border = GetComponent<LineRenderer>();

         m_imgFurnitureDisplay = GetComponent<Image>();

         m_border.material = new Material(Shader.Find("Custom/DashedLine"));
         m_border.material.SetColor("_MainColor", Color.grey);
         m_border.startWidth = 0.05f;
         m_border.endWidth = 0.05f;

         m_border.useWorldSpace = false;
         m_border.loop = true;

         RectTransform l_trf = m_imgFurnitureDisplay.rectTransform;

         Vector3[] l_corners = new Vector3[4] {
            new Vector3(l_trf.rect.xMin, l_trf.rect.yMin, 0f),
            new Vector3(l_trf.rect.xMin, l_trf.rect.yMax, 0f),
            new Vector3(l_trf.rect.xMax, l_trf.rect.yMax, 0f),
            new Vector3(l_trf.rect.xMax, l_trf.rect.yMin, 0f)
         };

         m_border.positionCount = l_corners.Length;
         m_border.SetPositions(l_corners);
      }

      private void Update()
      {
         if(m_isWaitingSelection == true)
         {
            if (m_gradientTimer > 1 || m_gradientTimer < 0)
            {
               m_timerCoef *= -1;
            }

            Color l_newColor = m_selectionGradient.Evaluate(m_gradientTimer);

            m_border.material.SetColor("_MainColor", l_newColor);

            m_gradientTimer += 0.005f * m_timerCoef;
         }
      }

      public void OnClick()
      {
         m_levelManager.LastSelectedInteractable = this;
      }
   }
}
