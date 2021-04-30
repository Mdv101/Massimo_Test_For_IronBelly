using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test_GUI : MonoBehaviour
{
    [SerializeField] private TestManager testManager;

    [Header("Add GUI")]
    [SerializeField] private TMP_InputField addAmountInput;
    [SerializeField] private Button addAmountButton;
    
    [SerializeField] private Button addOneButton;

    [Header("Remove GUI")] 
    [SerializeField] private CanvasGroup removeCanvasGroup;
    
    [SerializeField] private TMP_InputField removeAmountInput;
    [SerializeField] private Button removeAmountButton;
    
    [SerializeField] private TMP_InputField removeSpecificInput;
    [SerializeField] private Button removeSpecificButton;
    
    [SerializeField] private Button removeOneButton;

    private void Start()
    {
        CheckToDisableRemoveCanvas();
        
        addOneButton.onClick.AddListener(SpawnOneObject);
        addAmountButton.onClick.AddListener(SpawnObjects);
        
        removeOneButton.onClick.AddListener(RemoveRandomObject);
        removeAmountButton.onClick.AddListener(RemoveAnAmount);
        removeSpecificButton.onClick.AddListener(RemoveSpecificObject);
    }

    private void CheckToDisableRemoveCanvas()
    {
        removeCanvasGroup.interactable = testManager.ActiveObjectsAmount != 0;
    }

    private void SpawnOneObject()
    {
        testManager.SpawnNeighbors(1);
        CheckToDisableRemoveCanvas();
    }

    private void SpawnObjects()
    {
        if (addAmountInput.text == "" || addAmountInput.text == "0")
        {
            return;
        }
        testManager.SpawnNeighbors(int.Parse(addAmountInput.text));
        CheckToDisableRemoveCanvas();
    }

    private void RemoveRandomObject()
    {
        testManager.ReturnRandomNeighborToPool();
        CheckToDisableRemoveCanvas();
    }

    private void RemoveAnAmount()
    {
        if (removeAmountInput.text == "" || removeAmountInput.text == "0")
        {
            return;
        }
        
        testManager.ReturnToPoolAmount(int.Parse(
            removeAmountInput.text));
        CheckToDisableRemoveCanvas();
    }
    
    private void RemoveSpecificObject()
    {
        testManager.ReturnToPool(removeSpecificInput.text);
        CheckToDisableRemoveCanvas();
    }
}
