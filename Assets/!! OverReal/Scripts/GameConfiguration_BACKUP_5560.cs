using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class GameConfiguration : MonoBehaviour {

	static public VR_Controller _VRInput = new OculusController();
=======
public abstract class GameConfiguration {

	static public VR_Controller _VRInput = new OculusController();
	static public Inventary inventary;
>>>>>>> release/RefactoryVRInput
}
