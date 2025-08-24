using UnityEngine;
using UnityEngine.UI;

namespace FFF.UI
{
   public class FurnitureDropSlotBehaviour : MonoBehaviour
   {
      private LineRenderer m_border;

      private Image m_imgFurnitureDisplay;

      public void Init()
      {
         DrawBorder();
      }

      private void DrawBorder()
      {
         m_border = GetComponent<LineRenderer>();

         m_imgFurnitureDisplay = GetComponent<Image>();

         m_border.material = new Material(Shader.Find("Custom/DashedLine"));
         m_border.material.SetColor("_MainColor", Color.yellow);
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
   }
}


