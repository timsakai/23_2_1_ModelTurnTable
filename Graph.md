23_2_1_ModelTurnTable
 - Unityでの3Dモデル展示用プロジェクト
# 環境
 - ~~Unity 2021.3.15f1~~
 - 24/11/17更新　：　Unity 2022.3.6f1 LTS に更新
 - Built-inRenderPipeline
 - Cinemachine 2.8.9
 - ~~DOTween 1.2.705~~ (使ってない)
# 使い方
## モデルプレハブのセットアップ

  1. Resources > Prefabs > Pawn を選択。
  2. 右クリック→Create > Prefab Variant でPrefabVariantを作成。
  3. 作成したPrefabVariantを編集。
  4. Pawn > Model 以下にモデルを配置します。

モデルはオブジェクトとして存在できればスクリプトにもアニメーションにも特に規格はありません。ApplyRootMotionのチェックは外しておくとよいでしょう。

## シーンのセットアップ

  1. Scenes > SampleSceneをコピー。
  2. コピーしたシーンを編集。
  3. GameManager > PawnGenerator > Prefabs に作成したモデルプレハブを指定。
  4. GameManager > PawnGenerator > Generateボタン
    - 指定したモデルプレハブが円周上に並べられます。
  5. シーンを再生して動作確認。
# 操作
  - 左クリック
    - モデル回転
  - 画面下部のボタン/左右キー
    - フォーカスするモデルを切り替え

  - 操作をせずに2秒経つと自動で回転します。
    - (モデルオブジェクトPawn > PawnController > NoControllTime　で調整可能)
  - 操作せずに10秒待つと自動でフォーカスするモデルを切り替えます。
    - (GameManager > FocusManager > NoControllTime　で調整可能)
# 調節  
- モデルの高さを調節
GameManager > PawnGenerator > BasePoint に指定されたTransformのローカル座標
- 台座（モデル毎に生成される同一オブジェクト）  
GameManager > PawnGenerator > Pedestal でプレハブ指定。
- 生成する円周の半径を設定  
GameManager > PawnGenerator > Radius で設定。
# 仕様
 - GameManager > PawnGenerator > Generate 時にGenerated内オブジェクトを削除します。
- 生成後に生成オブジェクトをGenerated内にリンクします。
 - GameManager > FocusManager > Pawns内にフォーカス可能なモデルオブジェクトがリンクされます。

# シーン構成　網羅
```mermaid
flowchart TD
subgraph Scene 構成 網羅
    PG-->|生成|DisplayObjects
    PG-->|モデル参照|ASTPAW
    PG-->|台座モデル参照|ASTPED
    PaVC-->|操作|CMC
    Canvas-->|操作|FM
    CMR-->|撮影結果書き込み|RRND
    FM-->|フォーカス設定|PC
    PC-->|入力～移動|PaVC
    CM-->|カーソル画像参照|ASTARW
    FM-->|操作|CMR
    AX-->|操作|FM
    subgraph GameManager
        CM(CursorManager)
        FM(FocusManager)
        AX(AxisPressEvent)
        PG(PawnGenerator)
    end
    subgraph DisplayObjects
        subgraph Pawns
            PC(PawnController)-->|回転|PaMS
            PaMS(Mesh)
            PaVC(VCAM)
            PaSVC(Side Camera VCAM)
        end
        subgraph Pedestals
            PdMS(Mesh)
        end
    end
    subgraph Canvas
        RRND(Right-LeftRenderer)
        BTNXT(ButtonNext-Prev)
    end
    subgraph Cameras
        CMC(MainCamera)
        CMR(Side Camera R-L)
    end
    subgraph Assets
        ASTARW[十字矢印画像]
        ASTPAW[モデルPrefab]
        ASTPED[台座Prefab]
    end
end
```
# シーン構成　抜粋
```mermaid
flowchart TD
subgraph Scene 構成 抜粋
    PG-->|生成|DisplayObjects
    PG-->|モデル参照|ASTPAW
    PG-->|台座モデル参照|ASTPED
    PaVC-->|操作|CMC
    Canvas-->|操作|FM
    FM-->|フォーカス設定|PC
    PC-->|操作|PaVC
    subgraph GameManager
        FM(FocusManager)
        PG(PawnGenerator)
    end
    subgraph DisplayObjects
        subgraph Pawns
            PC(PawnController)-->|回転|PaMS
            PaMS(Mesh)
            PaVC(VCAM)
        end
        subgraph Pedestals
            PdMS(Mesh)
        end
    end
    subgraph Canvas
        BTNXT(ButtonNext-Prev)
    end
    subgraph Cameras
        CMC(MainCamera)
    end
    subgraph Assets
        ASTPAW[モデルPrefab]
        ASTPED[台座Prefab]
    end
end
```
# クラス　(抜粋の範囲)
```mermaid
classDiagram
PawnGenerator : [インスペクタ]Transform ポーンの親
PawnGenerator : [インスペクタ]Transform ペデスタルの親
PawnGenerator : [インスペクタ]Transform 生成の中心
PawnGenerator : [インスペクタ]GameObject 台座のプレファブ
PawnGenerator : [インスペクタ]GameObjectList モデルのプレファブ
PawnGenerator : [インスペクタ]float 半径
PawnGenerator : [インスペクタ]GameObjectList 生成したオブジェクト
PawnGenerator : +生成()

PawnState : Focused - 画面中央で閲覧中
PawnState : Standby - 画面外で待機中

PawnController : [インスペクタ]float 回転速度
PawnController : [インスペクタ]float マウス感度
PawnController : [インスペクタ]float 回転慣性
PawnController : [インスペクタ]float 放置時間
PawnController : - float 現在の放置時間
PawnController : - bool 自動回転中？
PawnController : - bool マウス操作可能？
PawnController : [インスペクタ]Transform 回転中心
PawnController : [インスペクタ]Transform 操作するVirtualCamera
PawnController : [インスペクタ]Transform 前面固定カメラ位置
PawnController : - Vector3 回転速度
PawnController : [インスペクタ]PawnState ステート
PawnController : - Update()
PawnController : - FixedUpdate()
PawnController : - マウス操作開始()
PawnController : - マウス操作終了()
PawnController : - マウス操作入力()
PawnController : - Vcam回転()
PawnController : - PawnState設定()
PawnController : - 前面固定カメラ位置取得()
PawnController <|-- FocusManager : 登録されているPawnControllerのPawnStateを操作
PawnController <|-- PawnGenerator : 生成()にてモデルのプレハブを通して生成

```