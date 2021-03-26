using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// test git
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Hurt();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                StartCoroutine(HurtEnemy(collision.gameObject.GetComponent<Enemy>()));
                break;

            default:
                break;
        }
    }

    IEnumerator HurtEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        yield return new WaitForEndOfFrame();
    }
    void Hurt()
    {
        Destroy(this.gameObject);
    }
}
