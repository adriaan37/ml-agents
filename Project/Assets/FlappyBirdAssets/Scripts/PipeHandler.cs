using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeHandler : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Pipe pipePrefab = null;

    [Header("Settings")]
    [SerializeField] private float gapSize = 4f;
    [SerializeField]private float secondsBetweenSpawns = 2f;

    [Header("Fields")]
    [SerializeField] private float stageEnd = -15f;
    [SerializeField] private float pipeHigh = 4f;
    [SerializeField] private float pipeLow = -1.5f;

    private float spawntimer;
    private readonly List<Pipe> pipes = new List<Pipe>();

    // Update is called once per frame
    private void Update()
    {   
        RemoveOldPipes();

        SpawnNewPipes();
    }

    public void ResetPipes()
        {
            foreach(var pipe in pipes)
            {
                Destroy(pipe.gameObject);
            }
            pipes.Clear();
            spawntimer = 0f;
        }

        private void RemoveOldPipes()
        {
            for (int i = pipes.Count-1; i > 0; i--)
            {
                if(pipes[i].transform.position.x < stageEnd)
                {
                    Destroy(pipes[i].gameObject);
                    pipes.RemoveAt(i);
                }
            }
        }

        private void SpawnNewPipes()
        {
            spawntimer -= Time.deltaTime;

            if(spawntimer > 0f){return;}
            Pipe topPipe = Instantiate(pipePrefab,transform.position,Quaternion.Euler(0f,0f,180f));
            Pipe bottomPipe = Instantiate(pipePrefab,transform.position,Quaternion.identity);

            float centerHeight = UnityEngine.Random.Range(pipeLow,pipeHigh);

            topPipe.transform.Translate(Vector3.up *(centerHeight+(gapSize/2)),Space.World);
            bottomPipe.transform.Translate(Vector3.up *(centerHeight-(gapSize/2)),Space.World);

            pipes.Add(topPipe);
            pipes.Add(bottomPipe);

            spawntimer = secondsBetweenSpawns;

        }
}
