using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// サンプルのコードなので消していただいて構いません
/// このスクリプトをボタンにアタッチしていろいろやれば実際にどう使えばいいかわかるかと思います
/// このスクリプトではenum型とか使って良いカンジにやってますがそんなことする必要はなく、
/// StartCoroutine(SoundManager.ChangeBGM(int ,float ,float ));
/// StartCoroutine(SoundManager.StopBGM(float)));
/// StartCoroutine(SoundManager.PlaySE(int));
/// という形がちゃんとできていればおｋ
/// SoundManager.csを見てみてね
/// </summary>
public class SoundSample : MonoBehaviour
{
    private enum TypeOfSound//PlayTheSoundメソッドのswitch文に使われる。
    {
        BGM_0, BGM_1, SE_0, SE_1
    }
    [SerializeField] private TypeOfSound sound;//インスペクターから設定することで動作を選択。

[Header("BGMの場合だけ使うパラメータ（SEなら無視OK）")]//ChangeBGMに渡す引数用
    [SerializeField] private float fadeDuration;
    [SerializeField] private float waitTime;

    
    public void PlayTheSound()
    {
        switch(sound)
        {
            case TypeOfSound.BGM_0:
                //fadeDurationかけてフェードアウト、waitTimeだけ待機して0番のBGM開始
                StartCoroutine(SoundManager.ChangeBGM(0, fadeDuration, waitTime));
                Debug.Log("BGM_0 Pressed");
                break;
            case TypeOfSound.BGM_1:
                StartCoroutine(SoundManager.ChangeBGM(1, fadeDuration, waitTime));
                Debug.Log("BGM_1 Pressed");
                break;
            case TypeOfSound.SE_0:
                //0番のSEを再生
                StartCoroutine(SoundManager.PlaySE(0));
                Debug.Log("SE_0 Pressed");
                break;
            case TypeOfSound.SE_1:
                StartCoroutine(SoundManager.PlaySE(1));
                Debug.Log("SE_1 Pressed");
                break;
        }
    }
}
