using FFF.Interactable;
using System.Collections.Generic;
using UnityEngine;

// TODO (A2) : Define Player class with int Stamina and int StaminaMax
using Player = UnityEngine.MonoBehaviour;
// TODO END

public static class FurnitureUtils
{
    // Get the probability of a Furniture
    public static float GetProbabilityOfFalling(Player p_player, List<DraggableFurnitureBehaviour> p_lstFurniture)
    {
        return GetProbabilityOfFallingV1(p_player, p_lstFurniture);
    }

    // Check if the cat succed to climb the furnitures
    // Return the index of the failing furniture, or -1 if succed
    public static int TryClimbing(Player p_player, List<DraggableFurnitureBehaviour> p_lstFurniture)
    {
        return TryClimbingV1(p_player, p_lstFurniture);
    }

    #region V1

    private static float GetProbabilityOfFallingV1(Player p_player, List<DraggableFurnitureBehaviour> p_lstFurniture)
    {
        Debug.Log("GetProbabilityOfFallingV1");

        /* ### RULE 1 ### */
        // You can't fall from the first furniture
        if (p_lstFurniture.Count < 2) { return 0f; }
        
        DraggableFurnitureBehaviour p_current = p_lstFurniture[0];
        DraggableFurnitureBehaviour p_previous = p_lstFurniture[1];

        int l_dCurrentStability = p_current.CurrentStability;

        /* ### RULE 2 ### */
        // If the stability is bigger than the previous one, we subtract 1 from the current one
        if (l_dCurrentStability > p_previous.CurrentStability)
        {
            l_dCurrentStability--;
        }

        // You fall if the stability is 0 or 1 (Dev's shortcut)
        if (l_dCurrentStability < 2) { return 1f; }

        float l_fProbabilityOfFalling = 0f;

        /* ### RULE 3 ### */
        // If player's stamina is lower than the stability (modified or not), we add stamina / staminaMax to the probability of falling

        // TODO (A2) : Define Player class with int Stamina and int StaminaMax
        /*if (p_player.Stamina < l_dCurrentStability)
        {
            l_fProbabilityOfFalling += (float)p_player.Stamina / (float)p_player.StaminaMax;
        }*/
        // TODO END

        /* ### RULE 4 ### */
        // If the (original) stability is equal to the previous one, we ignore the stability for the probability of falling
        if (p_current.CurrentStability == p_previous.CurrentStability) { return l_fProbabilityOfFalling; }


        /* ### RULE 5 ### */
        // If the stabilities are different we add 1 / stability to the probability of falling
        return l_fProbabilityOfFalling += 1f / l_dCurrentStability;
    }
    private static int TryClimbingV1(Player p_player, List<DraggableFurnitureBehaviour> p_lstFurniture)
    {
        for(int i = 0; i < p_lstFurniture.Count; i++)
        {
            float l_fProbability = p_lstFurniture[i].CurrentProbability;

            // Maybe store the random value somewhere in the furniture for a visual effect ?

            if(Random.value <= l_fProbability)
            {
                return i;
            }
        }

        return -1;
    }

    #endregion
}
