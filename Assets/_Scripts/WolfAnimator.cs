using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class WolfAnimator : MonoBehaviour
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
        animator.SetFloat("X", GetComponent<wolfAI>().dir.x);
        animator.SetFloat("Y", GetComponent<wolfAI>().dir.y);
    }
}