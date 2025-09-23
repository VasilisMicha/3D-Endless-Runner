using UnityEngine;
using System.Collections;


public class SegmentGenerator : MonoBehaviour
{
    public GameObject[] segment;
    [SerializeField] float zPos = 86f;
    [SerializeField] bool creatingSegment = false;
    [SerializeField] int segmentNum;

    void Update()
    {
        if (!creatingSegment)
        {
            creatingSegment = true;
            StartCoroutine(SegmentGen());
        }
    }

    IEnumerator SegmentGen()
    {
        segmentNum = Random.Range(0, segment.Length);
        yield return new WaitForSeconds(6);
        Instantiate(segment[segmentNum], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 86f;
        yield return new WaitForSeconds(7);
        creatingSegment = false;
    }
}
