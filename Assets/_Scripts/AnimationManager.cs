using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class AnimationManager : SingletonMonoBehaviour<AnimationManager>
{
    public Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!MovementController.instance.freez)
        {
            if (!MovementController.instance.moving)
                animator.speed = 0;
            else
            {
                animator.speed = 1;
                animator.SetFloat("X", MovementController.instance.inputDirection.x);
                animator.SetFloat("Y", MovementController.instance.inputDirection.y);
                animator.SetFloat("MAG", MovementController.instance.inputDirection.magnitude);
            }
        }
        else if (PlayerAIMovement.instance.moving)
        {
            animator.speed = 1;
            animator.SetFloat("X", PlayerAIMovement.instance.dir.x);
            animator.SetFloat("Y", PlayerAIMovement.instance.dir.y);
            animator.SetFloat("MAG", PlayerAIMovement.instance.dir.magnitude);
        }
        else animator.speed = 0;
    }
}