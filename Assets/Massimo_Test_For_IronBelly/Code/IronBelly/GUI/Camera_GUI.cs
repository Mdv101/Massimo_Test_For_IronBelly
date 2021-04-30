using UnityEngine;
using UnityEngine.UI;

public class Camera_GUI : MonoBehaviour
{
   [SerializeField] private CameraController CameraController;
   [SerializeField] private Button zoomInButton;
   [SerializeField] private Button zoomOutButton;

   [Header("Rotation")] 
   [SerializeField] private Color rotationActiveColor;
   [SerializeField] private Color rotationDeactivateColor;
   [SerializeField] private Button changeRotationStateButton;
   [SerializeField] private Image ChangeRotationStateImage;
   

   private void Awake()
   {
      zoomInButton.onClick.AddListener((() =>
      {
         CameraController.ZoomIn();
      }));
      
      zoomOutButton.onClick.AddListener((() =>
      {
         CameraController.ZoomOut();
      }));
      
      changeRotationStateButton.onClick.AddListener(ChangeRotationState);
   }

   private void ChangeRotationState()
   {
      if (CameraController.ChangeRotationState())
      {
         ChangeRotationStateImage.color = rotationActiveColor;
      }
      else
      {
         ChangeRotationStateImage.color = rotationDeactivateColor;
      }
   }
}
