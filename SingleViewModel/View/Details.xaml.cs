using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SingleViewModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Details : ContentPage
	{
        public event EventHandler<CustomerDetail> CustomerAdded;
        private SQLiteAsyncConnection _connection;

        public Details (CustomerDetail customer)
		{
			InitializeComponent ();
            _connection = DependencyService.Get<ISqliteDBConnection>().GetsqliteConnection();
          
            BindingContext = new CustomerDetail
            {
                ID   = customer.ID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Phone = customer.Phone,
                Email = customer.Email,
                IDNumber = customer.IDNumber,
                Age = customer.Age
            };
        }

        /// <summary>Adding New Customer
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnSave(object sender, System.EventArgs e)
        {
            var customer = BindingContext as CustomerDetail;

            if (String.IsNullOrWhiteSpace(customer.FirstName) || String.IsNullOrWhiteSpace(customer.LastName))
            {
                await DisplayAlert("Error", "Please enter the first and last name.", "OK");
                return;
            }

            if (IsIDValid(customer.IDNumber)<0)
            {
                await DisplayAlert("Error", "Invalid ID Number.", "OK");
                return;
            }

            if (customer.Age <= 0)
            {
                await DisplayAlert("Error", "Invalid Age Number.", "OK");
                return;
            }

            if (customer.ID == 0)
            {
                await _connection.InsertAsync(customer);

                CustomerAdded?.Invoke(this, customer);
            }
            await Navigation.PopAsync();
        }

        /// <summary>ID Number Validation
        /// 
        /// </summary>
        /// <param name="IDNumber"></param>
        /// <returns></returns>
        private int IsIDValid(string IDNumber)
        {
            int d = -1;
            try
            {
                int a = 0;
                for (int i = 0; i < 6; i++)
                {
                    a += int.Parse(IDNumber[2 * i].ToString());
                }
                int b = 0;
                for (int i = 0; i < 6; i++)
                {
                    b = b * 10 + int.Parse(IDNumber[2 * i + 1].ToString());
                }
                b *= 2;
                int c = 0;
                do
                {
                    c += b % 10;
                    b = b / 10;
                }
                while (b > 0);
                c += a;
                d = 10 - (c % 10);
                if (d == 10) d = 0;
            }
            catch {/*ignore*/}
            return d;
        }
    }
}