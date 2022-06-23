using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersController : MonoBehaviour
{
    [Header("Public GamePlay settings")]
    public bool useHelper = true;                   //use helper dots when player is aiming to shoot
    public int baseShootPower = 30;                 //base power. edit with care.
    public int playerHealth = 100;                  //starting (full) health. can be edited.
    private int minShootPower = 15;                 //powers lesser than this amount are ignored. (used to cancel shoots)
    internal int playerCurrentHealth;               //real-time health. not editable.
    public static bool isPlayerDead;				//flag for gameover event

    [Header("Linked GameObjects")]
    //Reference to game objects (childs and prefabs)
    public GameObject arrow;
    // public GameObject trajectoryHelper;
    // public GameObject playerTurnPivot;
    // public GameObject playerShootPosition;
    // public GameObject infoPanel;
    // public GameObject UiDynamicPower;
    // public GameObject UiDynamicDegree;

//private settings
	private Vector2 icp; 							//initial Click Position
	private Ray inputRay;
	private RaycastHit hitInfo;
	private float inputPosX;
	private float inputPosY;
	private Vector2 inputDirection;
	private float distanceFromFirstClick;
	private float shootPower;
	private float shootDirection;
	private Vector3 shootDirectionVector;

	//helper trajectory variables
	private float helperCreationDelay = 0.12f;
	private bool canCreateHelper;
	private float helperShowDelay = 0.2f;
	private float helperShowTimer;
	private bool helperDelayIsDone;


    /// <summary>
	/// Init
	/// </summary>
	void Awake () {
		// icp = new Vector2 (0, 0);
		// infoPanel.SetActive (false);
		// shootDirectionVector = new Vector3(0,0,0);
		// playerCurrentHealth = playerHealth;
		// isPlayerDead = false;

		// // gc = GameObject.FindGameObjectWithTag ("GameController");
		// // cam = GameObject.FindGameObjectWithTag ("MainCamera");

		// canCreateHelper = true;
		// helperShowTimer = 0;
		// helperDelayIsDone = false;
	}
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    	/// <summary>
	/// When player is aiming, we need to turn the body of the player based on the angle of icp and current input position
	/// </summary>
	void turnPlayerBody() {
		
		inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(inputRay, out hitInfo, 50)) {
			// determine the position on the screen
			inputPosX = this.hitInfo.point.x;
			inputPosY = this.hitInfo.point.y;
			//print("Pos X-Y: " + inputPosX + " / " + inputPosY);

			// set the bow's angle to the arrow
			inputDirection = new Vector2(icp.x - inputPosX, icp.y - inputPosY);
			//print("Dir X-Y: " + inputDirection.x + " / " + inputDirection.y);

			shootDirection = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;

			//for an optimal experience, we need to limit the rotation to 0 ~ 90 euler angles.
			//so...
			if (shootDirection > 90)
				shootDirection = 90;
			if (shootDirection < 0)
				shootDirection = 0;

			//apply the rotation
			playerTurnPivot.transform.eulerAngles = new Vector3(0, 0, shootDirection);

			//calculate shoot power
			distanceFromFirstClick = inputDirection.magnitude / 4;
			shootPower = Mathf.Clamp(distanceFromFirstClick, 0, 1) * 100;
			//print ("distanceFromFirstClick: " + distanceFromFirstClick);
			//print("shootPower: " + shootPower);

			//modify camera cps - next update
			//CameraController.cps = 5 + (shootPower / 100);

			//show informations on the UI text elements
			UiDynamicDegree.GetComponent<TextMesh>().text = ((int)shootDirection).ToString();
			UiDynamicPower.GetComponent<TextMesh> ().text = ((int)shootPower).ToString() + "%";

			if (useHelper) {
				//create trajectory helper points, while preventing them to show when we start to click/touch
				if (shootPower > minShootPower && helperDelayIsDone)
					StartCoroutine (shootTrajectoryHelper ());
			}
		}
	}



    // shoot player
    void shootArrow() {

		//set the unique flag for arrow in scene.
		// GameController.isArrowInScene = true;

		//play shoot sound
		playSfx(shootSfx[Random.Range(0, shootSfx.Length)]);

		//add to shoot counter
		//GameController.playerArrowShot++;

		GameObject arr = Instantiate(arrow, playerShootPosition.transform.position, Quaternion.Euler(0, 180, shootDirection * -1)) as GameObject;
		arr.name = "PlayerProjectile";
		// arr.GetComponent<MainLauncherController> ().ownerID = 0;

		shootDirectionVector = Vector3.Normalize (inputDirection);
		shootDirectionVector = new Vector3 (Mathf.Clamp (shootDirectionVector.x, 0, 1), Mathf.Clamp (shootDirectionVector.y, 0, 1), shootDirectionVector.z);

		arr.GetComponent<MainLauncherController> ().playerShootVector = shootDirectionVector * ( (shootPower + baseShootPower) / 50);

		print("shootPower: " + shootPower + " --- " + "shootDirectionVector: " + shootDirectionVector);

		cam.GetComponent<CameraController> ().targetToFollow = arr;

		//reset body rotation
		StartCoroutine(resetBodyRotation());
	}

}
