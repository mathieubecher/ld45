using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text : MonoBehaviour
{
    private float time = 0;
    [SerializeField] private AnimationCurve curve;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 0.5f) time += Time.deltaTime;
        else Destroy(this.gameObject);
        transform.localPosition = new Vector3(transform.localPosition.x, curve.Evaluate(time*2));
        GetComponent<TextMeshPro>().color = new Color(1,1,1,1 - curve.Evaluate(time * 2));
    }
}
