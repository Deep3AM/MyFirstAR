using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip enemyAudio;
    private AudioSource audioSource;
    public GameObject gameOver;
    public AnimationCurve curveGraph;
    private IEnumerator curEnemyCouroutine;
    public AudioClip gameOverAudio;
    int limitation = 30;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartEnemyCoroutine());
    }

    IEnumerator EnemyCoroutine()
    {
        for (int i = 0; i < limitation; i++)
        {
            audioSource.PlayOneShot(enemyAudio);
            audioSource.volume = 0.5f + (0.5f / 30) * i;
            yield return new WaitForSeconds(Mathf.Log(30 - i, 30));
        }
        gameOver.SetActive(true);
        audioSource.PlayOneShot(gameOverAudio);
    }

    IEnumerator StartEnemyCoroutine()
    {
        int rand = Random.Range(45, 60);
        yield return new WaitForSeconds(rand);
        curEnemyCouroutine = EnemyCoroutine();
        StartCoroutine(curEnemyCouroutine);
    }

    public void StopEnemyCoroutine(bool isGameWin)
    {
        if (curEnemyCouroutine != null)
        {
            StopCoroutine(curEnemyCouroutine);
            if (!isGameWin)
            {
                StartCoroutine(StartEnemyCoroutine());
            }
        }

        if (isGameWin)
        {
            StopAllCoroutines();
        }
    }
}
