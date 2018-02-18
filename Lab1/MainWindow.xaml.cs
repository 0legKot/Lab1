using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab1
{
    public partial class MainWindow : Window
    {
        // TODO: Add int-property to each class and use as key for Dictionaries  (maybe useless)
        MyDictionary TransportTypes = new MyDictionary();
        MyDictionary Transports = new MyDictionary();
        MyDictionary Routes = new MyDictionary();
        MyDictionary Stops = new MyDictionary();
        public MainWindow()
        {
            InitializeComponent();

            TransportTypes.AddDependency(TypeOfTransport);
            TransportTypes.AddDependency(TypeOfTransportList);

            Transports.AddDependency(TransportBox);
            Transports.AddDependency(TransportList);

            Routes.AddDependency(RouteList);

            Stops.AddDependency(ChosenStops);
            Stops.AddDependency(AvailableStops);
            Stops.AddDependency(StopList);
        }



        private void AddTransportTypeButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit(Action.Add, Class.TransportType);
        }

        private Type ChooseType(string source)
        {

            switch (source.ToLower())
            {
                case "air":
                    return Type.Air;

                case "land":
                    return Type.Land;

                case "underground":
                    return Type.Underground;

                case "water":
                    return Type.Water;

                default:
                    return Type.Other;

            }
        }

        private void AddOrRemoveOrEdit(Action action, Class currentClass)
        {
            try
            {
                string currentName = (action == Action.Add) ?
                     ChooseName(currentClass) :
                     ChooseNameFromList(currentClass);
                MyDictionary currentDictionary = GetDictionary(currentClass);
                AnyTransportClass currentElement = GetElement(currentClass, currentName);
                switch (action)
                {
                    case Action.Remove:
                        currentDictionary.RemoveElement(currentName);
                        break;
                    case Action.Edit:
                        currentDictionary.EditElement(currentElement, currentName);
                        break;
                    case Action.Add:
                        currentDictionary.AddElement(currentElement, currentName);
                        break;
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        private AnyTransportClass GetElement(Class currentClass, string currentName)
        {
            switch (currentClass)
            {
                case Class.TransportType:
                    Type chosenType = ChooseType(Position.Text);
                    return new TransportType(currentName, chosenType, AverageTicketPrice.Text, Number.Text, MonthServiceCost.Text, AccidentRate.Text);
                case Class.Transport:
                    return new Transport(currentName, (TransportType)TransportTypes.GetElement(TypeOfTransport.Text), Capacity.Text, TicketPrice.Text);
                case Class.Route:
                    List<Stop> stopList = new List<Stop>();
                    foreach (string s in ChosenStops.Items)
                        stopList.Add((Stop)Stops.GetElement(s, "stop"));
                    return new Route(currentName, StartLocation.Text, EndLocation.Text, stopList, (Transport)Transports.GetElement(TransportBox.Text));
                case Class.Stop:
                    return new Stop(currentName, Location.Text, OpenedFrom.Text, ClosedAt.Text);
                default: return null;
            }


        }

        private void RemoveItem(Class currentClass, string currentName)
        {
            MyDictionary dictionary = GetDictionary(currentClass);
            dictionary.RemoveElement(currentName);
        }
        private MyDictionary GetDictionary(Class currentClass)
        {
            switch (currentClass)
            {
                case Class.TransportType: return TransportTypes;
                case Class.Transport: return Transports;
                case Class.Route: return Routes;
                case Class.Stop: return Stops;
                default: return null;
            }
        }

        private string ChooseNameFromList(Class currentClass)
        {
            try
            {
                switch (currentClass)
                {
                    case Class.TransportType:
                        return TypeOfTransportList.SelectedItem.ToString();

                    case Class.Transport:
                        return TransportList.SelectedItem.ToString();
                    case Class.Route:
                        return RouteList.SelectedItem.ToString();
                    case Class.Stop:
                        return StopList.SelectedItem.ToString();
                    default: return null;
                }
            }
            catch { throw new Exception("No item selected"); }
        }

        private string ChooseName(Class currentClass)
        {
            switch (currentClass)
            {
                case Class.TransportType:
                    return NameOfTransportType.Text;
                case Class.Transport:
                    return Id.Text;
                case Class.Route:
                    return NameOfRoute.Text;
                case Class.Stop:
                    return NameOfStop.Text;
                default: return null;
            }
        }

        private void AddTransportButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit(Action.Add, Class.Transport);
        }

        private void AddStopButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit(Action.Add, Class.Stop);
        }

        private void AddStopToRouteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ChosenStops.Items.Contains(AvailableStops.SelectedItem)) ChosenStops.Items.Add(AvailableStops.SelectedItem);
        }

        private void AddRouteButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit(Action.Add, Class.Route);
        }

        private void RemoveStopFromRouteButton_Click(object sender, RoutedEventArgs e)
        {
            ChosenStops.Items.Remove(ChosenStops.SelectedItem);
        }

        private void EditTypeOfTransport_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit(Action.Edit, Class.TransportType);
        }

        private void RemoveTypeOfTransportButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit(Action.Remove, Class.TransportType);
        }

        private void RemoveTransportButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit(Action.Remove, Class.Transport);
        }

        private void EditTransportButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit(Action.Edit, Class.Transport);
        }

        private void RemoveRouteButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit(Action.Remove, Class.Route);
        }

        private void EditRouteButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit(Action.Edit, Class.Route);
        }

        private void RemoveStopButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit(Action.Remove, Class.Stop);
        }

        private void EditStopButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit(Action.Edit, Class.Stop);
        }

        private void TypeOfTransportList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (TypeOfTransportList.SelectedItem != null)
            {
                var tmp = (TransportType)TransportTypes.GetElement(TypeOfTransportList.SelectedItem.ToString());
                NameOfTransportType.Text = tmp.Name;
                Position.Text = tmp.Position.ToString();
                AverageTicketPrice.Text = tmp.AvgTicketPrice.ToString();
                Number.Text = tmp.Quantity.ToString();
                MonthServiceCost.Text = tmp.MonthServiceCost.ToString();
                AccidentRate.Text = tmp.AccidentRate.ToString();

            }
        }

        private void TransportList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (TransportList.SelectedItem != null)
            {
                var tmp = (Transport)Transports.GetElement(TransportList.SelectedItem.ToString());
                Id.Text = tmp.Name;
                Capacity.Text = tmp.Capacity.ToString();
                TicketPrice.Text = tmp.TicketPrice.ToString();
            }
        }

        private void RouteList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RouteList.SelectedItem != null)
            {
                var tmp = (Route)Routes.GetElement(RouteList.SelectedItem.ToString());
                NameOfRoute.Text = tmp.Name;
                StartLocation.Text = tmp.StartLocation;
                EndLocation.Text = tmp.EndLocation;
                ChosenStops.Items.Clear();
                foreach (Stop s in tmp.Stops)
                    ChosenStops.Items.Add(s.Name);
            }
        }

        private void StopList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (StopList.SelectedItem != null)
            {
                var tmp = (Stop)Stops.GetElement(StopList.SelectedItem.ToString());
                NameOfStop.Text = tmp.Name;
                Location.Text = tmp.Location;
                OpenedFrom.Text = tmp.OpenedFrom.ToShortTimeString();
                ClosedAt.Text = tmp.ClosedAt.ToShortTimeString();
            }
        }
    }
}
