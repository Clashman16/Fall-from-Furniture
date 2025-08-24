using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureUtils : MonoBehaviour
{
    // Get the probability of a Furniture
    public float GetProbabilityOfFalling(Furniture p_current, Furniture p_previous, Player p_player)
    {
        return GetProbabilityOfFallingV1(p_current, p_previous, p_player);
    }

    // ########## V1 ##########

    float GetProbabilityOfFallingV1(Furniture p_current, Furniture p_previous, Player p_player)
    {
        // You can't fall from the first furniture
        // You can't fall if the stability of the furniture equal to the previous one
        if (p_previous == null || p_current.GetStability() == p_previous.GetStability()) { return 0f ; }


        int l_dCurrentStability = p_current.GetStability();

        // If the stability is lower than the previous one, we subtract 1 from the current one
        if (l_dCurrentStability > p_previous.GetStability()) {
            l_dCurrentStability--;
        }

        // You fall if the stability is 0 or 1
        if (l_dCurrentStability < 2) { return 1f; }


        float l_probabilityOfFalling = 1f / l_dCurrentStability;

        // If player's stamina is lower than the stability, we add stamina / staminaMax to the probability of falling
        if (p_player.GetStamina() < l_dCurrentStability)
        {
            l_probabilityOfFalling += (float) p_player.GetStamina() / (float) p_player.GetStaminaMax();
        }

        return l_probabilityOfFalling;
    }
}
