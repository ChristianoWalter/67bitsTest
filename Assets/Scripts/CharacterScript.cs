using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{
    // Componentes para controle de movimentação/animação
    [Header("Character components")]
    public CapsuleCollider characterCollider;
    public Rigidbody rb;
    public Animator anim;
    public bool canMove;
    [SerializeField] bool isRegdollActive;

    // Componentes para controle do ragdoll
    [Header("Ragdoll components")]
    public SphereCollider headCol;
    public BoxCollider spineCol, bodyCol;
    public CapsuleCollider leftUpLegCol, rightUpLegCol, leftLegCol, rightLegCol, leftArmCol, rightArmCol,
        leftMidArmCol, rightMidArmCol;

    // Método para trocar entre modo de animação e ragdoll
    public void SwitchPhysics()
    {
        if (isRegdollActive)
        {
            rb.isKinematic = false;
            characterCollider.isTrigger = false;
            anim.enabled = true;
            canMove = true;

            //região destinada às mudanças dos ragdolls
            #region Ragdoll physics change
            leftUpLegCol.isTrigger = true;
            rightUpLegCol.isTrigger = true;
            leftLegCol.isTrigger = true;
            rightLegCol.isTrigger = true;
            leftArmCol.isTrigger = true;
            rightArmCol.isTrigger = true;
            leftMidArmCol.isTrigger = true;
            rightMidArmCol.isTrigger = true;
            spineCol.isTrigger = true;
            headCol.isTrigger = true;
            bodyCol.isTrigger = true;
            #endregion

            isRegdollActive = false;
        }
        else
        {
            rb.isKinematic = true;
            characterCollider.isTrigger = true;
            anim.enabled = false;
            canMove = false;
            rb.velocity = Vector3.zero;

            //região destinada às mudanças dos ragdolls
            #region Ragdoll physics change
            leftUpLegCol.isTrigger = false;
            rightUpLegCol.isTrigger = false;
            leftUpLegCol.isTrigger = false;
            rightUpLegCol.isTrigger = false;
            leftArmCol.isTrigger = false;
            rightArmCol.isTrigger = false;
            leftMidArmCol.isTrigger = false;
            rightMidArmCol.isTrigger = false;
            spineCol.isTrigger = false;
            headCol.isTrigger = false;
            bodyCol.isTrigger = false;
            #endregion

            isRegdollActive = true;
        }
    }

}
