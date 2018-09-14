using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KomunikatyRSOUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Ogolne : BasePage
    {
        public Ogolne()
        {
            this.InitializeComponent();
            ShowReviewReminder();
            Init();
        }

        private string IsReviewed = "isreviewed";
        private string LastReviewRemind = "lastreviewremind";

        private async Task ShowReviewReminder()
        {
            await Task.Delay(2000);
            try
            {
                var settings = Windows.Storage.ApplicationData.Current.LocalSettings;

                if (!settings.Values.ContainsKey(IsReviewed))
                {
                    settings.Values.Add(IsReviewed, 0);
                    settings.Values.Add(LastReviewRemind, DateTime.Today.Ticks);
                }
                else
                {
                    int isReviewed = Convert.ToInt32(settings.Values[IsReviewed]);
                    long dateticks = (long)(settings.Values[LastReviewRemind]);
                    TimeSpan elapsed = TimeSpan.FromTicks(DateTime.Today.Ticks - dateticks);
                    if (isReviewed >= 0 && isReviewed < 2 && TimeSpan.FromDays(14) <= elapsed)//!!!!!!!!! <=
                    {
                        settings.Values[LastReviewRemind] = DateTime.Today.Ticks;
                        settings.Values[IsReviewed] = isReviewed++;

                        MessageDialog dialog = new MessageDialog("Jeśli podoba Ci się aplikacja, może zechciałbyś poświęcić chwilę czasu, aby ją ocenić?");
                        dialog.Title = "Oceń Komunikaty RSO";
                        dialog.Commands.Add(new UICommand("Tak") { Id = 0 });
                        dialog.Commands.Add(new UICommand("Później") { Id = 1 });
                        dialog.DefaultCommandIndex = 0;
                        dialog.CancelCommandIndex = 1;

                        await dialog.ShowAsync();
                    }
                }
            }
            catch (Exception) { }
        }
    }
}
