using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

namespace TestTransitionerSelection
{
    public partial class MainWindow : Window
    {
        public record ListItem( string Text );

        public ListCollectionView Items { get; } = new ListCollectionView( new List<ListItem>{
            new( "item 1" ), new( "item 2" ), new( "item 3" ), new( "item 4" ) } );

        public MainWindow() => InitializeComponent();

        private void Transitioner_ChangePanel( object sender, RoutedEventArgs e ) =>
            transitioner.SelectedItem = transitioner.SelectedItem == panel2 ? panel1 : panel2;

        private void Transitioner_SelectionChanged( object sender, System.Windows.Controls.SelectionChangedEventArgs e )
        {
            static string describeItem( object? obj )
            {
                if( obj is FrameworkElement el ) return $"FrameworkElement (name={el.Name})";
                else if( obj is ListItem li ) return $"ListItem (text={li.Text})";
                else return obj?.GetType()?.Name ?? "null";
            }

            Debug.WriteLine( $"Transition from " +
                $"{(e.RemovedItems.Count > 0 ? describeItem( e.RemovedItems[0] ) : "null")} to " +
                $"{(e.AddedItems.Count > 0 ? describeItem( e.AddedItems[0] ) : "null")}" );
        }

    }
}
