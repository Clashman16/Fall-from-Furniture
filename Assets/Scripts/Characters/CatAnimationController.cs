using UnityEngine;

namespace FFF.Characters
{
  public class CatAnimationController : MonoBehaviour
  {
    private Animator m_animator;

    public void TriggerWalk()
    {
      // Avoid forced reset when cat is reset before ending states
      m_animator.ResetTrigger("TrReset");
      TriggerAnimation("TrWalk");
    }

    public void TriggerClimb()
    {
      TriggerAnimation("TrClimb");
    }

    public void TriggerFall()
    {
      TriggerAnimation("TrFall");
    }

    public void TriggerWin()
    {
      TriggerAnimation("TrWin");
    }

    public void TriggerReset()
    {
      TriggerAnimation("TrReset");
    }

    private void TriggerAnimation(string p_animationName)
    {
      if (m_animator != null)
      {
        m_animator.SetTrigger(p_animationName);
      }
    }

    // Start is called before the first frame update
    void Start()
    {
      m_animator = GetComponent<Animator>();
    }
  }
}
