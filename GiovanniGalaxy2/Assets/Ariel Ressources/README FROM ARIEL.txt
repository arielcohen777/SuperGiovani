GameManager:
Need a UI with a TextMeshPro for the Coins (GameObject -> UI -> Canvas, then UI -> TextMeshPro inside of Canvas)
Reference a TextMeshPro UI in the GameManager, no need to write anything in there, 
maybe just "Coins: " for reference.
And also reference the Player
** To get GameManger instance in your script -> GameManager gm; (Global Variable)
(In Start() or Awake()) -> gm = GameManager.Instance; **

Coin:
Add CoinSpawn prefab in your Enemy.
Call CoinSpawn.SpawnCoin() whenever needed (enemy dead or wtv). 
If you forgot how to call it from the Enemy prefab add this line to your script:
GetComponentInChildren<CoinSpawn>().SpawnCoin();
or make CoinSpawn instance and call through there.
(Check CoinSpawn for better help)
PS: TESTCoinSpawner Prefab is just to test. It only works if you walk through it.

Player ref:
(Must have GameManager instance in your script initialized)
Player player = gm.player;