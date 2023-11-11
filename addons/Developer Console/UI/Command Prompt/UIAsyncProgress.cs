using Godot;
using System;
using hamsterbyte.DeveloperConsole;

public partial class UIAsyncProgress : ProgressBar, ICanInitialize{
    public void Initialize(){
        bool success = TryInitialize();
        if (success) DC.Print($"UIAsyncProgress => {success.OKFail()}");
    }

    public bool TryInitialize(){
        try{
            SetupCallbacks();
        }
        catch (Exception e){
            e.PrintToDC();
            return false;
        }

        return true;
    }

    private void SetupCallbacks(){
        DC.OnShowProgress += ShowProgress;
        DC.OnIncrementProgress += IncrementProgress;
        ValueChanged += Complete;
        Command.OnCancel += Cancel;
    }

    private void Complete(double value){
        if (!(value >= MaxValue)) return;
        Value = 0;
        Hide();
    }

    private void Cancel(){
        Complete(MaxValue);
    }

    private void ShowProgress(int maxValue){
        Value = 0;
        MaxValue = maxValue;
        Visible = true;
    }
    
    private void IncrementProgress(int i){

        Value += i;
    }

}