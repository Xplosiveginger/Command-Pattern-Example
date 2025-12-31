using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickManager : MonoBehaviour
{
    public static ClickManager instance;
    public LayerMask groundMask;
    public NavMeshAgent agent;
    public GameObject clickFX;

    public Queue<Command> clickQueue = new Queue<Command>();
    private Command activeCommand;

    #region Demo 3
    [SerializeField] Character selectedCharacter;
    [SerializeField] List<Character> activeCharacters = new List<Character>();
    #endregion

    private void Awake()
    {
        instance = this;
    }

    #region Demo 3
    private void OnEnable()
    {
        Character.OnCharacterClicked += OnCharacterClickedHandled;
        Character.OnCharacterRegistered += AddCharacterToActiveCharacters;
        Character.OnCharacterDeregistered += OnCharacterRemovedHandled;
    }

    private void OnDisable()
    {
        Character.OnCharacterClicked -= OnCharacterClickedHandled;
        Character.OnCharacterRegistered -= AddCharacterToActiveCharacters;
        Character.OnCharacterDeregistered -= OnCharacterRemovedHandled;
    }

    private void OnCharacterClickedHandled(Character character)
    {
        selectedCharacter = character;
    }

    private void OnCharacterRemovedHandled(Character character)
    {
        if (activeCharacters.Contains(character))
        {
            activeCharacters.Remove(character);
        }
    }
    #endregion

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedCharacter == null) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 99f, groundMask))
            {
                Instantiate(clickFX, hit.point + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
                //Command command = new Command(hit.point, agent); // uncomment this line for Demo 2
                //clickQueue.Enqueue(command); // uncomment this line for Demo 2

                #region Demo 3
                if (selectedCharacter != null)                 // uncomment this line for Demo 3
                {                                              // uncomment this line for Demo 3
                    selectedCharacter.QueueCommand(hit.point); // uncomment this line for Demo 3
                }                                              // uncomment this line for Demo 3
                #endregion
            }
        }

        //ProcessQueue(); // uncomment this line for Demo 2

        #region Demo 3
        ProcessActiveCharactersQueue(); // uncomment this line for Demo 3
        #endregion
    }

    private void ProcessQueue()
    {
        if (activeCommand != null && !activeCommand.isFinished) return;

        if (clickQueue.Count == 0) return;

        activeCommand = clickQueue.Dequeue();
        activeCommand.Execute();
    }

    #region Demo 3
    private void AddCharacterToActiveCharacters(Character character)
    {
        if (activeCharacters.Contains(character)) return;

        activeCharacters.Add(character);
    }

    private void ProcessActiveCharactersQueue()
    {
        if (activeCharacters.Count == 0) return;

        for (int i = 0; i < activeCharacters.Count; i++)
        {
            activeCharacters[i].ProcessQueue();
        }
    }
    #endregion
}
