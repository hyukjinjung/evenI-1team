using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationItem : MonoBehaviour
{
    public TransformationType transformationType;
    public float duration;
    public int specialAbilityUses;

    public void ApplyTransformation(PlayerTransformationController controller)
    {
        controller.StartTransformation(this);
    }
}
