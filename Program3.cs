using UnityEngine;

public class x: MonoBehaviour{
    public Animator animator;

    void Start(){
        if(animator==null){
            animator=GetComponent<animator>();
        }
    }

    public void PlayAnimation(){
        animator.SetTrigger("win");
    }
}