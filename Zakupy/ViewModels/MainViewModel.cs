using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Zakupy.Data;
using Zakupy.Models;

namespace Zakupy.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly ZakupyContext context;

    public MainViewModel(ZakupyContext context)
    {
        this.context = context;
        Purchases = context.Purchases
            .LoadAsync()
            .ContinueWith(p => context.Purchases.Local.ToObservableCollection());
    }

    [RelayCommand]
    private async Task AddPurchaseAsync()
    {
        if (NewPurchase.Validate())
        {
            context.Add(NewPurchase);
            await context.SaveChangesAsync();
            NewPurchase = new Purchase();
        }
    }

    [RelayCommand]
    private async Task DeletePurchaseAsync(Purchase purchase)
    {
        context.Remove(purchase);
        await context.SaveChangesAsync();
    }

    [ObservableProperty]
    private Purchase newPurchase = new Purchase();

    private TaskNotifier<ObservableCollection<Purchase>> purchases;
    public Task<ObservableCollection<Purchase>> Purchases
    {
        get => purchases;
        set => SetPropertyAndNotifyOnCompletion(ref purchases, value);
    }
}
