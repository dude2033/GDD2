using UnityEngine;

public class BlinkRandomizer : MonoBehaviour
{
    public Vector2 minMaxBlinkDelay = new Vector2(3, 5);

    private Animator animator;
    private static readonly int Blink1 = Animator.StringToHash("Blink");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Blink();
    }
    
    [ContextMenu("Trigger Blink")]
    private void Blink()
    {
        animator.SetTrigger(Blink1);
        float delay = Random.Range(minMaxBlinkDelay.x, minMaxBlinkDelay.y);
        Invoke(nameof(Blink), delay);
    }
}
