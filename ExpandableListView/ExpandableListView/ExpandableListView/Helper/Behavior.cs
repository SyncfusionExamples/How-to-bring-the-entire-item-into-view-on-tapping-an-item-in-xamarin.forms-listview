using System.Linq;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using Syncfusion.ListView.XForms.Control.Helpers;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;

namespace AutoFit
{
    internal class SfListViewAccordionBehavior : Behavior<ContentPage>
    {
        #region Fields

        private Contact tappedItem;
        private Syncfusion.ListView.XForms.SfListView listview;
        private AccordionViewModel AccordionViewModel;
        public VisibleLinesCollection visibleLines;

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
        }

        #endregion

        #region Private Methods

        private void ListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            visibleLines = this.listview.GetVisualContainer().ScrollRows.GetVisibleLines();
            var tappedItemIndex = listview.DataSource.DisplayItems.IndexOf(e.ItemData as Contact);

            ToExpandTappedItem(e.ItemData as Contact);

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

        private void ToExpandTappedItem(Contact ItemData)
        {
            if (tappedItem != null && tappedItem.IsVisible)
            {
                var previousIndex = listview.DataSource.DisplayItems.IndexOf(tappedItem);
                tappedItem.IsVisible = false;
            }

            if (tappedItem == ItemData)
            {
                if (Device.RuntimePlatform == Device.macOS)
                {
                    var previousIndex = listview.DataSource.DisplayItems.IndexOf(tappedItem);
                }
                tappedItem = null;
                return;
            }

            tappedItem = ItemData;
            tappedItem.IsVisible = true;

            if (Device.RuntimePlatform == Device.macOS)
            {
                var firstIndex = visibleLines[visibleLines.FirstBodyVisibleIndex].LineIndex;
                var lastIndex = visibleLines[visibleLines.LastBodyVisibleIndex].LineIndex;
            }
            else
            {
                var currentIndex = listview.DataSource.DisplayItems.IndexOf(ItemData);
            }

        }

        #endregion

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            listview.ItemTapped -= ListView_ItemTapped;
        }
    }
}
