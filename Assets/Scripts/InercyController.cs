using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InercyController : MonoBehaviour
{
    Transform target;
    Vector3 targetPreviousPosition;
    public float delay;

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
    }
}
