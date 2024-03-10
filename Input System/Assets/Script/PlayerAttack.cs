using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] BoxCollider hitColl;
    Animator animator;
    WaitForSeconds wait;

    float curTime;
    float maxTime = 1f;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        wait = new WaitForSeconds(0.2f);
    }

    void Update()
    {
        curTime += Time.deltaTime;
    }

    void OnAttack(InputValue value) {
        if(curTime > maxTime) {
            curTime = 0;
        }
    }


    IEnumerator AttackRoutine() {
        Debug.Log("Attack");
        animator.SetTrigger("Attack");

        yield return wait;
        hitColl.enabled = true;

        yield return wait;
        hitColl.enabled = false;
    }

    public void ActionAttack(InputAction.CallbackContext context) {
        if (context.started) { //입력 시작
            Debug.Log("Input Start...");
        } else if(context.performed) { //입력 성공(인정)
            StartCoroutine(AttackRoutine());
        } else if(context.canceled) { //입력 취소
            Debug.Log("Input Cancel...");
        }
    }
}
