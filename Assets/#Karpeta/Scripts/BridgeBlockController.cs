using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBlockController : MonoBehaviour
{
    [Header("Configuración del puente")]
    public float blockRiseHeight = 2f;         // Altura que suben los bloques
    public float blockSpeed = 1f;              // Velocidad de subida/bajada
    public float timeBetweenBlocks = 1f;       // Tiempo entre bloques

    [Header("Opciones")]
    public bool autoStart = false;             // ¿Se activa automáticamente?

    private List<Transform> blocks = new List<Transform>();
    private Queue<int> activeBlocks = new Queue<int>();
    private bool isRunning = false;

    private void Awake()
    {
        // Guarda los bloques hijos
        foreach (Transform child in transform)
        {
            blocks.Add(child);
        }
    }

    private void Start()
    {
        if (autoStart)
        {
            ActivateBridge();
        }
    }

    // Este método lo puede llamar un botón externo
    public void ActivateBridge()
    {
        if (!isRunning)
        {
            isRunning = true;
            StartCoroutine(AnimateBridge());
        }
    }

    private IEnumerator AnimateBridge()
    {
        int currentIndex = 0;

        while (true)
        {
            Transform currentBlock = blocks[currentIndex];

            // Subir el nuevo bloque
            StartCoroutine(MoveBlock(currentBlock, Vector3.up * blockRiseHeight));
            activeBlocks.Enqueue(currentIndex);

            // Si ya hay más de 2 bloques activos, hundir el más antiguo
            if (activeBlocks.Count > 2)
            {
                int indexToSink = activeBlocks.Dequeue();
                Transform blockToSink = blocks[indexToSink];
                StartCoroutine(MoveBlock(blockToSink, Vector3.down * blockRiseHeight));
            }

            yield return new WaitForSeconds(timeBetweenBlocks);
            currentIndex = (currentIndex + 1) % blocks.Count;
        }
    }

    private IEnumerator MoveBlock(Transform block, Vector3 offset)
    {
        Vector3 startPos = block.position;
        Vector3 endPos = startPos + offset;
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            block.position = Vector3.Lerp(startPos, endPos, elapsed);
            elapsed += Time.deltaTime * blockSpeed;
            yield return null;
        }

        block.position = endPos;
    }
}
