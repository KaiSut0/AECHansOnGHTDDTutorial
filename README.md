# 必要な環境
- Rhino 6 or later version
- Visual Studio 2017 or 2019

# このハンズオンの目的と対象者
Grasshopper を使ってモデリングの自動化が進む中、企業からGH案件が来る方も多いのでは無いでしょうか。

手元で扱うGHコンポーネントを作るなら自己流でGHを書いていても特に問題は出ないかとは思いますが、
いざ納品するとなると、作ったコンポーネントが正しく動作するかを保証する必要があります。

プログラミングの世界では、プロダクションコードにはテストコードを必ず書き、所定の動作を保証することが当たり前となっています。
また、テストコードを利用して正しく動作するプログラムを効率的に書くための手法であるテスト駆動開発というものもあります。


このハンズオンでは、正しく動作するGHコンポーネントを開発するために、
「GHをテスト駆動開発する」ということに取り組み、一つのお作法を作ることが目的です。


対象者は、
- 企業からGH案件が降ってくる方（多分日本に数人しかいない。。。）
- いつもGHがスパゲッティになって辛い方
- コードを書くのは得意だけど最近GHを初めて開発方法がよくわからないという方

あたりを想定しています。

# テストコードって何？
テストコードとは、プログラムが正しく動作することを保証するための命綱となるコードです。

ほとんどのプログラムはある入力Aが来たときに何らかの処理をしてAに対する所定の出力Bを返すという構成をしています。

プロダクションコードを開発する上で想定した挙動を保証するためには、開発したプログラムに対して、様々な入力Aに対して正しい出力Bを返すかどうかを確かめるプログラムがテストコードです。

# テストを書くと何が嬉しいの？
以下 Qiita より引用

- テストコードはアプリケーションの命綱、安全ネット、防弾チョッキ
- 「このコード、テストなしでリリースするのはちょっと不安だな」と思ったら、それがテストを書くトリガー
- リリースするときに「ちゃんとうまく動きますように」と祈ってる自分がいたら、テストが不足している証拠
- 将来の自分が楽をするために書く
- コマンド一発でこれまで書いてきたコードの動作確認ができる！ 速いし、楽ちん！！
- 毎回リリース前に全部手作業と目視でテストするつもり？無理だよね！
- 将来、自分のコードをメンテするかもしれない他のメンバーのために書く
- ドキュメントを書く代わりに、コードを書いた人の意図や頭の中にある仕様を明示的なテストコードとして残しておく
- 将来、Railsやライブラリ（gem）をアップデートするときのために書く
- 「テストが全部パスすればきっと大丈夫」と思えるようにする
- 不具合を修正するときに書く
- テストコード上で不具合を再現させて、失敗するテストコードがパスするようにアプリ側のコードを修正する
- テストコードがあれば不具合の修正と再発防止を一度に実現できる（同じ不具合を再発させてしまうのはプロとして恥ずかしい）
# テストを書いてみよう！
```csharp SumClass.cs
using System.Linq;

namespace MyTestTutorial
{
    public class SumClass
    {
        public static double Add(double a, double b)
        {
            return a + b;
        }
        public static double Sum(double[] numbers)
        {
            return numbers.Sum();
        }
    }
}
```

```csharp SumClassTests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyTestTutorial.Tests
{
    [TestClass()]
    public class SumClassTests
    {
        [TestMethod()]
        public void AddTest()
        {
            // Prepare test case
            double[] aArray = new double[]             { 0.0, 1.0,   2.0, -1.0,  10000000000.0 };
            double[] bArray = new double[]             { 0.0, 2.0, -20.0, -2.0, -10000000000.0 };
            double[] addedExpectedArray = new double[] { 0.0, 3.0, -18.0, -3.0,            0.0 };
            // Run test
            for (int i = 0; i < 5; i++)
            {
                double a = aArray[i];
                double b = bArray[i];
                double addedExpected = addedExpectedArray[i];
                double added = SumClass.Add(a, b);
                // Make sure that the results are expected values
                Assert.AreEqual(addedExpected, added);
            }
        }
        [TestMethod()]
        public void SumTest()
        {
            // Prepare test case
            double[] numbers = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            double summedExpected = 15.0;
            double summed = SumClass.Sum(numbers);
            Assert.AreEqual(summedExpected, summed);
        }
    }
}
```

# TDD とは？
TDD (Test Driven Development, テスト駆動開発) とは、まずプログラムに必要な機能のテストを書いて（テストファースト）、そのテストを通るような必要最低限の実装を行い、これを繰り返すことでコードを洗練化させる開発手法のことです。

より詳しく知りたい方はこちらを参照してください。https://dev.classmethod.jp/articles/what-tdd/

# TDD の流れ
・失敗するテストを書く
・できる限り早く、テストに通るような最小限のコードを書く
・コードの重複を除去する（リファクタリング）

