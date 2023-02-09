GameManager:
** To get GameManger instance in your script -> GameManager gm; (Global Variable)
In Start() add -> gm = GameManager.Instance; **
Includes a reference for (actual names of variables):
coins (int)
player (GameObject)
playerIsAlive (bool)
fpc (FirstPersonController)
cam (Main Camera)
changeGun (ChangeGun)
inventory (InventoryObject)
activeWeapon (WeaponSlot)
wepUi (Weapon Ui)
barUi (Bars Ui)

Coin:
Add CoinSpawn prefab in your Enemy.
Call CoinSpawn.SpawnCoin() whenever needed (enemy dead or wtv). 
If you forgot how to call it from the Enemy prefab add this line to your script:
GetComponentInChildren<CoinSpawn>().SpawnCoin();
or make CoinSpawn instance and call through there.
(Check CoinSpawn for better help)
PS: TESTCoinSpawner Prefab is just to test. It only works if you walk through it.

Weapon: 
Right Click -> Create/WeaponSO/Weapon
In properties, give it a name, the prefab, max ammo and count ammo,
damage, rate of fire, SKIP Next Time To Fire, give range,
If you have particles for muzzle flash and a prefab for impact, add it (It can be null)

Inventory:
The weapons are added by colliding with an object 
containing the "Weapon.cs" (Pickables/Items/WeaponSO/Script/) and adding
a weapon to it. Switch weapons in inventory by clicking Q or Left Trigger (GamePad).
