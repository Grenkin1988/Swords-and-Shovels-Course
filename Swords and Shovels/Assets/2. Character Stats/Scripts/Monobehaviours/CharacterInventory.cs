using UnityEngine;

public class CharacterInventory : MonoBehaviour {
    #region Variable Declarations
    public static CharacterInventory instance;
    #endregion

    #region Initializations
    private void Start() {
        instance = this;
    }
    #endregion
}