# mauiUI Branch — CommunityToolkit.Maui Rich UI Components

Ye branch `MauiApp2` project mein ek naya page **`UiShowcasePage`** add karta hai jahan
CommunityToolkit.Maui aur Maui.DataGrid ke rich components ek real feature (Products) ke
sath demo kiye gaye hain, taake unka use-pattern samajh aaye.

## Naye Files

| File | Kaam |
|---|---|
| `Views/UiShowcasePage.xaml(.cs)` | Sab components ka playground page |
| `Views/Popups/ProductQuickViewPopup.xaml(.cs)` | `CommunityToolkit.Maui.Views.Popup` ka custom card popup |
| `ViewModels/UiShowcaseViewModel.cs` | Toast/Snackbar trigger + Products binding |

## Kaunse Components use hue hain

### 1. Toast — `CommunityToolkit.Maui.Alerts.Toast`
Auto-dismiss chhota message. ViewModel se hi trigger ho sakta hai kyunke ye Page se
independent hai:
```csharp
var toast = Toast.Make("message", ToastDuration.Short, 14);
await toast.Show();
```

### 2. Snackbar — `CommunityToolkit.Maui.Alerts.Snackbar`
Toast jesa hi lekin ek action button ke sath (e.g. Undo/Retry):
```csharp
var snackbar = Snackbar.Make("message", action: async () => {...}, actionButtonText: "Retry",
    duration: TimeSpan.FromSeconds(4), visualOptions: new SnackbarOptions {...});
await snackbar.Show();
```

### 3. Popup — `CommunityToolkit.Maui.Views.Popup`
v9+ mein `Popup` ek `View`-based class hai (Page nahi), is liye **show karne ke liye
hamesha `Page` chahiye** — yahi wajah hai `ShowPopupAsync` call ViewModel mein nahi,
`UiShowcasePage.xaml.cs` (code-behind) mein hai:
```csharp
var result = await this.ShowPopupAsync(new ProductQuickViewPopup(product));
```
Popup ke andar `Close(value)` call karne se popup band hota hai aur `value`
`result.Result` ke through wapas caller ko milta hai.

### 4. Expander — `toolkit:Expander`
Accordion-style collapsible card — `Header` tap karne par `Content` show/hide hota hai.
Koi extra code nahi chahiye, pure XAML.

### 5. DataGrid — `Maui.DataGrid` (community package, CommunityToolkit ka hissa nahi)
CommunityToolkit.Maui khud koi grid control nahi deta, is liye `Maui.DataGrid` NuGet
package add kiya gaya hai (`MauiApp2.csproj` dekho). Columns XAML mein declare hote hain
(`PropertyName`, `Title`, `Width`, `StringFormat`), aur `ItemsSource`/`SelectedItem`
normal MVVM binding ki tarah kaam karte hain.

## Navigation
Dashboard page par ek naya button **"🎨 UI Components Playground"** add kiya hai jo
`UiShowcasePage` par navigate karta hai (`GoToUiShowcaseCommand`).

## Zaroori: Build se pehle
1. `dotnet restore` chalao taake `Maui.DataGrid` package resolve ho jaye.
2. Package ka naam/version net access na hone ki wajah se manually verify nahi ho saka —
   agar version mismatch ka error aaye to NuGet par `Maui.DataGrid` search karke latest
   stable version `MauiApp2.csproj` mein update kar dena.
3. `App.xaml` mein `IsNotNullConverter` (CommunityToolkit.Maui.Converters) bhi register
   kiya gaya hai — Popup button ke `IsEnabled` binding ke liye.
