using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChan : MonoBehaviour
{

    private Animator animator;
    public enum EnemyChanBehavior
    {
        idle,
        charging,
        release
    }

    public EnemyChanBehavior enemyChanState;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyChanState = EnemyChanBehavior.idle;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
//        switch (enemyChanState)
//        {
//            case EnemyChanBehavior.charging:
//                animator.SetBool("charging", true);
//                break;
//            case EnemyChanBehavior.release:
//                animator.SetBool("release", true);
//                break;
//            default:
//                animator.SetBool("idle", true);
//                break;
//        }
//        
        
        
        
        
    }
}
