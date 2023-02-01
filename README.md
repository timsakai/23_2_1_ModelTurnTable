# 23_2_1_ModelTurnTable
 Unityでの3Dモデル展示用プロジェクト
# 環境
 - Unity 2021.3.15f1
 - Built-inRenderPipeline
 - Cinemachine 2.8.9
 - DOTween 1.2.705 (使ってない)
# 使い方
 ## モデルプレハブのセットアップ
  - Resources > Prefabs > Pawn を選択。
  - 右クリック→Create > Prefab Variant でPrefabVariantを作成。
  - 作成したPrefabVariantを編集。
  - Pawn > Model 以下にモデルを配置します。
  - モデルはオブジェクトとして存在できればスクリプトにもアニメーションにも特に規格はありません。ApplyRootMotionのチェックは外しておくとよいでしょう。
 ## シーンのセットアップ
  - Scenes > SampleSceneをコピー。
  - コピーしたシーンを編集。
  - GameManager > PawnGenerator > Prefabs に作成したモデルプレハブを指定。
  - GameManager > PawnGenerator > Generateボタンで指定したモデルプレハブが円周上に並べられます。
  - シーンを再生して動作確認。
  # 仕様
  - 左クリックで回転します。
  - 画面下部のボタン/左右キーでフォーカスするモデルを切り替えます。
  - 操作をせずに2秒経つと自動で回転します。(モデルオブジェクトPawn > PawnController > NoControllTime)
  - 操作せずに10秒待つと自動でフォーカスするモデルを切り替えます。(GameManager > FocusManager > NoControllTime)
  - GameManager > PawnGenerator > BasePoint に指定されTransformのローカル座標でモデルの高さを調節できます。
  - GameManager > PawnGenerator > Pedestal でモデル毎に生成される同一オブジェクトを指定します。(台座として使用します。)
  - GameManager > PawnGenerator > Radius で生成する円周の半径を設定します。
  - GameManager > PawnGenerator > Generate 時にGenerated内オブジェクトを削除します。生成後に生成オブジェクトをGenerated内にリンクします。
  - GameManager > FocusManager > Pawns内にフォーカス可能なモデルオブジェクトがリンクされます。
