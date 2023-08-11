using System;
using System.Threading;
using CommunityToolkit.Mvvm.Input;

namespace MauiCalculator.Controls
{
    public class HoldableButton : Button
    {
        private bool isHolding;
        private DateTime pressStartTime;
        private CancellationTokenSource tokenSource;

        public static readonly BindableProperty ClickAndHoldCommandProperty = BindableProperty.Create(
            nameof(ClickAndHoldCommand), typeof(RelayCommand), typeof(HoldableButton), null);

        public RelayCommand ClickAndHoldCommand
        {
            get { return (RelayCommand)GetValue(ClickAndHoldCommandProperty); }
            set { SetValue(ClickAndHoldCommandProperty, value); }
        }

        public HoldableButton()
        {
            Pressed += OnPressed;
            Released += OnReleased;
        }

        private void OnPressed(object sender, EventArgs e)
        {
            isHolding = true;
            pressStartTime = DateTime.Now;

            tokenSource = new CancellationTokenSource();
            _ = CheckHolding(tokenSource.Token);
        }

        private async Task CheckHolding(CancellationToken token)
        {
            await Task.Delay(500, token); // Adjust the delay time here (1 second)

            if (isHolding && !token.IsCancellationRequested)
            {
                ClickAndHoldCommand?.Execute(CommandParameter);
            }
        }

        private void OnReleased(object sender, EventArgs e)
        {
            isHolding = false;

            if (DateTime.Now - pressStartTime < TimeSpan.FromMilliseconds(100))
            {
                if (tokenSource != null)
                {
                    tokenSource.Cancel();
                }
                else
                {
                    Command?.Execute(CommandParameter);
                }
            }
        }
    }
}