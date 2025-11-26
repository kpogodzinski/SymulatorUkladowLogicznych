using System.Collections.Generic;
using UnityEngine;

public class SevenSeg : Element
{
    [SerializeField]
    private GameObject segLight;
    [SerializeField]
    private GameObject segDot;

    private Vector3[] positions;
    private Vector3[] rotations;
    private Vector3[] scales;

    private List<GameObject> segments;

    private void Start()
    {
        positions = new Vector3[8];
        positions[0] = new Vector3(-0.044f, 1.678f);
        positions[1] = new Vector3(0.786f, 0.882f);
        positions[2] = new Vector3(0.64f, -0.784f);
        positions[3] = new Vector3(-0.336f, -1.616f);
        positions[4] = new Vector3(-1.18f, -0.798f);
        positions[5] = new Vector3(-1.028f, 0.846f);
        positions[6] = new Vector3(-0.204f, 0.028f);
        positions[7] = new Vector3(1.262f, -1.592f);

        rotations = new Vector3[8];
        rotations[0] = Vector3.zero;
        rotations[1] = 180 * Vector3.up + 95 * Vector3.forward;
        rotations[2] = 180 * Vector3.up + 95 * Vector3.forward;
        rotations[3] = 180 * Vector3.up;
        rotations[4] = 180 * Vector3.up + 95 * Vector3.forward;
        rotations[5] = 180 * Vector3.up + 95 * Vector3.forward;
        rotations[6] = Vector3.zero;
        rotations[7] = Vector3.zero;

        scales = new Vector3[8];
        scales[0] = Vector3.one;
        scales[1] = new Vector3(0.92f, 1, 1);
        scales[2] = new Vector3(0.92f, 1, 1);
        scales[3] = Vector3.one;
        scales[4] = new Vector3(0.9f, 1, 1);
        scales[5] = new Vector3(0.9f, 0.95f, 1);
        scales[6] = Vector3.one;
        scales[7] = 0.425f * Vector3.one;

        segments = new();
        for (int i = 0; i < inputCount; i++)
        {
            GameObject newLight = i == 7 ?
                Instantiate(segDot, gameObject.transform) : Instantiate(segLight, gameObject.transform);
            newLight.name = $"SEG_{i}";
            newLight.transform.localPosition -= i * new Vector3(0.08f, 0.825f);
            newLight.transform.localPosition = positions[i];
            newLight.transform.localEulerAngles = rotations[i];
            newLight.transform.localScale = 2 * scales[i];

            segments.Add(newLight);
        }
    }

    protected override void LateUpdate()
    {
        for (int i = 0; i < inputCount; i++)
        {
            segments[i].GetComponent<SpriteRenderer>().color = inputs[i] ? Color.white : new Color(0.25f, 0.25f, 0.25f);
            pins[i].GetComponent<SpriteRenderer>().color = inputs[i] ? Color.green : Color.red;
        }
    }
}