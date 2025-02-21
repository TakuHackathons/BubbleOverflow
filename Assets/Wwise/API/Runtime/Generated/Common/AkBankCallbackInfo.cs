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


public class AkBankCallbackInfo : global::System.IDisposable {
  private global::System.IntPtr swigCPtr;
  protected bool swigCMemOwn;

  internal AkBankCallbackInfo(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  internal static global::System.IntPtr getCPtr(AkBankCallbackInfo obj) {
    return (obj == null) ? global::System.IntPtr.Zero : obj.swigCPtr;
  }

  internal virtual void setCPtr(global::System.IntPtr cPtr) {
    Dispose();
    swigCPtr = cPtr;
  }

  ~AkBankCallbackInfo() {
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
          AkUnitySoundEnginePINVOKE.CSharp_delete_AkBankCallbackInfo(swigCPtr);
        }
        swigCPtr = global::System.IntPtr.Zero;
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public uint bankID { get { return AkUnitySoundEnginePINVOKE.CSharp_AkBankCallbackInfo_bankID_get(swigCPtr); } 
  }

  public global::System.IntPtr inMemoryBankPtr { get { return AkUnitySoundEnginePINVOKE.CSharp_AkBankCallbackInfo_inMemoryBankPtr_get(swigCPtr); }
  }

  public AKRESULT loadResult { get { return (AKRESULT)AkUnitySoundEnginePINVOKE.CSharp_AkBankCallbackInfo_loadResult_get(swigCPtr); } 
  }

  public AkBankCallbackInfo() : this(AkUnitySoundEnginePINVOKE.CSharp_new_AkBankCallbackInfo(), true) {
  }

}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.