using Engine.Singleton;
using UnityEngine.UI;

public class InputFieldManager : Singleton<InputFieldManager>
{
    #region Fields

    #region MouseSensivity
    private int _verticalSensivity = 10;
    private int _horizontalSensivity = 10;
    #endregion MouseSensivity

    #region Interactions
    private string _shuffle = "f";
    #endregion Interactions

    #endregion Fields

    #region Properties
    public int VerticalSensivity { get { return _verticalSensivity; } set { _verticalSensivity = value; } }
    public int HorizontalSensivity { get { return _horizontalSensivity; } set { _horizontalSensivity = value; } }
    public string Shuffle { get { return _shuffle; } set { _shuffle = value; } }
    #endregion Properties
}
