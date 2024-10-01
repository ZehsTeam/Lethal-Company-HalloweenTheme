using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.MonoBehaviours;

public class AnimatedItemReskin : ItemReskin
{
    [Space(20f)]
    [Header("Animated")]
    [Space(5f)]
    public Animator Animator;

    public override void LateStart()
    {
        base.LateStart();

        SetAnimator();
    }

    private void SetAnimator()
    {
        if (GrabbableObject == null) return;
        if (Animator == null) return;

        AnimatedItem animatedItem = GrabbableObject as AnimatedItem;
        if (animatedItem == null) return;

        animatedItem.itemAnimator = Animator;
    }
}
