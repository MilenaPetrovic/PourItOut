using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace PourItOut.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://github.com/MilenaPetrovic/PourItOut")));
        }

        public ICommand OpenWebCommand { get; }
    }
}