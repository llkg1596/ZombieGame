using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : Control
{
    public Joystick move_joystick;
    public Joystick shoot_joystick;
    private Animator playerAnimator;
    private PlayerData playerData;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerData = GetComponent<PlayerData>();
    }

    void Update()
    {
        InputControl();
    }

    void InputControl()
    {
        if (move_joystick.Horizontal != 0 && move_joystick.Vertical != 0)
        {
            playerAnimator.SetFloat("Move", 1);
            Move(new Vector3(move_joystick.Horizontal, 0, move_joystick.Vertical));
        }
        else playerAnimator.SetFloat("Move", 0);

        if (shoot_joystick.Horizontal != 0 && shoot_joystick.Vertical != 0)
        {
            Vector3 vec = new Vector3(shoot_joystick.Horizontal, 0, shoot_joystick.Vertical);


            playerData.Main_Weapon.Shoot(vec);
            transform.rotation = Quaternion.LookRotation(vec, Vector3.up);
        }

    }

    public Transform gunPivot; // 총 배치의 기준점
    public Transform leftHandMount; // 총의 왼쪽 손잡이, 왼손이 위치할 지점
    public Transform rightHandMount; // 총의 오른쪽 손잡이, 오른손이 위치할 지점

    private void OnAnimatorIK(int layerIndex)
    {

        // 총의 기준점 (gunPivot) 을 모델의 오른쪽 팔꿈치로 이동한다
        gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        // IK를 사용하여 왼손의 위치와 회전을 총의 왼쪽 손잡이에 맞춘다. 
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

        // IK를 사용하여 오른손의 위치와 회전을 총의 오른쪽 손잡이에 맞춘다.
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    }

}

