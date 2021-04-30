using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class FindNearestNeighbour_Tests
{
    private PositionHandler positionHandler;
    private FindNearestNeighbour prefab;
    private AreaOfMove areaOfMove;
    
    [SetUp]
    public void SetUp()
    { 
        positionHandler = new PositionHandler();
        prefab = Resources.Load<FindNearestNeighbour>("TestResources/GameObject_FindNearestNeighbour");
        areaOfMove = new AreaOfMove();
    }

    [UnityTest]
    public IEnumerator WhenInstantiateObjectOfTypeFindNearestNeighbour_ThenPositionHandlerRegisterNewFindNearestNeighbour()
    {
        //Act
        FindNearestNeighbour findNearestNeighbour = Object.Instantiate(prefab);
        findNearestNeighbour.GetComponent<RandomMove>().Initialize(areaOfMove);
        yield return 0;
        //Asserts
        Assert.IsTrue(positionHandler.ItIsRegistered(findNearestNeighbour), 
            "do not register the instantiated FindNearestNeighbour");
    }
    
    [UnityTest]
    public IEnumerator WhenDisableFindNearestNeighbour_ThenFindDeregisterNeighbour()
    {
        //Act
        FindNearestNeighbour findNearestNeighbour = Object.Instantiate(prefab);
        findNearestNeighbour.GetComponent<RandomMove>().Initialize(areaOfMove);
        yield return 0;
        findNearestNeighbour.Deregister();
        yield return 0;
        //Asserts
        Assert.IsFalse(positionHandler.ItIsRegistered(findNearestNeighbour), 
            "it is registered deactivate FindNearestNeighbour");
    }
    
    [UnityTest]
    public IEnumerator WhenMoveFindNearestNeighbour_ThenNearestNeighbourUpdate()
    {
        FindNearestNeighbour firstNearestNeighbour = Object.Instantiate(prefab);
        firstNearestNeighbour.GetComponent<RandomMove>().gameObject.SetActive(false);
        FindNearestNeighbour secondNearestNeighbour = Object.Instantiate(prefab);
        secondNearestNeighbour.GetComponent<RandomMove>().gameObject.SetActive(false);
        FindNearestNeighbour thirdNearestNeighbour = Object.Instantiate(prefab);
        thirdNearestNeighbour.GetComponent<RandomMove>().gameObject.SetActive(false);

        //Act & Asserts
        firstNearestNeighbour.transform.position = Vector3.zero;
        secondNearestNeighbour.transform.position = Vector3.one;
        thirdNearestNeighbour.transform.position = new Vector3(10,10,10);

        positionHandler.UpdatePositions();
        yield return 0;
        Assert.AreSame(secondNearestNeighbour, firstNearestNeighbour.CurrentNearestNeighbour, "No the correct neighbour");
        yield return 0;
        
        secondNearestNeighbour.transform.position = new Vector3(11,11,11);
        yield return 0;
        positionHandler.UpdatePositions();
        yield return 0;
        Assert.AreSame(thirdNearestNeighbour, firstNearestNeighbour.CurrentNearestNeighbour, "No the correct neighbour");
        
    }  
}
