using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField]
	int playerCurrentHP = 100;
	int playerMaxHP = 100;


	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Bullet") {
			this.playerCurrentHP -= 20;

		}
	}
		
	void Update () {
		if (this.playerCurrentHP == 0) {
			//PRZEGRANA KONIEC ZYCIA
			Destroy(gameObject);
		}
	}
}
