using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	private GameController gameControl;
	private PlayerController playControl;
	
	public Texture2D noUmbrella;
	
	enum TEXTURES{TITLE, CONTROLS}
	
	enum GUIS{COOL,LIVES,SCORE,PAUSE,OVER,START,STARTP,CONTROL,CONTROLP}
	
	public GUITexture[] textures;
	public GUIText[] texts;
	
	public float ratio = 1;
	
	void Start() {
		gameControl = GameObject.Find ("GameManager").GetComponent <GameController> ();
		playControl = GameObject.Find ("Avatar").GetComponent <PlayerController> ();
	}
	
	void OnGUI() {
		if(gameControl.currState != GameController.STATES.SPLASH){
			GUI.Box(new Rect(60 * ratio, 50 * ratio, 300 * ratio, 100 * ratio), "");
			
			if(playControl.umbrellaTime > 0){
				float width = playControl.umbrellaTime * 300 / playControl.maxUmbrella;
				GUI.Box(new Rect(60 * ratio, 50 * ratio, width * ratio, 100 * ratio), "");
			}
			
			if(playControl.cooldownTime > 0){
				GUI.Label(new Rect(75 * ratio, 175 * ratio, 275 * ratio, 275 * ratio), noUmbrella);
			}
		}
	}
	
	void Update()	{
		texts[(int)GUIS.PAUSE].enabled = (gameControl.currState == GameController.STATES.PAUSE);
	}
	
	void FixedUpdate() {        
		
		ratio = (Screen.width / 16f) / 100;
		
		textures [(int)TEXTURES.TITLE].pixelInset = new Rect(-75 * ratio, -125 * ratio, 1000 * ratio, 250 * ratio);
		textures [(int)TEXTURES.CONTROLS].pixelInset = new Rect(-700 * ratio, -400 * ratio, 1000 * ratio, 500 * ratio);
		texts [(int)GUIS.PAUSE].fontSize = texts [(int)GUIS.OVER].fontSize = Mathf.RoundToInt(100 * ratio);
		texts [(int)GUIS.START].fontSize = texts [(int)GUIS.CONTROL].fontSize = texts [(int)GUIS.STARTP].fontSize = texts [(int)GUIS.CONTROLP].fontSize = Mathf.RoundToInt(100*ratio);
		texts [(int)GUIS.COOL].fontSize = texts [(int)GUIS.LIVES].fontSize = texts [(int)GUIS.SCORE].fontSize = Mathf.RoundToInt(50 * ratio);
		
		texts [(int)GUIS.COOL].pixelOffset = new Vector2(20 * ratio, 80 * ratio);
		texts [(int)GUIS.LIVES].pixelOffset = new Vector2(20 * ratio, 0);
		texts [(int)GUIS.SCORE].pixelOffset = new Vector2(20 * ratio, -80 * ratio);
		texts [(int)GUIS.START].pixelOffset = new Vector2 (100 * ratio, -160 * ratio);
		texts [(int)GUIS.CONTROL].pixelOffset = new Vector2 (100 * ratio, -280 * ratio);
		texts [(int)GUIS.STARTP].pixelOffset = new Vector2 (10 * ratio, -160 * ratio);
		texts [(int)GUIS.CONTROLP].pixelOffset = new Vector2 (10 * ratio, -280 * ratio);
		
		texts[(int)GUIS.COOL].text = "Cooldown: " + Mathf.CeilToInt(playControl.cooldownTime);
		texts[(int)GUIS.LIVES].text = "Lives: " + gameControl.lives;
		texts[(int)GUIS.SCORE].text = "Score: " + (int) gameControl.score;
		
		textures[(int)TEXTURES.TITLE].enabled = (gameControl.currState == GameController.STATES.SPLASH);
		
		texts[(int)GUIS.COOL].enabled = (gameControl.currState != GameController.STATES.SPLASH && playControl.cooldownTime > 0);
		texts[(int)GUIS.LIVES].enabled = (gameControl.currState != GameController.STATES.SPLASH);
		texts[(int)GUIS.SCORE].enabled = (gameControl.currState != GameController.STATES.SPLASH); 
		
		texts[(int)GUIS.OVER].enabled = (gameControl.currState == GameController.STATES.LOSE);
	}
}