# 実際にTDDをやってみよう！
## まずテストを書く
```csharp SubtractClass.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTestTutorial;

namespace MyTestTutorialTests
{
    /// <summary>
    /// Summary description for SubtructClassTest1
    /// </summary>
    [TestClass]
    public class SubtractClassTests
    {
        [TestMethod]
        public void SubtractTest()
        {
            // Prepare test case
            double a = 2.0;
            double b = 1.0;
            double subtructExpected = 1.0;
            Assert.AreEqual(SubtractClass.Subtract(a, b), subtructExpected);
        }
    }
}
```
## コンパイルエラーさせる
まだテストするコード (SubtractClass.Subtract(double a, doube b)) を実装していないのでコンパイルエラーが発生する。
## テストを通る最小限の実装をする
```csharp
namespace MyTestTutorial
{
    public class SubtractClass
    {
        public static double Subtract(double a, double b)
        {
            return 2.0 - 1.0;
        }
    }
}
```

## テストが通る
<img width="167" alt="image.png (4.1 kB)" src="https://img.esa.io/uploads/production/attachments/11581/2020/11/28/50027/92131a67-27a8-45d3-a8ec-d0ddab5e67ad.png">

## テストケースを増やす
```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTestTutorial;

namespace MyTestTutorialTests
{
    [TestClass]
    public class SubtractClassTests
    {
        [TestMethod]
        public void SubtractTest()
        {
            // Prepare test case
            double[] aArray = new double[] { 2.0, 3.0 };
            double[] bArray = new double[] { 1.0, 2.0 };
            double[] subtructExpectedArray = new double[] { 3.0, 5.0 };
            for(int i = 0; i < 2; i++)
            {
                double a = aArray[i];
                double b = bArray[i];
                double subtructExpected = subtructExpectedArray[i];
                Assert.AreEqual(SubtractClass.Subtract(a, b), subtructExpected);
            }
        }
    }
}
```
## テストが失敗する
<img width="185" alt="image.png (4.0 kB)" src="https://img.esa.io/uploads/production/attachments/11581/2020/11/28/50027/964baa9a-3e1b-4dc3-bafd-74cdea047933.png">

## 複数のテストケースに対応したコードを書く
```csharp
namespace MyTestTutorial
{
    public class SubtractClass
    {
        public static double Subtract(double a, double b)
        {
            return a - b;
        }
    }
}
```
## テストが通る
<img width="167" alt="image.png (4.1 kB)" src="https://img.esa.io/uploads/production/attachments/11581/2020/11/28/50027/92131a67-27a8-45d3-a8ec-d0ddab5e67ad.png">

# GHでTDDを試みる
## お題：螺旋階段を自動生成するコンポーネント
<img width="300" alt="Image from iOS.jpg (70.1 kB)" src="https://img.esa.io/uploads/production/attachments/11581/2020/11/28/50027/c148f1db-8970-4d85-b18a-6d8ad246b5a4.jpg">

## まずテストケースを作る
<img width="400" alt="image.png (187.8 kB)" src="https://img.esa.io/uploads/production/attachments/11581/2020/11/28/50027/460bf25c-e2bd-4cd1-9a26-6fa92ca8cc22.png">
<img width="400" alt="image.png (22.7 kB)" src="https://img.esa.io/uploads/production/attachments/11581/2020/11/28/50027/7134b110-dddb-4e65-91f2-20d2eefb2cb0.png">
<img width="220" alt="image.png (21.8 kB)" src="https://img.esa.io/uploads/production/attachments/11581/2020/11/28/50027/5323738a-c463-4115-8ef3-245f333690d6.png">

## 入力と出力を定義する
<img width="906" alt="image.png (55.0 kB)" src="https://img.esa.io/uploads/production/attachments/11581/2020/11/28/50027/7712235e-28d4-4a7e-a0e8-0032634795cb.png">



## いい感じに入出力をモジュール化する
<img width="1448" alt="image.png (108.7 kB)" src="https://img.esa.io/uploads/production/attachments/11581/2020/11/28/50027/f1cc5a6f-5761-4550-8011-4e1ff955da26.png">


## 実装とテストを書く
<img width="1631" alt="image.png (174.8 kB)" src="https://img.esa.io/uploads/production/attachments/11581/2020/11/28/50027/9da26a4e-d0c5-4e7a-8d00-9840e937e868.png">

## クラスター化してテストを実行する
<img width="665" alt="image.png (85.3 kB)" src="https://img.esa.io/uploads/production/attachments/11581/2020/11/28/50027/d7de9aa2-95d4-45b7-a8fe-c4acac97a58c.png">

## 各モジュールを組み合わせてコンポーネント化する
<img width="1363" alt="image.png (105.7 kB)" src="https://img.esa.io/uploads/production/attachments/11581/2020/11/28/50027/0c7f6ba2-6653-4656-b4bf-ee1af0ccb151.png">

