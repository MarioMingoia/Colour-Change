using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public CharacterController charController;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private AnimationCurve jumpFalloff;

    private float originaljump;

    private bool isJumping;
    public void Start()
    {
        charController = GetComponent<CharacterController>();

        originaljump = jumpMultiplier;
    }

    public void Update()
    {
        if (Input.GetKeyDown(jumpKey) && isJumping == false)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }     

    IEnumerator JumpEvent()
    {
        float timeInAir = 0.0f;
        do
        {
            //Jump Logic (for one step of the jump)
            float jumpForce = jumpFalloff.Evaluate(timeInAir);
            Vector3 jumpDirection = Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime;
            charController.Move(jumpDirection);
            timeInAir += Time.deltaTime;

            yield return null;

        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        isJumping = false;
    }
}