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
    public GameObject trajectoryHelper;
    public GameObject playerTurnPivot;
    public GameObject playerShootPosition;
    public GameObject infoPanel;
    public GameObject UiDynamicPower;
    public GameObject UiDynamicDegree;


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
}
