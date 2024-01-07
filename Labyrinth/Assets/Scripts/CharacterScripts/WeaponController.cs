using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject axe;
    public bool canAttack=true;
    public float attackCooldown = 1.0f;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(canAttack)
            {
                AxeAttack();
            }
        }
    }

    public void AxeAttack()
    {
        canAttack = false;
        Animator anim = axe.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }



    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

}
