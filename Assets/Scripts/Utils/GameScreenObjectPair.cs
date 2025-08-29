using FFF.Behaviours.UI;
using System;
using UnityEngine;

namespace FFF.Utils
{
   [Serializable]
   public struct GameScreenObjectPair
   {
      [SerializeField] private EGameScreen m_key;

      public EGameScreen Key
      {
         get => m_key;
      }

      [SerializeField] private CanvasBehaviour m_value;

      public CanvasBehaviour Value
      {
         get => m_value;
      }
   }
}
