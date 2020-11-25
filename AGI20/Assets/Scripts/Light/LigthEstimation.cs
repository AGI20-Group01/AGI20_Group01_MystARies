 using UnityEngine;
 using UnityEngine.XR.ARFoundation;
 
     public class LigthEstimation : MonoBehaviour {

          public float lightIntensity = 1;

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
                    mainLight.intensity = Brightness.Value * lightIntensity;
                    RenderSettings.ambientLight = new Vector4(1, 1, 1, 1) * lightIntensity * Brightness.Value;
               }

               if (args.lightEstimation.averageColorTemperature.HasValue) {
                    ColorTemperature = args.lightEstimation.averageColorTemperature.Value;
                    mainLight.colorTemperature = ColorTemperature.Value;
               }
     
               if (args.lightEstimation.colorCorrection.HasValue) {
                    ColorCorrection = args.lightEstimation.colorCorrection.Value;
                    mainLight.color = ColorCorrection.Value;
               }
          }
     
     }