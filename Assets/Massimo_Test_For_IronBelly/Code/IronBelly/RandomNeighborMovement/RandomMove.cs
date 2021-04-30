using UnityEngine;

public class RandomMove : MonoBehaviour
{
   private AreaOfMove _areaOfMove;
   private Vector3 target;
   
   public void Initialize(AreaOfMove areaOfMove)
   {
      _areaOfMove = areaOfMove;
      target = _areaOfMove.GetRandomPointInArea();
   }
   
   private void Update()
   {
      Move();
   }

   private void Move()
   {
      float step = _areaOfMove.speed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, target, step);

      if (Vector3.Distance(transform.position, target) < 0.001f)
      {
         target = _areaOfMove.GetRandomPointInArea();
      }
   }
}
