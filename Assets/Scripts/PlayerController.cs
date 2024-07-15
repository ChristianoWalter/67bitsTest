using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : CharacterScript
{
    public static PlayerController instance;

    // Direção de movimento via input do personagem
    Vector2 direction;
    [Header("Movement Stats")]
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject visual;
    bool isPunching;

    // Variáveis para controle dos npcs adicionados às costas do player
    [Header("NPCs on back Controllers")]
    public List<GameObject> npcsInTheBack;
    public int maxNpcsInBack;
    public Transform jointPoint;
    bool isSelling;
    

    private void Awake()
    {
        instance = this;
        SwitchPhysics();
    }

    // Update is called once per frame
    void Update()
    {
        if(!canMove) return;

        rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.y * speed);
        anim.SetBool("IsMoving", direction != Vector2.zero);

        if (direction != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y), Vector3.up);
            visual.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed);
        }
    }

    //método de detecção da entrada de colisão para ataque e colheita de objetos
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC" && !isPunching)
        {
            if (other.gameObject.GetComponent<Animator>().enabled == false)
            {
                if(!npcsInTheBack.Contains(other.gameObject) && npcsInTheBack.Count < maxNpcsInBack)
                {
                    AddObjectOnBack(other.gameObject);
                }
            }
            else 
            { 
                if(!npcsInTheBack.Contains(other.gameObject)) StartCoroutine(AttackSequence(other.gameObject));
            }
        }     
    }

    //método de detecção continua de colisão para venda de objetos
    private void OnTriggerStay(Collider other)
    {      
        if (other.gameObject.tag == "MoneyPlatform" && !isSelling)
        {
            isSelling = true;
            StartCoroutine(SellBackObjects());
        }
    }

    //método para aprimoramento de carga nas costas do personagem
    public void IncrementMaxNpcsInBack(int _valueToIncrement)
    {
        maxNpcsInBack += _valueToIncrement;
    }

    //rotina para venda de objetos nas costas do personagem
    public IEnumerator SellBackObjects()
    {
        yield return new WaitForSeconds(1f);
        if (npcsInTheBack.Count > 0)
        {
            GameController.instace.SellObjects();
            GameObject _npc = npcsInTheBack[npcsInTheBack.Count - 1];
            npcsInTheBack.RemoveAt(npcsInTheBack.Count - 1);
            yield return new WaitForSeconds(.1f);
            Destroy(_npc);
            jointPoint.position = new Vector3(jointPoint.position.x, jointPoint.position.y - 1f, jointPoint.position.z);
            if (npcsInTheBack.Count == 0)
            {
                anim.SetBool("IsCarrying", false);
            }
        }
        isSelling = false;
    }

    //rotina para sequência de ataque e movimentação do player
    public IEnumerator AttackSequence(GameObject npc)
    {
        isPunching = true;
        anim.SetTrigger("Punching");
        canMove = false;
        yield return new WaitForSeconds(.5f);
        canMove = true;
        npc.GetComponent<Animator>().enabled = false;
        npc.GetComponent<CapsuleCollider>().isTrigger = true;
        isPunching = false;
    }

    //método para adicionar objetos às costas do personagem
    public void AddObjectOnBack(GameObject _npc)
    {
        npcsInTheBack.Add(_npc);
        _npc.GetComponent<Animator>().enabled = true;
        _npc.GetComponent<Animator>().SetTrigger("BeingCarry");
        _npc.GetComponent<InercyController>().AttachToPlayer(jointPoint, jointPoint.position, npcsInTheBack.Count);
        _npc.transform.position = jointPoint.position;
        jointPoint.position = new Vector3(jointPoint.position.x, jointPoint.position.y + 1f, jointPoint.position.z);
        anim.SetBool("IsCarrying", true);
    }

    //método de recepção de input para movimentação
    public void MovementAction(InputAction.CallbackContext value)
    {
        direction = value.ReadValue<Vector2>();
    }
}
