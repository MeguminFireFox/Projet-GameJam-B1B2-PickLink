using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinedTheGame : MonoBehaviour
{
    [SerializeField]
    GameObject PrefabMidPoint;

    [SerializeField]
    PlayerInputManager inputManager;

    [SerializeField]
    GameObject Line;

    [SerializeField]
    private List<GameObject> players = new List<GameObject>();

    int nbplayer = 1;

    private void Start()
    {
        inputManager.onPlayerJoined += OnPlayerJoined;
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        GameObject playerClone = playerInput.gameObject;

        if (!players.Contains(playerClone))
        {
            playerClone.name = "MoutMout" + nbplayer;
            players.Add(playerClone);
            Debug.Log("Joueur ajouté: " + playerClone.name);
            nbplayer++;
        }
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        if (players.Count == 1) return;
        
        else if (players.Count == 2)
        {
            SetUpGameObject(0, 1);
        }

        else if (players.Count == 3)
        {
            SetUpGameObject(1, 2);
        }

        else if (players.Count == 4)
        {
            SetUpGameObject(2, 3);
        }

        var x = FindAnyObjectByType<LineRendererLinkPlayer>();
        if (x!= null)
        {
            foreach (var player in players)
            {
                Debug.Log(player.name);
                if (!x.GetComponent<LineRendererLinkPlayer>().PlayersTransform.Contains(player))
                {
                    x.GetComponent<LineRendererLinkPlayer>().PlayersTransform.Add(player);
                }
               
            }
            x.GetComponent<LineRendererLinkPlayer>().lr.positionCount = x.GetComponent<LineRendererLinkPlayer>().PlayersTransform.Count;

        }
        else
        {
            GameObject line = Instantiate(Line);
            foreach (var player in players)
            {
                var a = line.GetComponent<LineRendererLinkPlayer>().PlayersTransform;
                line.GetComponent<LineRendererLinkPlayer>().PlayersTransform.Add(player);
            }
        }
       
    }

    public void SetUpGameObject(int one, int two)
    {
        GameObject Mid = Instantiate(PrefabMidPoint);
        Mid.gameObject.GetComponent<Middle>().Player1 = players[one];
        Mid.gameObject.GetComponent<Middle>().Player2 = players[two];
        Mid.gameObject.GetComponent<Middle>().MiddlePoint = Mid.gameObject;
        players[one].gameObject.GetComponent<PlayerDistance>().PlayerNeighboor2 = players[two];
        players[one].gameObject.GetComponent<PlayerDistance>().MiddlePoint2 = Mid;
        players[two].gameObject.GetComponent<PlayerDistance>().PlayerNeighboor1 = players[one];
        players[two].gameObject.GetComponent<PlayerDistance>().MiddlePoint1 = Mid;
    }
}
