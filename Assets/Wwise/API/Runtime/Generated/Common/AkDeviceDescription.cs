#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.3.0
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class AkDeviceDescription : global::System.IDisposable {
  private global::System.IntPtr swigCPtr;
  protected bool swigCMemOwn;

  internal AkDeviceDescription(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  internal static global::System.IntPtr getCPtr(AkDeviceDescription obj) {
    return (obj == null) ? global::System.IntPtr.Zero : obj.swigCPtr;
  }

  internal virtual void setCPtr(global::System.IntPtr cPtr) {
    Dispose();
    swigCPtr = cPtr;
  }

  ~AkDeviceDescription() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkUnitySoundEnginePINVOKE.CSharp_delete_AkDeviceDescription(swigCPtr);
        }
        swigCPtr = global::System.IntPtr.Zero;
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public uint idDevice { set { AkUnitySoundEnginePINVOKE.CSharp_AkDeviceDescription_idDevice_set(swigCPtr, value); }  get { return AkUnitySoundEnginePINVOKE.CSharp_AkDeviceDescription_idDevice_get(swigCPtr); } 
  }

  public string deviceName { set { AkUnitySoundEnginePINVOKE.CSharp_AkDeviceDescription_deviceName_set(swigCPtr, value); }  get { return AkUnitySoundEngine.StringFromIntPtrOSString(AkUnitySoundEnginePINVOKE.CSharp_AkDeviceDescription_deviceName_get(swigCPtr)); } 
  }

  public AkAudioDeviceState deviceStateMask { set { AkUnitySoundEnginePINVOKE.CSharp_AkDeviceDescription_deviceStateMask_set(swigCPtr, (int)value); }  get { return (AkAudioDeviceState)AkUnitySoundEnginePINVOKE.CSharp_AkDeviceDescription_deviceStateMask_get(swigCPtr); } 
  }

  public bool isDefaultDevice { set { AkUnitySoundEnginePINVOKE.CSharp_AkDeviceDescription_isDefaultDevice_set(swigCPtr, value); }  get { return AkUnitySoundEnginePINVOKE.CSharp_AkDeviceDescription_isDefaultDevice_get(swigCPtr); } 
  }

  public void Clear() { AkUnitySoundEnginePINVOKE.CSharp_AkDeviceDescription_Clear(swigCPtr); }

  public static int GetSizeOf() { return AkUnitySoundEnginePINVOKE.CSharp_AkDeviceDescription_GetSizeOf(); }

  public void Clone(AkDeviceDescription other) { AkUnitySoundEnginePINVOKE.CSharp_AkDeviceDescription_Clone(swigCPtr, AkDeviceDescription.getCPtr(other)); }

  public AkDeviceDescription() : this(AkUnitySoundEnginePINVOKE.CSharp_new_AkDeviceDescription(), true) {
  }

}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.