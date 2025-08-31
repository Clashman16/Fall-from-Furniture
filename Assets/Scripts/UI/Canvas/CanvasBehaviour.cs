using UnityEngine;

namespace FFF.Behaviours.UI
{
   public abstract class CanvasBehaviour : MonoBehaviour
   {
      private Translator m_translator;

      public bool HasTranslated
      {
         get
         {
            if (m_translator == null)
            {
               m_translator = new Translator();
            }
      
            return m_translator.HasTranslated;
         }
      }

      public virtual void ResetData()
      {
         if(!HasTranslated)
         {
            TranslateCanvas();
         }
      }

      public void TranslateCanvas()
      {
         if(m_translator == null)
         {
            m_translator = new Translator();
         }
         
         m_translator.TranslateScreen(gameObject);
      }
   }
}
