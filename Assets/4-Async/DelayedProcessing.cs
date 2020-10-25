using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedProcessing : MonoBehaviour
{
    [SerializeField] GameObject m_objectToBeDestoyed = null;

    public void DelayedDestroy(float delaySeconds)
    {
        Debug.LogFormat("{0} を {1} 秒後に破棄します", m_objectToBeDestoyed.name, delaySeconds.ToString());
        Destroy(m_objectToBeDestoyed, delaySeconds);
        Debug.Log("関数を抜けます");
    }

    public void DelayedFunctionCall(float delaySeconds)
    {
        Debug.LogFormat("Test 関数を {0} 秒後に呼び出します", delaySeconds.ToString());
        Invoke("Test", delaySeconds);
        Debug.Log("関数を抜けます");
    }

    public void RepeatFunctionCall(float delaySeconds)
    {
        Debug.LogFormat("Test 関数を {0} 秒後から {0} 秒おきに呼び出します", delaySeconds.ToString());
        InvokeRepeating("Test", delaySeconds, delaySeconds);    // CancelInvoke 関数で止める事ができる
        Debug.Log("関数を抜けます");
    }

    /// <summary>
    /// Console に "Test" とログを出力する
    /// </summary>
    void Test()
    {
        Debug.Log("Test");
    }
}
