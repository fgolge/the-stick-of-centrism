using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Animator childAnim;
    public Rigidbody rb;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(OnStateChangeHandler);
    }

    private void OnStateChangeHandler(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (currentState == GameManager.GameState.RUNNING && (previousState == GameManager.GameState.END || previousState == GameManager.GameState.PREGAME))
        {
            childAnim.SetTrigger("Default");
            rb.isKinematic = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("black_hole"))
        {
            UIManager.Instance.WinStatus = false;
            StartCoroutine(GameOver());
        }
        if (collision.gameObject.CompareTag("red_hole"))
        {
            UIManager.Instance.WinStatus = true;
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        SpawnManager.Instance.FreezeStick();

        rb.isKinematic = true;
        childAnim.SetTrigger("Fall");

        yield return new WaitForSeconds(2);

        GameManager.Instance.GameOver();
    }
}
