using Content.Shared._NF.BountyContracts;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.XAML;

namespace Content.Client._NF.BountyContracts.UI;

[GenerateTypedNameReferences]
public sealed partial class BountyContractUiFragmentList : Control
{
    public event Action? OnCreateButtonPressed;
    public event Action? OnRefreshButtonPressed;
    public event Action<BountyContract>? OnRemoveButtonPressed;
    public BountyContractUiFragmentList()
    {
        RobustXamlLoader.Load(this);
        CreateButton.OnPressed += _ => OnCreateButtonPressed?.Invoke();
        RefreshButton.OnPressed += _ => OnRefreshButtonPressed?.Invoke();
    }

    public void SetContracts(List<BountyContract> listStateContracts, bool canRemove)
    {
        BountiesContainer.RemoveAllChildren();

        if (listStateContracts.Count == 0)
        {
            NoContractsLabel.Visible = true;
            return;
        }

        NoContractsLabel.Visible = false;
        foreach (var contract in listStateContracts)
        {
            var entry = new BountyContractUiFragmentListEntry(contract, canRemove);
            entry.OnRemoveButtonPressed += c => OnRemoveButtonPressed?.Invoke(c);
            BountiesContainer.AddChild(entry);
        }
    }

    public void SetCanCreate(bool canCreate)
    {
        CreateButton.Disabled = !canCreate;
    }
}
