using CommunityToolkit.Mvvm.Input;

namespace MauiCalculator.Behaviors;

public class EnterPressedBehavior : Behavior<Entry>
{
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(RelayCommand), typeof(EnterPressedBehavior), null);

    public RelayCommand Command
    {
        get => (RelayCommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    protected override void OnAttachedTo(Entry entry)
    {
        base.OnAttachedTo(entry);
        entry.Completed += OnEntryCompleted;
    }

    protected override void OnDetachingFrom(Entry entry)
    {
        entry.Completed -= OnEntryCompleted;
        base.OnDetachingFrom(entry);
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        var entry = (Entry)sender;
        if (Command != null && Command.CanExecute(null))
        {
            Command.Execute(entry.Text);
        }
    }
}