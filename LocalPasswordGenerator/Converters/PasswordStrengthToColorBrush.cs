
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LocalPasswordGenerator.Converters;

// Deleting this causes a weird build error in a .g.cs file that I've never seen before. 
// I will need to figure out why this is happening, but for now just leaving it here. 

public class PasswordStrengthToColorBrush : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value != null && int.TryParse(value.ToString(), out int parsedScore)) {
            switch (parsedScore) {
                case 0:
                    return Brushes.Red; // Very Weak
                case 1:
                    return Brushes.Orange; // Weak
                case 2:
                    return Brushes.Yellow; // Okay
                case 3:
                    return Brushes.Green; // Strong
                case 4:
                    return Brushes.Blue; // Very Strong
                default:
                    return Brushes.Transparent; // Default color
            }
        }
        else {
            return Brushes.Transparent;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
