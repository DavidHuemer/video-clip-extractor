﻿using System.Text.Json.Serialization;
using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Extractions.Basics;

public abstract class BaseExtractionViewModel : BaseViewModel, IExtractionViewModel
{
    private Action<IExtractionViewModel>? _selectionCallback;

    [JsonIgnore] public ICommand Select => new RelayCommand<string>(DoSelect, _ => true);

    public bool IsSelected { get; set; }

    public abstract VideoPosition Position { get; set; }

    public virtual void SetupSelection(Action<IExtractionViewModel> selectionCallback)
    {
        _selectionCallback = selectionCallback;
    }

    private void DoSelect(string? obj) => _selectionCallback?.Invoke(this);
}