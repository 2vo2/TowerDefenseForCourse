using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIScreen : MonoBehaviour
{
    [SerializeField] protected UIDocument _menuUIDocument;
    [SerializeField] protected VisualTreeAsset _menuVisualTreeAsset;
    
    protected VisualElement _root;
    
    public abstract void Initialize();

    public virtual void ShowMenu()
    {
        _menuUIDocument.visualTreeAsset = _menuVisualTreeAsset;
        _root = _menuUIDocument.rootVisualElement;
        
        Initialize();
    }
}
