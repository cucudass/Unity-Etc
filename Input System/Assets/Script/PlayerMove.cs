using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    CharacterController controller;
    Animator animator;
    Transform cam;

    [SerializeField] Vector2 inputVec;
    Vector3 moveVec;

    private void Awake() {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
    }

    void Update() {
        moveVec = cam.right * inputVec.x + cam.forward * inputVec.y;
        moveVec.y = 0;
        
        if(moveVec.magnitude > 0) { // Rotate
            Quaternion dirQuat = Quaternion.LookRotation(moveVec);
            Quaternion nextQuat = Quaternion.Slerp(transform.rotation, dirQuat, 0.3f);
            transform.rotation = nextQuat;
        }
    }

    private void FixedUpdate() {
        controller.SimpleMove(moveVec * speed * Time.deltaTime * 50); //Move
    }

    private void LateUpdate() {
        animator.SetFloat("Speed", moveVec.magnitude * speed);
    }

    
    void OnMove(InputValue value) { // Send Messages일 때...
        inputVec = value.Get<Vector2>(); //에셋에서 설정한 Value Type 그대로 Get 함수를 이용해 가져온다.
    }

    public void ActionMove(InputAction.CallbackContext context) { //Invoke Unity Events 일 때
        inputVec = context.ReadValue<Vector2>();
    }
}
