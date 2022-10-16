using JournalModels;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace JournalApp.Converters
{
    public class IsNotCurrentResearcherConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var currentResearcher = DependencyService.Get<Researcher>();
            return (long)value != currentResearcher.Id;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
