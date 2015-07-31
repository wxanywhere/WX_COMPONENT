
namespace WX.Windows.Diagram.Designer
{
    // Common interface for items that can be selected
    // on the DesignCanvas; used by DesignItem and Connection
    public interface ISelectable
    {
        bool IsSelected { get; set; }
    }
}
