using FFF.Translations;
using System.Collections.Generic;
using UnityEngine;

namespace FFF.ScriptableObjects
{
   [CreateAssetMenu(menuName = "ScriptableObjects/LanguagesDatabase")]
   public class ScriptableLanguageDatabase : ScriptableObject
   {
      public List<TranslationEntry> m_lstFrenchInit;

      public List<TranslationEntry> m_lstEnglishInit;

      private Dictionary<string, string> m_lstFrench = new();

      public Dictionary<string, string> French
      {
         get => m_lstFrench;
      }

      private Dictionary<string, string> m_lstEnglish = new();

      public Dictionary<string, string> English
      {
         get => m_lstEnglish;
      }
   }
}
