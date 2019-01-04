using System.Linq;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using Syncfusion.ListView.XForms.Helpers;
using Syncfusion.ListView.XForms.Control.Helpers;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;
using System.Reflection;

namespace SfListViewSample
{
    internal class SfListViewAccordionBehavior : Behavior<ContentPage>
    {
        #region Fields

        private Contact tappedItem;
        private Syncfusion.ListView.XForms.SfListView listview;
        private AccordionViewModel AccordionViewModel;
        VisualContainer visualContainer;
        private ScrollAxisBase scrollRows;

        #endregion

        #region Properties
        public SfListViewAccordionBehavior()
        {
            AccordionViewModel = new AccordionViewModel();
        }

        #endregion

        #region Override Methods

        protected override void OnAttachedTo(ContentPage bindable)
        {
            listview = bindable.FindByName<Syncfusion.ListView.XForms.SfListView>("listView");
            listview.ItemsSource = AccordionViewModel.ContactsInfo;
            listview.ItemTapped += ListView_ItemTapped;
            listview.PropertyChanged += Listview_PropertyChanged;
        }

        private void Listview_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                var selectedItemIndex = listview.DataSource.DisplayItems.IndexOf(listview.SelectedItem);

                visualContainer = listview.GetVisualContainer();
                scrollRows = visualContainer.GetType().GetRuntimeProperties().First(p => p.Name == "ScrollRows").GetValue(visualContainer) as ScrollAxisBase;
                var visibleLines = scrollRows.GetVisibleLines();

                if (visibleLines.Count <= 0)
                    return;
                var endIndex = visibleLines[visibleLines.LastBodyVisibleIndex].LineIndex;

                if (selectedItemIndex == endIndex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Task.Delay(200);
                        (listview.LayoutManager as LinearLayout).ScrollToRowIndex(selectedItemIndex, Syncfusion.ListView.XForms.ScrollToPosition.End, true);
                    });
                }
            }

        }

        #endregion

        #region Private Methods

        private void ListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (tappedItem != null && tappedItem.IsVisible)
            {
                var previousIndex = listview.DataSource.DisplayItems.IndexOf(tappedItem);

                tappedItem.IsVisible = false;

                if (Device.RuntimePlatform != Device.macOS)
                    Device.BeginInvokeOnMainThread(() => { listview.RefreshListViewItem(previousIndex, previousIndex, false); });
            }

            if (tappedItem == (e.ItemData as Contact))
            {
                if (Device.RuntimePlatform == Device.macOS)
                {
                    var previousIndex = listview.DataSource.DisplayItems.IndexOf(tappedItem);
                    Device.BeginInvokeOnMainThread(() => { listview.RefreshListViewItem(previousIndex, previousIndex, false); });
                }

                tappedItem = null;
                return;
            }

            tappedItem = e.ItemData as Contact;
            tappedItem.IsVisible = true;

            if (Device.RuntimePlatform == Device.macOS)
            {
                var visibleLines = this.listview.GetVisualContainer().ScrollRows.GetVisibleLines();
                var firstIndex = visibleLines[visibleLines.FirstBodyVisibleIndex].LineIndex;
                var lastIndex = visibleLines[visibleLines.LastBodyVisibleIndex].LineIndex;
                Device.BeginInvokeOnMainThread(() => { listview.RefreshListViewItem(firstIndex, lastIndex, false); });
            }
            else
            {
                var currentIndex = listview.DataSource.DisplayItems.IndexOf(e.ItemData);
                Device.BeginInvokeOnMainThread(() => { listview.RefreshListViewItem(currentIndex, currentIndex, false); });
            }
        }

        #endregion

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            listview.ItemTapped -= ListView_ItemTapped;
        }
    }
}

