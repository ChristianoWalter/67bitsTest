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
            // Calcula a nova posi��o baseada na posi��o anterior do jogador, apelidado de target.
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPreviousPosition, Time.deltaTime / delay);
            transform.position = newPosition;

            // Atualiza a posi��o anterior do alvo
            targetPreviousPosition = targetPosition;
        }
    }

    // M�todo chamado pelo Player para adicionar vari�veis que far�o o npc segu�-lo
    public void AttachToPlayer(Transform _target, Vector3 _targetPreviousPos, int _delayMultValue)
    {
        target = _target;
        targetPreviousPosition = _targetPreviousPos;
        delay *= _delayMultValue;
    }
}
