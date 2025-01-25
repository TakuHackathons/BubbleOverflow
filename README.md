# グローバルクラスの呼び出し方

## 1. PrefabをSceneに適用する

`Assets/Prefabs/GameController.prefab` をそれぞれのSceneにドラッグ&ドロップでおいてください

## 2. GameController(グローバルクラス)を呼び出す

1. が適用済みの場合それぞれのクラスから `GameController` を呼び出して実行することができます。以下のメソッドを定義することで呼び出すことができます。

```C#
GameController.Instance.メソッド名(引数)
```

で呼び出すことができます。
メソッド名などの各種処理を記述したい場合は `Assets/Scripts/Controllers/GameController.cs` のC#のスクリプトを編集して実装していってください