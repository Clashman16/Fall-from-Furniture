using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

    #region Walk
    
    private bool m_bIsWalking;
    public bool IsWalking
    {
      get => m_bIsWalking;
      set
      {
        m_bIsWalking = value;
        if(m_animator != null)
        {
          m_animator.SetBool("IsWalking", value);
        }
      }
    }

    public float m_speed = 3.0f;

    public GameObject m_target;

    private Vector2 m_targetPosition;
    private Vector2 TargetPosition
    {
      get
      {
        if(m_targetPosition.Equals(Vector2.zero))
        {
          m_targetPosition = new Vector2(m_target.transform.position.x - 1.5f, transform.position.y);
        }
        return m_targetPosition;
      }
    }

    private void Walk()
    {
        // Move our position a step closer to the target.
        var step =  m_speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector2.MoveTowards(transform.position, TargetPosition, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector2.Distance(transform.position,TargetPosition) < 0.001f)
        {
          // Swap the position of the cylinder.
            IsWalking = false;
        }
    }

    #endregion

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

    void Update()
    {
      if(m_bIsWalking)
      {
        Walk();
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
