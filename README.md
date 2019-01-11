# How to bring the entire listview item into view on tapping an item in xamarin.forms listview configured as expandable listview?

This example demonstrates how to bring the entire listview item into view on tapping an item in xamarin.forms listview configured as expandable listview.

You can call [ScrollToRowIndex](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.LayoutBase~ScrollToRowIndex.html) method when an item is tapped and bring that item into view if it is not fully visible. 

```
namespace Accordion
{
    internal class SfListViewAccordionBehavior : Behavior<SfListView>
    {
       
        protected override void OnAttachedTo(SfListView bindable)
        {
            listview = bindable.FindByName<Syncfusion.ListView.XForms.SfListView>("listView");
            listview.ItemTapped += ListView_ItemTapped;
        }
        
        private void ListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
             ToExpandTappedItem(e.ItemData as Contact);   
            
           var tappedItemIndex = listview.DataSource.DisplayItems.IndexOf(e.ItemData as Contact);
            if (visibleLines.Count <= 0)
                return;
            var endIndex = visibleLines[visibleLines.LastBodyVisibleIndex].LineIndex;
            if (tappedItemIndex == endIndex) 
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(200);
                    (listview.LayoutManager as LinearLayout).ScrollToRowIndex(tappedItemIndex, Syncfusion.ListView.XForms.ScrollToPosition.End, true);
                });
            }
        }
    }
}

```

## Requirements to run the demo

* [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/) or [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/).
* Xamarin add-ons for Visual Studio (available via the Visual Studio installer).

## Troubleshooting

### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.
