using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] public GameObject axe;
    [SerializeField] public bool canAttack = true;
    [SerializeField] public float attackCooldown = 1.0f;
    [SerializeField] public int damage = 40;
    public bool isAttacking = false;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canAttack)
            {
                WeaponAnimAttack();
                WeaponDamageAttack();
                AudioManager.instance.Play("AxeHit");
            }
        }
    }

    private void WeaponAnimAttack()
    {
        isAttacking = true;
        canAttack = false;
        Animator anim = axe.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());


    }

    private void WeaponDamageAttack()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 4f))
        {
            if (hit.collider.GetComponent<Enemy>())
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(damage, transform.root.gameObject);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        WeaponDamageAttack();
    }


    IEnumerator ResetAttackCooldown()
    {
        ResetAttackBool();
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }

}


