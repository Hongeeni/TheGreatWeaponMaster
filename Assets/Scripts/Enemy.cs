using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    public GameObject monsterHealth;

    private Animator animator;
    private Image monsterHealthBarImage;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        monsterHealthBarImage = monsterHealth.transform.GetChild(1).GetComponent<Image>();
    }

    public void TakeDamage(float damageAmount)
    {
        monsterHealthBarImage.fillAmount -= damageAmount;
        var go = Instantiate(FloatingTextPrefab ,transform.position, Quaternion.identity, transform);
        float damageAmountText = damageAmount*100f;
        go.GetComponent<TextMesh>().text = damageAmountText.ToString();
        if(monsterHealthBarImage.fillAmount <= 0)
        {
            animator.SetTrigger("die");
            monsterHealth.SetActive(false);

            if (this.transform.tag == "Boss")
            {
                BossBasicAttack.bossDead = true;
            }
            else if (this.transform.tag == "MiddleBoss")
            {
                ClearStage_Script.killMiddleBoss = true;
            }
            else
            {
                ClearStage_Script.killEnemyCount++;
            }
            StartCoroutine(DestroyEnemy());
        }
        else
        {
            animator.SetTrigger("damage");
        }
    }

    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(3.0f);
        this.gameObject.SetActive(false);
    }
}
