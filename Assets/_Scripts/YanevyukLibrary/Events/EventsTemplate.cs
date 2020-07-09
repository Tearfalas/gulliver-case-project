using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Category Template for Events //

/// <summary>
/// Changing the class name will break the functionality of the OnEvent attribute.
/// The class and its variables should NOT be public. You do not have to instantiate this class,
/// it is automatic.
/// </summary>

namespace YanevyukLibrary.Events{
		public class Events
		{
				public static Events Instance;


				#region Character Events
				public static Event characterDashed;
				public static Event characterJumped;
				public static Event characterStartedRunning;


				/// <summary>
				/// Triggered when the character hits a wall and is pushed back. 
				/// </summary>
				public static Event characterGotPushed;
				#endregion


				#region  World Events
				public static Event coinPickedUp;
				public static Event finishReached;
				public static Event levelLoaded;
				#endregion

		}
}

