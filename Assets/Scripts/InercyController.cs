using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InercyController : MonoBehaviour
{
    // Variáveis para controle do efeito de inércia
    [Header("Inercy Variables")]
    Transform target;
    Vector3 targetPreviousPosition;
    public float delay;

    // Componentes para controle do ragdoll
    [Header("Ragdoll components")]
    public SphereCollider headCol;
    public BoxCollider spineCol, bodyCol;
    public CapsuleCollider leftUpLegCol, rightUpLegCol, leftLegCol, rightLegCol, leftArmCol, rightArmCol,
        leftMidArmCol, rightMidArmCol;

    private void FixedUpdate()
    {
        if (target != null)
        {
            // Calcula a nova posição baseada na posição anterior do jogador, apelidado de target.
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPreviousPosition, Time.deltaTime / delay);
            transform.position = newPosition;

            // Atualiza a posição anterior do alvo
            targetPreviousPosition = targetPosition;
        }
    }

    // Método chamado pelo Player para adicionar variáveis que farão o npc seguí-lo
    public void AttachToPlayer(Transform _target, Vector3 _targetPreviousPos, int _delayMultValue)
    {
        target = _target;
        targetPreviousPosition = _targetPreviousPos;
        delay *= _delayMultValue;

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
    }
}
