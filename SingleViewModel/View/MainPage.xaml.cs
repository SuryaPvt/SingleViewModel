using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SingleViewModel
{
    public partial class MainPage : ContentPage
    {

        private ObservableCollection<CustomerDetail> _customerDetail;
        private SQLiteAsyncConnection _connection;

        /// <summary>Creating DependecySerive for iSQLite DB
        /// 
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISqliteDBConnection>().GetsqliteConnection();
        }

        /// <summary>On Appearing Load Customer
        /// 
        /// </summary>
        protected override async void OnAppearing()
        {
            await LoadCustomers();
            base.OnAppearing();
        }

        /// <summary>On initial Loading Customer Details
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task LoadCustomers()
        {
            await _connection.CreateTableAsync<CustomerDetail>();
            var customerDetails = await _connection.Table<CustomerDetail>().ToListAsync();
            _customerDetail = new ObservableCollection<CustomerDetail>(customerDetails);
            customerListView.ItemsSource = _customerDetail;
        }

        /// <summary>On New Customer Add initializing new Page once added the new customer triggering event delegate
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnAddCustomer(object sender, System.EventArgs e)
        {
            var page = new Details(new CustomerDetail());

            page.CustomerAdded += (source, customer) =>
            {
                _customerDetail.Add(customer);
            };

            await Navigation.PushAsync(page);
        }
    }
}
