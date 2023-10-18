using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float launchPower = 500;
    [SerializeField] private float maxDragDistance = 3.5f;

    private Vector2 initialPosition;
    private bool birdWasLaunched;
    private float timeSittingAround;

    private LineRenderer lineRenderer;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        initialPosition = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lineRenderer.SetPosition(1, initialPosition);
        lineRenderer.SetPosition(0, transform.position);

        if (birdWasLaunched &&
            GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            timeSittingAround += Time.deltaTime;
        }

        if (transform.position.y > 15 ||
            transform.position.y < -15 ||
            transform.position.x > 15 ||
            transform.position.x < -15 ||
            timeSittingAround > 2)
        {
            String currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // coroutine ResetAfterDelay()
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(1);
        rb2d.position = initialPosition;
        rb2d.velocity = Vector2.zero;
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        if (transform.position.x < -5.00)
        {
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

        if (transform.position.x < -5.00)
        {
            Vector2 directionToInitialPosition = initialPosition - new Vector2(transform.position.x, transform.position.y);

            GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * launchPower);
            GetComponent<Rigidbody2D>().gravityScale = 1;
            birdWasLaunched = true;
        }

        lineRenderer.enabled = false;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = new Vector2(mousePosition.x, mousePosition.y);

        float distance = Vector2.Distance(desiredPosition, initialPosition);

        if (distance > maxDragDistance)
        {
            Vector2 direction = desiredPosition - initialPosition;
            direction.Normalize();
            desiredPosition = initialPosition + (direction * maxDragDistance);
        }

        if (desiredPosition.x > initialPosition.x)
        {
            desiredPosition.x = initialPosition.x;
        }

        rb2d.position = desiredPosition;
    }
}