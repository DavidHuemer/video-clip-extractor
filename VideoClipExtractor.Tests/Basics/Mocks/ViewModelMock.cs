using System.ComponentModel;
using BaseUI.ViewModels;
using Moq;

namespace VideoClipExtractor.Tests.Basics.Mocks;

public class ViewModelMock<T> : Mock<T> where T : class, IBaseViewModel
{
    public void RaisePropertyChanged(string propertyName) =>
        Raise(x => x.PropertyChanged += null, new PropertyChangedEventArgs(propertyName));
}