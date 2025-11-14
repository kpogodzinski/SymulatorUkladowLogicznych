using UnityEngine;

public class Matrix : Element
{
    [SerializeField]
    private GameObject ledLight;

    private bool[] rows;
    private bool[] cols;

    private float ledOffset;

    private new void Awake()
    {
        base.Awake();

        rows = new bool[6];
        cols = new bool[6];

        ledOffset = 1.065f;
    }

    private void OnMatrixChanged()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (rows[i] && cols[j])
                {
                    if (GameObject.Find($"LED{i}{j}") == null)
                    {
                        GameObject newLight = Instantiate(ledLight, transform);
                        newLight.name = $"LED{i}{j}";
                        newLight.transform.localPosition += new Vector3(j * ledOffset, i * -ledOffset);
                    }
                }
                else
                {
                    if (GameObject.Find($"LED{i}{j}") != null)
                        Destroy(GameObject.Find($"LED{i}{j}"));
                }
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < inputCount;  i++)
        {
            if (i < 6)
            {
                if (rows[i] != inputs[i])
                {
                    rows[i] = inputs[i];
                    OnMatrixChanged();
                }
            }
            else
            {
                if (cols[i - 6] != inputs[i])
                {
                    cols[i - 6] = inputs[i];
                    OnMatrixChanged();
                }
            }
        }

        for (int i = 0; i < inputCount; i++)
        {
            pins[i].GetComponent<SpriteRenderer>().color = inputs[i] ? Color.green : Color.red;
        }
    }
}
