using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_enemyAI : MonoBehaviour, CS_IDamage
{

    [Header("----- Components -----")]
    [SerializeField] Renderer model;

    [Header("----- Enemy Stats -----")]
    [SerializeField] int HP;

    // Start is called before the first frame update
    void Start()
    {
        CS_gameManager.instance.updateGameGoal(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damageTaken)
    {
        HP -= damageTaken;
        StartCoroutine(flashColor());

        if (HP <= 0)
        {
            CS_gameManager.instance.updateGameGoal(-1);
            Destroy(gameObject);
        }
    }

    IEnumerator flashColor()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.white;
    }
}
