using UnityEngine;

public abstract class QuarkEventListener : MonoBehaviour
{
    public QuarkEvents EventSystem;
    public abstract void OnEvent(QuarkEvent e);
}
