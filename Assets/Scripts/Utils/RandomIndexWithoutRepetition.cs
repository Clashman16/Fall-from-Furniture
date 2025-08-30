using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIndexWithoutRepetition
{
  private readonly int m_dLastIndex;
  private int m_dPreviousRandomIndex = -1;
  public int Next
  {
    get
    {
      // We pick a random index between the first and the second to last
      int l_fNewRandomIndex = Random.Range(0, m_dLastIndex - 1);

      // If we picked the previous one or any index after it, we shift the value (+1)
      if (l_fNewRandomIndex >= m_dPreviousRandomIndex)
      {
        l_fNewRandomIndex++;
        // If we reached the last index, the value goes back to 0
        if (l_fNewRandomIndex > m_dLastIndex)
        {
          l_fNewRandomIndex = 0;
        }
      }

      m_dPreviousRandomIndex = l_fNewRandomIndex;
      return l_fNewRandomIndex;
    }
  }

  public RandomIndexWithoutRepetition(int p_dArrayLength)
  {
    m_dLastIndex = p_dArrayLength - 1;

    // Previous index randomly picked from all
    m_dPreviousRandomIndex = Random.Range(0, m_dLastIndex);
  }
}
