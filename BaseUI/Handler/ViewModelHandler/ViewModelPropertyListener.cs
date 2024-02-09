using System.ComponentModel;

namespace BaseUI.Handler.ViewModelHandler;

/// <summary>
/// Provides a way to listen to a property in a view model
/// </summary>
public class ViewModelPropertyListener(INotifyPropertyChanged viewModel)
{
    /// <summary>
    /// Adds a listener to a property in the view model
    /// </summary>
    /// <param name="propertyName">The name of the property which should be listened</param>
    /// <param name="callBack">The callback-method that should be invoked when the property has changed</param>
    /// <typeparam name="T">The type of the property</typeparam>
    public void AddPropertyListener<T>(string propertyName, Action<T?> callBack)
    {
        viewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName != propertyName) return;
            var property = viewModel.GetType().GetProperty(propertyName);
            if (property == null) return;

            var propertyValue = property.GetValue(viewModel);
            switch (propertyValue)
            {
                case null:
                    callBack(default);
                    break;
                case T value:
                    callBack(value);
                    break;
            }
        };
    }

    public static void AddPropertyListener(INotifyPropertyChanged viewModel, IEnumerable<string> propertyNames,
        Action callBack)
    {
        viewModel.PropertyChanged += (_, args) =>
        {
            if (!propertyNames.Contains(args.PropertyName)) return;
            callBack();
        };
    }
}