using FFF.ScriptableObjects;
using UnityEngine;

namespace FFF.Player
{
   public sealed class PlayerSaveSingleton
   {
      private static PlayerSaveSingleton m_instance = null;

      private const string m_prefPrefix = "FFF.";

      private ScriptableLevelListData m_levelListData;

      public static PlayerSaveSingleton Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new PlayerSaveSingleton();
            }
            return m_instance;
         }
      }

      private PlayerSaveSingleton()
      {

      }

      public int CurrentLevelId
      {
         get => PlayerPrefs.GetInt(string.Concat(m_prefPrefix, "CurrentLevelId"), 0);
         set => PlayerPrefs.SetInt(string.Concat(m_prefPrefix, "CurrentLevelId"), value);
      }

      public ScriptableFurnitureData[] CurentLevelData
      {
         get
         {
            if(m_levelListData == null)
            {
               m_levelListData = Resources.Load<ScriptableLevelListData>("ScriptableObjects/LevelList");
            }

            return m_levelListData.LevelList[CurrentLevelId].FurnitureList;
         }
      }

      public int TimerMax
      {
         get => PlayerPrefs.GetInt(string.Concat(m_prefPrefix, "TimerMax"), 5);
         set => PlayerPrefs.SetInt(string.Concat(m_prefPrefix, "TimerMax"), value);
      }

      public string LanguageId
      {
         get => PlayerPrefs.GetString(string.Concat(m_prefPrefix, "LanguageId"), "english");
         set => PlayerPrefs.SetString(string.Concat(m_prefPrefix, "LanguageId"), value);
      }

      public float BackgroundMusicVolume
      {
         get => PlayerPrefs.GetFloat(string.Concat(m_prefPrefix, "BackgroundMusicVolume"), 0.5f);
         set => PlayerPrefs.SetFloat(string.Concat(m_prefPrefix, "BackgroundMusicVolume"), value);
      }

      public float SFXVolume
      {
         get => PlayerPrefs.GetFloat(string.Concat(m_prefPrefix, "SFXVolume"), 0.5f);
         set => PlayerPrefs.SetFloat(string.Concat(m_prefPrefix, "SFXVolume"), value);
      }
   }
}
