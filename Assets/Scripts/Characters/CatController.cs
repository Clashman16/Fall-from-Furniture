using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FFF.Characters
{
  public class CatController : MonoBehaviour
  {
    #region Data

    private CatData m_cat;

    public CatData Data
    {
      get => m_cat;
    }

    public void Init(int p_staminaValue)
    {
      m_cat = new CatData(p_staminaValue);
    }

    #endregion

    #region Animation

    private Animator m_animator;

    public void Walk()
    {
      if(m_animator != null)
      {
        m_animator.SetTrigger("TrWalk");
      }
    }

    public void Climb()
    {
      if(m_animator != null)
      {
        m_animator.SetTrigger("TrClimb");
      }
    }

    public void Fall()
    {
      if(m_animator != null)
      {
        m_animator.SetTrigger("TrFall");
      }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }
  }
}
