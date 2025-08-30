using FFF.Characters;
using FFF.Behaviours.UI;
using System.Collections.Generic;
using UnityEngine;

namespace FFF.Utils
{
   public static class FurnitureUtils
   {
      private const float m_dMaxStability = 5;

      private const float m_dMinStability = 1;

      // Get the probability of a Furniture
      public static float GetProbabilityOfFalling(CatBehaviour p_cat, List<FurnitureDropSlotBehaviour> p_lstFurniture)
      {
         return GetProbabilityOfFallingV1(p_cat, p_lstFurniture);
      }

      // Check if the cat succed to climb the furnitures
      // Return the index of the failing furniture, or -1 if succed
      public static int TryClimbing(List<FurnitureDropSlotBehaviour> p_lstFurniture)
      {
         return TryClimbingV1(p_lstFurniture);
      }

      #region V1

      private static float GetProbabilityOfFallingV1(CatBehaviour p_cat, List<FurnitureDropSlotBehaviour> p_lstFurniture)
      {
         // No fall with the first furniture
         if (p_lstFurniture == null || p_lstFurniture.Count < 2)
         {
            return 0f;
         }

         FurnitureDropSlotBehaviour l_previousSlot = p_lstFurniture[0];
         FurnitureDropSlotBehaviour l_currentSlot = p_lstFurniture[1];

         int l_dPreviousStability = Mathf.Clamp(l_previousSlot.CurrentStability, 1, 5);
         int l_dCurrentStability = Mathf.Clamp(l_currentSlot.CurrentStability, 1, 5);

         int l_dPreviousEffectiveStability = l_dPreviousStability;
         int l_dCurrentEffectiveStability = (l_dPreviousEffectiveStability < l_dCurrentStability) ? Mathf.Max(1, l_dCurrentStability - 1) : l_dCurrentStability;

         // No fall with the same 2 consecutive stabilities
         if (l_dCurrentEffectiveStability == l_dPreviousEffectiveStability)
         {
            return 0f;
         }

         int l_dStamina = Mathf.Max(0, p_cat.Data.Stamina);
         int l_dStaminaMax = Mathf.Max(1, p_cat.Data.StaminaMax);

         // Probability of falling
         float l_probability = (m_dMaxStability - (float)l_dCurrentEffectiveStability) / (float) (m_dMaxStability - m_dMinStability);

         if (l_dStamina < l_dCurrentEffectiveStability)
         {
            l_probability += (float)l_dStamina / (float)l_dStaminaMax;
         }

         return Mathf.Clamp01(l_probability);
      }
      private static int TryClimbingV1(List<FurnitureDropSlotBehaviour> p_lstFurniture)
      {
         for (int i = 0; i < p_lstFurniture.Count; i++)
         {
            float l_fProbability = p_lstFurniture[i].CurrentProbability;

            // Maybe store the random value somewhere in the furniture for a visual effect ?

            if (Random.value <= l_fProbability)
            {
               return i;
            }
         }

         return -1;
      }

      #endregion
   }
}

