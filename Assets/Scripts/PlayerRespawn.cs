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
        if (SpawnPoint.currentCheckpoint != null)
        {
            //Move player to last checkpoint position, if there is a current checkpoint set.
            gameObject.transform.position =
                SpawnPoint.currentCheckpoint.transform.position;
        }
        else
        {
            //If there is no current checkpoint, reload the level.
            StartCoroutine(RestartLevel());
        }
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(respawnTimer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
