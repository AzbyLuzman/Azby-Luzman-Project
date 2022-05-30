1927002 Azby Dzakiri Mazaya Luzman

担当箇所

1. プレイヤー移動操作　
	- CharacterMovementScript

2. ゲームプレイ流れと演出
	- GameplayScriptsフォルダー
	- GameStateManager はゲームプレイの流れ
	- MazeAssignment と　CollectBoxPost は迷路演出

3. 敵のAIと管理
	- EnemyScriptsフォルダー
	- 敵は２種類に分けます（ノーマルお化け と どんよりお化け）
	- EnemiesBehaviourはノーマルお化け、DonyoriBehaviourはどんよりお化けの AI
	- EnemiesManagerとDonyoriManager　は敵を管理するスクリプト
	- EnemySightは索敵の当たり判定

4. プレイヤーギミック（魔法）
	- ColorActionStatesフォルダー
	- ColorAction （DarkRed, DarkBlue, DarkYellow, Purple, Orange, Green）は魔法のギミック処理

5. UI
	- UIScriptsフォルダー

6. Unused　Scripts ：　現状のプロジェクトに使用されていない
