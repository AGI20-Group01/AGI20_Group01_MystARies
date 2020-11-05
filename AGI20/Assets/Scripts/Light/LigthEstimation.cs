 using UnityEngine;
 using UnityEngine.XR.ARFoundation;
 
     public class LigthEstimation : MonoBehaviour {
          [SerializeField] 
          private ARCameraManager arCameraManager;
          private Light mainLight;
     
          public float? Brightness { get; private set; }
          public float? ColorTemperature { get; private set; }
          public Color? ColorCorrection { get; private set; }
     
          private void Awake () {
               mainLight = GetComponent<Light> ();
               
          }
     
          private void OnEnable () {
               arCameraManager.frameReceived += FrameUpdated;
          }
     
          private void OnDisable () {
               arCameraManager.frameReceived -= FrameUpdated;
          }
     
          private void FrameUpdated (ARCameraFrameEventArgs args) {
               if (args.lightEstimation.averageBrightness.HasValue) {
                    Brightness = args.lightEstimation.averageBrightness.Value;
                    mainLight.intensity = Brightness.Value;
                    RenderSettings.ambientLight = new Vector4(1, 1, 1, 1) * Brightness.Value;
               }

               if (args.lightEstimation.averageColorTemperature.HasValue) {
                    ColorTemperature = args.lightEstimation.averageColorTemperature.Value;
                    Debug.Log (">>>>" + ColorTemperature.Value);
                    mainLight.colorTemperature = ColorTemperature.Value;
               }
     
               if (args.lightEstimation.colorCorrection.HasValue) {
                    ColorCorrection = args.lightEstimation.colorCorrection.Value;
                    Debug.Log (">>>>" + ColorCorrection.Value);
                    mainLight.color = ColorCorrection.Value;
               }
          }
     
     }