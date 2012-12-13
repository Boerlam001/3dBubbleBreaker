using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comp376_A1
{
  class CameraHolder
  {
    public static Camera camera;
    public static Camera cameraFar;
    public static Camera cameraNear;
    public static void initCamera()
    {
      initCameraFar();
      initCameraNear();
      switchCameras(true);
    }
    public static void initCameraFar()
    {
      Point eye = new Point(0, 0, 90);
      Point at = new Point(0, 10, 0);
      Point up = new Point(0, 1, 0);

      cameraFar = new Camera(eye, at, up);
    }
    public static void initCameraNear()
    {
      Point eye = new Point(0, -5, 12);
      Point at = new Point(0, 20, 0);
      Point up = new Point(0, 0, 1);

      cameraNear = new Camera(eye,at,up);
    }
    public static void switchCameras(bool isFp)
    {
      if (!isFp)
      {
        camera = cameraFar;
      }
      else
      {
        camera = cameraNear;
      }
    }
  }
}
