using System.Collections;
using UnityEngine;

public class CookerBehaviour : MonoBehaviour
{
    public CookerType type;
    public enum CookerType { Bake, Boil, Fry }
    [SerializeField] private float interval = 0.3f; // U“®‚ÌŠÔŠu
    [SerializeField] private float moveDistance = 0.1f; // U“®‚Ì‹——£
    [SerializeField] private GameObject cookerImage;
    private void Start()
    {
        StartCoroutine(CockerMove(interval));
    }
    IEnumerator CockerMove(float interval) // ˆê’èŠÔŠu‚ÅU“®‚·‚éƒRƒ‹[ƒ`ƒ“
    {
        while (true)
        {
            cookerImage.transform.position += new Vector3(0, moveDistance, 0);
            yield return new WaitForSeconds(interval);
            cookerImage.transform.position -= new Vector3(0, moveDistance, 0);
            yield return new WaitForSeconds(interval);
        }
    }
}
