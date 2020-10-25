using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 同期・非同期に複数の Cube を生成するコンポーネント
/// </summary>
public class CreateCubes : MonoBehaviour
{
    /// <summary>Cube を生成する X 座標の範囲</summary>
    [SerializeField] Range m_XRange = Range.Zero;
    /// <summary>Cube を生成する Y 座標の範囲</summary>
    [SerializeField] Range m_YRange = Range.Zero;
    /// <summary>Cube を生成する Z 座標の範囲</summary>
    [SerializeField] Range m_ZRange = Range.Zero;
    /// <summary>Cube を生成する間隔（インターバル）</summary>
    [SerializeField] float m_interval = 0.1f;
    /// <summary>開始したコルーチンを入れておく変数</summary>
    Coroutine m_coroutine = null;

    /// <summary>
    /// 同期的に複数の Cube を生成する
    /// </summary>
    /// <param name="count">生成する Cube の数</param>
    public void CreateCubesSync(int count)
    {
        Debug.Log("関数を抜けます");
        for (int i = 0; i < count; i++)
        {
            CreateCubeAtRandomPosition();
        }
        Debug.Log("関数を抜けます");
    }

    /// <summary>
    /// 非同期でディレイを入れながら複数の Cube を生成する
    /// </summary>
    /// <param name="count">生成する Cube の数</param>
    public void CreateCubesAsync(int count)
    {
        Debug.Log("キューブを作り始めます");
        m_coroutine = StartCoroutine(CreateCubesAsyncImpl(count));  // コルーチンは StartCoroutine 関数の引数として呼び出します。
        Debug.Log("関数を抜けます");
    }

    /// <summary>
    /// これがコルーチンです。ポイントは「戻り値の型を IEnumerator にする」ことです。
    /// 非同期でウェイトを入れながら複数の Cube を生成します。
    /// </summary>
    /// <param name="count">生成する Cube の数</param>
    /// <returns></returns>
    IEnumerator CreateCubesAsyncImpl(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateCubeAtRandomPosition();
            yield return new WaitForSeconds(m_interval);    // "yield return new (待ち方を指定する)" と書くと、待つことができます。待ち方には WaitForSeconds, WaitForEndOfFrame, WaitWhile, WaitUntil などがあります。
        }
    }

    /// <summary>
    /// コルーチンによる Cube 生成を停止する
    /// </summary>
    public void StopCreatingCubes()
    {
        if (m_coroutine != null)
        {
            StopCoroutine(m_coroutine); // StopAllCoroutine 関数で「現在実行中の全てのコルーチンを停止する」ことができる
        }
        else
        {
            Debug.Log("実行中のコルーチンはありません");
        }
    }

    /// <summary>
    /// 同期的にウェイトを入れながら複数の Cube を生成する
    /// </summary>
    /// <param name="count">生成する Cube の数</param>
    public void CreateCubeBadExample(int count)
    {
        Debug.Log("キューブを作り始めます");
        for (int i = 0; i < count; i++)
        {
            CreateCubeAtRandomPosition();
            System.Threading.Thread.Sleep(Mathf.RoundToInt(m_interval * 1000)); // スレッドを指定時間だけ停止する（同期的）
        }
        Debug.Log("関数を抜けます");
    }

    /// <summary>
    /// 設定された範囲のランダムな座標に Rigidbody 付きの Cube を生成する
    /// </summary>
    void CreateCubeAtRandomPosition()
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        float x = Random.Range(m_XRange.Min, m_XRange.Max);
        float y = Random.Range(m_YRange.Min, m_YRange.Max);
        float z = Random.Range(m_ZRange.Min, m_ZRange.Max);
        cube.transform.position = new Vector3(x, y, z);
        cube.AddComponent<Rigidbody>();
    }
}

/// <summary>
/// 最大と最小のペアを指定するための構造体
/// </summary>
[System.Serializable]
public struct Range
{
    public float Min;
    public float Max;

    public Range(float min, float max)
    {
        this.Min = min;
        this.Max = max;
    }

    public static Range Zero = new Range(0, 0);
}