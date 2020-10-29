using System;
using System.Diagnostics;

class Answer
{
    static void Main(string[] args)
    {
        Exercise01();
        Exercise02();
        Exercise03();

        Console.Write("Hit Enter to exit...");
        Console.ReadLine();
    }

    /// <summary>
    /// 素数を判定する。
    /// </summary>
    static void Exercise01()
    {
        while (true)
        {
            Console.Write("素数かどうか判定します。正の整数を入力して下さい > ");
            string buffer = Console.ReadLine();
            if (buffer.Length == 0)
            {
                break;
            }

            uint number;    // uint は「0 以上の整数」

            if (uint.TryParse(buffer, out number))  // out 修飾子は「関数に引数として変数を渡して、変数に値を入れてもらう時」に使う。値をもらう方法としての「戻り値」は一つしか値を返せないために、このように値をもらう手段として引数を使うことがある。参照: 『独習 C#』7.6.6. 出力引数（outキーワード）
            {
                Stopwatch sw = new Stopwatch(); // 実行時間を計るため
                sw.Start();
                bool isPrime = IsPrime(number);
                sw.Stop();

                if (isPrime)
                {
                    Console.WriteLine("{0} は素数です。実行時間: {1}", number, sw.ElapsedTicks.ToString());
                }
                else
                {
                    Console.WriteLine("{0} は素数ではありません。実行時間: {1}", number, sw.ElapsedTicks.ToString());
                }
            }
            else
            {
                Console.WriteLine("入力された値が不正です。");
            }
        }

        Console.WriteLine("終了します。");
    }

    /// <summary>
    /// フィボナッチ数を求める。
    /// </summary>
    static void Exercise02()
    {
        while(true)
        {
            Console.Write("いくつのフィボナッチ数を求めますか？ > ");
            string buffer = Console.ReadLine();
            
            if (buffer.Length == 0) // Enter のみが入力されたら終了する
            {
                break;
            }

            uint number;

            if (uint.TryParse(buffer, out number))
            {
                if (number == 0)    // 0 が入力されたら終了する
                {
                    break;
                }

                for (uint i = 1; i < number + 1; i++)
                {
                    Console.Write(FibonacciNumber(i));
                    Console.Write(", ");
                }

                Console.Write("\r\n");
            }
            else // uint にできない場合は終了する
            {
                break;
            }
        }

        Console.WriteLine("終了します。");
    }

    /// <summary>
    /// ソートする。
    /// </summary>
    static void Exercise03()
    {
        while (true)
        {
            Console.Write("整数を半角スペースで区切って入力してください > ");
            string buffer = Console.ReadLine();

            if (buffer.Length == 0)
            {
                break;
            }

            string[] bufArray = buffer.Split(' ');
            int[] intArray = new int[bufArray.Length];

            bool isInputInvalid = false;    // 入力された値が不正かどうかを入れる

            for (int i = 0; i < bufArray.Length; i++)
            {
                if (!int.TryParse(bufArray[i], out intArray[i]))
                {
                    Console.WriteLine("入力された内容が不正です。");
                    isInputInvalid = true;
                    break;
                }
            }

            if (isInputInvalid) // 入力された内容が不正だったら、ループ内の残りの処理は飛ばしてループの先頭から実行する
            {
                continue;
            }
            else
            {
                int[] resultArray = BubbleSort(intArray);

                foreach (var i in resultArray)
                {
                    Console.Write(i + ", ");
                }

                Console.Write("\r\n");
            }
        }

        Console.WriteLine("終了します。");
    }

    /// <summary>
    /// パラメーターとして渡された正の整数が素数かどうか判定する。
    /// 素数判定アルゴリズムは「エラトステネスの篩（ふるい）」を使う。
    /// </summary>
    /// <param name="number">判定対象の整数</param>
    /// <returns>素数の場合は true、そうでない場合は false を返す</returns>
    static bool IsPrime(uint number)
    {
        if (number < 2) // 0, 1 は素数ではない
        {
            return false;
        }
        else if (number == 2)   // 2 は素数
        {
            return true;
        }
        else if (number % 2 == 0)   // 偶数である場合は素数ではない
        {
            return false;
        }

        // それ以外の場合
        for (int i = 3; Math.Pow(i, 2) <= number; i += 2)  // 3 以上の奇数で割っていく
        {
            if (number % i == 0)    // 割り切れたらそれは素数ではない
            {
                return false;
            }
        }

        // どれでも割り切れなかったら素数
        return true;
    }

    /// <summary>
    /// number 番目のフィボナッチ数を返す。
    /// </summary>
    /// <param name="number">正の整数</param>
    /// <returns>number 番目のフィボナッチ数</returns>
    static uint FibonacciNumber(uint number)
    {
        if (number == 0)
        {
            Console.WriteLine("与えられた数が不正です。");
        }
        else if (number == 1 || number == 2)
        {
            return 1;
        }

        return FibonacciNumber(number - 2) + FibonacciNumber(number - 1);
    }

    /// <summary>
    /// 与えられた整数の配列を昇順ソートして返す。
    /// ソートアルゴリズムはバブルソートを使う。
    /// </summary>
    /// <param name="intArray">並び変えたい整数の配列</param>
    /// <returns>昇順ソートした整数の配列</returns>
    static int[] BubbleSort(int[] intArray)
    {
        for (int i = 0; i < intArray.Length; i++)
        {
            for (int j = intArray.Length - 1; i < j; j--)
            {
                if (intArray[j] < intArray[j - 1])
                {
                    int temp = intArray[j];
                    intArray[j] = intArray[j - 1];
                    intArray[j - 1] = temp;
                }
            }
        }

        return intArray;
    }
}
