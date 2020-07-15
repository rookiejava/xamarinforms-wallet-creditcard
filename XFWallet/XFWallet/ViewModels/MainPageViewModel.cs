using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XFWallet.Helpers;
using XFWallet.Models;
using XFWallet.ViewModel;
using XFWallet.Views;

namespace XFWallet.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            Cards = new ObservableCollection<Card>();
            Stores = new ObservableCollection<Store>();
            StoresSearch = new ObservableCollection<Store>();
            OpenLinkCommand = new Command<Store>(async (param) => await ExecuteOpenLinkCommand(param));
            SearchCommand = new Command(async () => await ExecuteSearchCommand());
            ClearSearchTextCommand = new Command(async () => await ExecuteClearSearchTextCommand());
            NavigateToAddCreditCardPageCommand = new Command(async () => await ExecuteNavigateToAddCreditCardPageCommand());
            GetCards();
            GetStores();
            IsEmpty = StoresSearch.Count == 0;
        }

        public Command OpenLinkCommand { get; }
        public Command SearchCommand { get; }
        public Command NavigateToAddCreditCardPageCommand { get; }
        public Command ClearSearchTextCommand { get; }
        public ObservableCollection<Card> Cards { get; set; }
        public ObservableCollection<Store> Stores { get; set; }
        public ObservableCollection<Store> StoresSearch { get; set; }

        public ScrollView ScrollView { get; set; }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                SetProperty(ref _isEmpty, value);
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    if (string.IsNullOrEmpty(_searchText))
                        SearchCommand.Execute(_searchText);
                }
            }
        }

        private bool _isVisibleBtnClose;
        public bool IsVisibleBtnClose
        {
            get { return _isVisibleBtnClose; }
            set { SetProperty(ref _isVisibleBtnClose, value); }
        }

        void GetCards()
        {
            Cards.Add(new Card()
            {
                cardNumber = "5555666677778884",
                cardName = "ALTEVIR CARDOSO NETO",
                cardFlag = "mastercard.png"
            });

            Cards.Add(new Card()
            {
                cardNumber = "4012001037141112",
                cardName = "CHETAN SINGH",
                cardFlag = "visa.png"
            });

            Cards.Add(new Card()
            {
                cardNumber = "376449047333005",
                cardName = "XAMARIN FORMS",
                cardFlag = "amex.png"
            });

            SetCardBackground();
        }

        void GetStores()
        {
            Stores.Add(new Store()
            {
                name = "McDonalds",
                link = "https://www.mcdonalds.com/us/en-us.html",
                image = "mcdonalds.png"
            });

            Stores.Add(new Store()
            {
                name = "Burger King",
                link = "https://www.bk.com",
                image = "bergerking.png"
            });

            Stores.Add(new Store()
            {
                name = "Jack In The Box",
                link = "https://www.jackinthebox.com/food",
                image = "jackinthebox.png"
            });

            //Stores.Add(new Store()
            //{
            //    name = "Subway",
            //    link = "https://www.subway.com/en-US/MenuNutrition/Menu",
            //    image = "subway.png"
            //});

            //Stores.Add(new Store()
            //{
            //    name = "Wendy's",
            //    link = "https://order.wendys.com/categories?site=menu",
            //    image = "wendys.png"
            //});

            //Stores.Add(new Store()
            //{
            //    name = "Dunkin' Donuts",
            //    link = "https://www.dunkindonuts.com/en",
            //    image = "dunkindonuts.png"
            //});


            //Stores.Add(new Store()
            //{
            //    name = "IN-N-OUT BURGER",
            //    link = "https://www.in-n-out.com",
            //    image = "inanout.png"
            //});

            //Stores.Add(new Store()
            //{
            //    name = "FIVE GUYS",
            //    link = "https://www.fiveguys.com",
            //    image = "fiveguys.png"
            //});

            //Stores.Add(new Store()
            //{
            //    name = "SHAKE SHACK",
            //    link = "https://www.shakeshack.com",
            //    image = "shakeshack.png"
            //});

            Stores.Add(new Store()
            {
                name = "Starbucks",
                link = "https://www.starbucks.com/",
                image = "starbucks.png"
            });

            Stores.Add(new Store()
            {
                name = "Blue Bottle Coffee",
                link = "https://bluebottlecoffee.com",
                image = "bluebottle.png"
            });

            Stores.Add(new Store()
            {
                name = "Cheesecake Factory",
                link = "https://www.thecheesecakefactory.com",
                image = "cheesecakefactory.png"
            });

            Stores.Add(new Store()
            {
                name = "Chipotle",
                link = "https://www.chipotle.com",
                image = "chipotle.png"
            });

            //Stores.Add(new Store()
            //{
            //    name = "Careem",
            //    link = "https://www.careem.com/",
            //    image = "careem.png"
            //});

            //Stores.Add(new Store()
            //{
            //    name = "Centrepoint",
            //    link = "https://www.centrepointstores.com/",
            //    image = "centrepoint.png"
            //});

            Stores.Add(new Store()
            {
                name = "Amazon",
                link = "https://www.amazon.com/",
                image = "amazon.png"
            });

            //Stores.Add(new Store()
            //{
            //    name = "ebay",
            //    link = "https://www.ebay.com/",
            //    image = "ebay.png"
            //});

            Stores.Add(new Store()
            {
                name = "Macy's",
                link = "https://www.macys.com/",
                image = "macys.png"
            });

            //Stores.Add(new Store()
            //{
            //    name = "BARNES & NOBLE",
            //    link = "https://www.barnesandnoble.com/",
            //    image = "barnsandnoble.png"
            //});

            Stores.Add(new Store()
            {
                name = "Target",
                link = "https://www.target.com/",
                image = "target.png"
            });

            foreach (var item in Stores)
                StoresSearch.Add(item);
        }

        private async Task ExecuteOpenLinkCommand(Store param)
        {
            await Browser.OpenAsync(param.link, BrowserLaunchMode.SystemPreferred);
        }

        private async Task ExecuteSearchCommand()
        {
            var stores = await Search(SearchText);
            StoresSearch.Clear();
            foreach (var item in stores)
                StoresSearch.Add(item);

            IsEmpty = StoresSearch.Count == 0;
            await ScrollView.ScrollToAsync(0, 0, false);
        }

        Task<List<Store>> Search(string text)
        {
            List<Store> stores = new List<Store>();
            IsVisibleBtnClose = false;

            if (string.IsNullOrEmpty(text))
            {
                foreach (var item in Stores)
                    stores.Add(item);
            }
            else
            {
                var result = Stores.Where(p => p.name.ToLower().Contains(text.ToLower()));
                IsVisibleBtnClose = !result.Any();

                if (result.Any())
                    foreach (var item in result)
                        stores.Add(item);
            }

            return Task.FromResult(stores);
        }

        private async Task ExecuteNavigateToAddCreditCardPageCommand()
        {
            var cardPage = new AddCreditCardPage();
            cardPage.Disappearing += Scroll;

            void Scroll(object sender, EventArgs e)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ScrollView.IsEnabled = true;
                    cardPage.Disappearing -= Scroll;
                });
            };

            await Navigation.PushPopupAsync(cardPage);
            ScrollView.IsEnabled = false;
        }

        public void SubscribeAddCard()
        {
            MessagingCenter.Subscribe<AddCreditCardPageViewModel, Card>(this, "addCard", (s, param) =>
            {
                Cards.Add(param);
                SetCardBackground();
            });
        }

        void SetCardBackground()
        {
            for (int i = 0; i < Cards.Count(); i++)
            {
                if (Helper.IsOddNumber(i))
                    Cards[i].cardBackground = "mask.png";
                else
                    Cards[i].cardBackground = "mask2.png";
            }
        }

        public void UnsubscribedAddCard()
        {
            MessagingCenter.Unsubscribe<AddCreditCardPageViewModel>(this, "addCard");
        }

        private async Task ExecuteClearSearchTextCommand()
        {
            IsVisibleBtnClose = false;
            SearchText = string.Empty;

            var stores = await Search(SearchText);
            StoresSearch.Clear();
            foreach (var item in stores)
                StoresSearch.Add(item);

            IsEmpty = StoresSearch.Count == 0;
            await ScrollView.ScrollToAsync(0, 0, false);
        }
    }
}
