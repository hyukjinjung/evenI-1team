using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformationController : MonoBehaviour
{
    private ITransformation currentState;
    public PlayerAnimationController AnimationController {  get; private set; }



    void Start()
    {
        currentState = new NormalState(this);
        AnimationController = GetComponent<PlayerAnimationController>();
    }



    public void ChangeState(ITransformation newState)
    {
        currentState = newState;
        currentState.Activate();
    }

    

    public void StartTransformation(TransformationItem item)
    {
        ITransformation newTransformation = new TransformationState(this, item.transformationType,
            item.duration, item.specialAbilityUses);

        ChangeState(newTransformation);
    }


    public void UseSpecialAbility()
    {
        currentState.UseSpecialAbility();
    }


    public void StartTransformationTimer(ITransformation transformation)
    {
        StartCoroutine(TransformationTimer(transformation));
    }


    private IEnumerator TransformationTimer(ITransformation transformation)
    {
        yield return new WaitForSeconds(5f);
        transformation.Deactivate();
    }
}
