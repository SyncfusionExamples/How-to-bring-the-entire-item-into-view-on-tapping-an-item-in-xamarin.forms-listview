using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace Accordion
{
    public class AccordionViewModel : INotifyPropertyChanged
    {
        #region Fields

        int counter = 11;

        #endregion

        #region Properties

        public ObservableCollection<Contact> ContactsInfo { get; set; }

        #endregion

        #region Constructor

        public AccordionViewModel()
        {
            ContactsInfo = new ObservableCollection<Contact>();
            Assembly assembly = typeof(Accordion.MainPage).GetTypeInfo().Assembly;
            int i = 0;
            foreach (var cusName in CustomerNames)
            {
                if (counter == 13)
                    counter = 1;
                var contact = new Contact(cusName);
                contact.CallTime = CallTime[i];
                contact.PhoneImage = ImageSource.FromResource("Sample.Images.PhoneImage.png", assembly);
                contact.ContactImage = ImageSource.FromResource("Sample.Images.Image" + counter + ".png", assembly);
                contact.AddContact = ImageSource.FromResource("Sample.Images.AddContact.png", assembly);
                contact.NewContact = ImageSource.FromResource("Sample.Images.NewContact.png", assembly);
                contact.SendMessage = ImageSource.FromResource("Sample.Images.SendMessage.png", assembly);
                contact.BlockSpan = ImageSource.FromResource("Sample.Images.BlockSpan.png", assembly);
                contact.CallDetails = ImageSource.FromResource("Sample.Images.CallDetails.png", assembly);
                i++;
                ContactsInfo.Add(contact);
                counter++;
            }
        }

        #endregion

        #region Fields

        string[] CustomerNames = new string[] {
            "Kyle",
            "Gina",
            "Irene",
            "Katie",
            "Oscar",
            "Ralph",
            "Torrey",
            "William",
            "Bill",
            "Daniel",
            "Frank",
            "Brenda",
            "Danielle",
            "Fiona",
            "Howard",
            "Jack",
            "Larry",
            "Holly",
            "Liz",
            "Pete",
            "Steve",
            "Vince",
            "Katherin",
            "Aliza",
            "Masona" ,
            "Lia" ,
            "Jacob  " ,
            "Jayden " ,
            "Ethani  " ,
            "Noah   " ,
            "Lucas  " ,
            "Logan  " ,
            "John  " ,
        };

        string[] CallTime = new string[]
        {
            "India, 1 days ago",
            "India, 1 days ago",
            "India, 1 days ago",
            "India, 1 days ago",
            "India, 1 days ago",
            "India, 2 days ago",
            "India, 2 days ago",
            "India, 2 days ago",
            "India, 2 days ago",
            "India, 2 days ago",
            "India, 3 days ago",
            "India, 3 days ago",
            "India, 3 days ago",
            "India, 3 days ago",
            "India, 3 days ago",
            "India, 4 days ago",
            "India, 4 days ago",
            "India, 4 days ago",
            "India, 4 days ago",
            "India, 4 days ago",
            "India, 5 days ago",
            "India, 5 days ago",
            "India, 5 days ago",
            "India, 5 days ago",
            "India, 5 days ago",
            "India, 6 days ago",
            "India, 6 days ago",
            "India, 6 days ago",
            "India, 6 days ago",
            "India, 6 days ago",
            "India, 1 week ago",
            "India, 1 week ago",
            "India, 1 week ago"
        };

        #endregion

        #region Interface Member

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
