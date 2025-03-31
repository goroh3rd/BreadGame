using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageSelectAnimation : MonoBehaviour // ステージが選ばれたときのアニメーション
{
    [SerializeField] private List<Sprite> usingImages;
    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private int rows = 5, cols = 5;
    [SerializeField] private float spacing = 2f;
    [SerializeField] private Vector2 offset = new Vector2(0, 0);
    [SerializeField] private float appearDuration = 0.5f;
    [SerializeField] private float disappearDuration = 0.5f;
    [SerializeField] private float transitionDelay = 2f;
    [SerializeField] private string nextScene = "";
    private bool isAnimating = false;
    public bool IsAnimating => isAnimating;
    private GameObject[,] images;

    public void SetNextScene(string sceneName)
    {
        nextScene = sceneName;
    }
    public void LoadScene(string sceneName)
    {
        if (isAnimating) return;
        isAnimating = true;
        SetNextScene(sceneName);
        DontDestroyOnLoad(this);
        StartCoroutine(GenerateImages());
    }

    IEnumerator GenerateImages()
    {
        Sprite sprite = usingImages[Random.Range(0, usingImages.Count)];
        images = new GameObject[rows, cols];
        Vector3 startPos = new Vector3(-cols * spacing / 2 + offset.x, -rows * spacing / 2 + offset.y, 0);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Vector3 position = startPos + new Vector3(x * spacing, y * spacing, 0);
                if (y % 2 == 1) position.x += spacing / 2; // 互い違い配置

                GameObject image = Instantiate(imagePrefab, position, Quaternion.identity);
                SpriteRenderer sr = image.GetComponent<SpriteRenderer>();
                sr.sprite = sprite;
                DontDestroyOnLoad(image);
                sr.sortingOrder = image.transform.GetSiblingIndex();
                image.transform.localScale = Vector3.zero;
                images[y, x] = image;

                // イージングで拡大
                image.transform.DOScale(3.5f, appearDuration).SetEase(Ease.OutBack);
                yield return new WaitForSeconds(transitionDelay / (rows * cols));
            }
        }
        SceneManager.LoadSceneAsync(nextScene).completed += _ => StartCoroutine(this.RemoveImages());
    }

    IEnumerator RemoveImages()
    {
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                images[y, x].transform.DOScale(0, disappearDuration).SetEase(Ease.InBack);
                yield return new WaitForSeconds(transitionDelay / (rows * cols));
            }
        }
        yield return new WaitForSeconds(disappearDuration);
        foreach (var img in images) Destroy(img);
        isAnimating = false;
        Destroy(this.gameObject);
    }
}
