using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField]
    private float respawnTimer = 5f;

    public void Respawn()
    {
            StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(respawnTimer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
