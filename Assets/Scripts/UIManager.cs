using UnityEngine;

public class UIManager: MonoBehaviour
{
    public CanvasGroup idleReticle;
    //public CanvasGroup grabReticle;



    public void ToggleIdleReticle(bool state)
    {
        idleReticle.alpha = state ? 0 : 1;
    }
    
    public void ToggleGrabReticle(bool state)
    {
        idleReticle.alpha = state ? 0 : 1;
    }
}