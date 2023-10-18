using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour
{
   [SerializeField] private new ParticleSystem particleSystem;
   [SerializeField] private Sprite deadSprite;
   bool hasDied;

   private void OnCollisionEnter2D(Collision2D collision)
   {
      if (ShouldDieFromCollision(collision))
      {
         StartCoroutine(Die());
      }

      /* Enemy enemy = collision.collider.GetComponent<Enemy>();

       if (enemy != null)
       {
          return;
       }

       if (collision.contacts[0].normal.y < -0.5)
       {
          Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
          Destroy(gameObject);
       }
 */
   }

   private IEnumerator Die()
   {
      hasDied = true;
      GetComponent<SpriteRenderer>().sprite = deadSprite;
      particleSystem.Play();
      yield return new WaitForSeconds(1);

      Destroy(gameObject);
   }

   private bool ShouldDieFromCollision(Collision2D collision)
   {
      if (hasDied)
      {
         return false;
      }

      Bird bird = collision.gameObject.GetComponent<Bird>();

      if (bird != null)
      {
         //Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
         //Destroy(gameObject);
         return true;
      }

      if (collision.contacts[0].normal.y < -0.5)
      {
         return true;
      }

      return false;
   }
}