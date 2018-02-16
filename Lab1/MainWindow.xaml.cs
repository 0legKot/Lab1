using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, TransportType> TransportTypes = new Dictionary<string, TransportType>();
        Dictionary<string, Transport> Transports = new Dictionary<string, Transport>();
        Dictionary<string, Route> Routes = new Dictionary<string, Route>();
        Dictionary<string, Stop> Stops = new Dictionary<string, Stop>();
        public MainWindow()
        {
            InitializeComponent();
        }


        private void AddTransportTypeButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit("Add","TransportType");
        }

        private Type ChooseType()
        {
            Type chosenType;
            switch (Position.Text.ToLower())
            {
                case "air":
                    chosenType = Type.Air;
                    break;
                case "land":
                    chosenType = Type.Land;
                    break;
                case "underground":
                    chosenType = Type.Underground;
                    break;
                case "water":
                    chosenType = Type.Water;
                    break;
                default:
                    chosenType = Type.Other;
                    break;
            }

            return chosenType;
        }

        private void AddOrRemoveOrEdit(string op, string currentClass)
        {
            Type chosenType = ChooseType();
            try
            {
                string currentName = "";
                try
                {
                    currentName = (op == "Add") ?
                         ChooseName(currentClass) :
                         ChooseNameFromList(currentClass);
                }
                catch { MessageBox.Show("No item selected"); return; }
                List<Stop> stopList = new List<Stop>();
                foreach (string s in ChosenStops.Items)
                    stopList.Add(Stops[s]);
                if ((stopList.Count == 0) && (currentClass == "Route")) { MessageBox.Show("Select stops"); return; }
                switch (op)
                {
                    case "Remove": RemoveItem(currentClass, currentName); break;
                    case "Edit":
                        switch (currentClass)
                        {
                            case "TransportType":
                                var tmpTransportType = new TransportType(currentName, chosenType, double.Parse(AverageTicketPrice.Text), int.Parse(Number.Text), int.Parse(MonthServiceCost.Text), int.Parse(AccidentRate.Text));
                                EditTransportType(tmpTransportType);
                                break;
                            case "Transport":
                                var tmpTransport = new Transport(currentName, TransportTypes[TypeOfTransport.Text], int.Parse(Capacity.Text), int.Parse(TicketPrice.Text));
                                EditTransport(tmpTransport);
                                break;
                            case "Route":
                                var tmpRoute = new Route(currentName, StartLocation.Text, EndLocation.Text, stopList, Transports[TransportBox.Text]);
                                EditRoute(tmpRoute);
                                break;
                            case "Stop":
                                var tmpStop = new Stop(currentName, Location.Text, DateTime.Parse(OpenedFrom.Text), DateTime.Parse(OpenedFrom.Text));
                                EditStop(tmpStop);
                                break;
                        }
                        break;
                    case "Add":
                        switch (currentClass)
                        {
                            case "TransportType":
                                var tmpTransportType = new TransportType(currentName, chosenType, double.Parse(AverageTicketPrice.Text), int.Parse(Number.Text), int.Parse(MonthServiceCost.Text), int.Parse(AccidentRate.Text));
                                if (!TransportTypes.ContainsKey(currentName)) AddTransportType(currentName, tmpTransportType);
                                else MessageBox.Show("This Transport type already exists");
                                break;
                            case "Transport":
                                var tmpTransport = new Transport(currentName, TransportTypes[TypeOfTransport.Text], int.Parse(Capacity.Text), int.Parse(TicketPrice.Text));
                                if (!Transports.ContainsKey(currentName)) AddTransport(tmpTransport);
                                else MessageBox.Show("This Transport already exists");
                                break;
                            case "Route":
                                var tmpRoute = new Route(currentName, StartLocation.Text, EndLocation.Text, stopList, Transports[TransportBox.Text]);
                                if (!Routes.ContainsKey(currentName)) AddRoute(tmpRoute);
                                else MessageBox.Show("This Route already exists");
                                break;
                            case "Stop":
                                var tmpStop = new Stop(currentName, Location.Text, DateTime.Parse(OpenedFrom.Text), DateTime.Parse(OpenedFrom.Text));
                                if (!Stops.ContainsKey(currentName)) AddStop(tmpStop);
                                else MessageBox.Show("This Stop already exists");
                                break;
                        }
                        break;
                }
            }
            catch { MessageBox.Show("Incorrect format"); }
        }

        private void RemoveItem(string currentClass, string currentName)
        {
            switch (currentClass)
            {
                case "TransportType": RemoveTransportType(currentName); break;
                case "Transport": RemoveTransport(currentName); break;
                case "Route": RemoveRoute(currentName); break;
                case "Stop": RemoveStop(currentName); break;
            }
        }

        private string ChooseNameFromList(string currentClass)
        {
            switch (currentClass)
            {
                case "TransportType":
                    return TypeOfTransportList.SelectedItem.ToString();

                case "Transport":
                    return TransportList.SelectedItem.ToString();
                case "Route":
                    return RouteList.SelectedItem.ToString();
                case "Stop":
                    return StopList.SelectedItem.ToString();
            }

            return "";
        }

        private string ChooseName(string currentClass)
        {
            switch (currentClass)
            {
                case "TransportType":
                    return NameOfTransportType.Text;
                case "Transport":
                    return Id.Text;
                case "Route":
                    return NameOfRoute.Text;
                case "Stop":
                    return NameOfStop.Text;
            }

            return "";
        }

        private void EditTransportType(TransportType tmp)
        {
            TransportTypes[tmp.Name] = tmp;
        }

        private void RemoveTransportType(string currentName)
        {
            TransportTypes.Remove(currentName);
            TypeOfTransport.Items.Remove(currentName);
            TypeOfTransportList.Items.Remove(currentName);
        }

        private void AddTransportType(string currentName, TransportType tmp)
        {
            TransportTypes.Add(currentName, tmp);
            TypeOfTransport.Items.Add(currentName);
            TypeOfTransportList.Items.Add(currentName);
        }



        private void AddTransportButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit("Add", "Transport");
        }

        private void AddTransport(Transport tmp)
        {
            Transports.Add(Id.Text, tmp);
            TransportBox.Items.Add(Id.Text);
            TransportList.Items.Add(Id.Text);
        }

        private void AddStopButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit("Add", "Stop");
        }

        private void AddStop(Stop tmp)
        {
            Stops.Add(NameOfStop.Text, tmp);
            StopList.Items.Add(NameOfStop.Text);
            AvailableStops.Items.Add(NameOfStop.Text);
        }

        private void AddStopToRouteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ChosenStops.Items.Contains(AvailableStops.SelectedItem)) ChosenStops.Items.Add(AvailableStops.SelectedItem);
        }

        private void AddRouteButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit("Add", "Route");
        }

        private void AddRoute(Route tmp)
        {
            Routes.Add(NameOfRoute.Text, tmp);
            RouteList.Items.Add(NameOfRoute.Text);
        }

        private void RemoveStopFromRouteButton_Click(object sender, RoutedEventArgs e)
        {
            ChosenStops.Items.Remove(ChosenStops.SelectedItem);
        }




        private void EditTypeOfTransport_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit("Edit", "TransportType");
        }



        private void RemoveTypeOfTransportButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit("Remove", "TransportType");
        }

        private void RemoveTransportType()
        {
            TransportTypes.Remove(TypeOfTransportList.SelectedItem.ToString());
            TypeOfTransport.Items.Remove(TypeOfTransportList.SelectedItem.ToString());
            TypeOfTransportList.Items.Remove(TypeOfTransportList.SelectedItem);
        }

        private void RemoveTransportButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit("Remove", "Transport");
        }

        private void RemoveTransport(string name)
        {
            Transports.Remove(name);
            TransportBox.Items.Remove(name);
            TransportList.Items.Remove(name);
        }

        private void EditTransportButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit("Edit", "Transport");
        }

        private void EditTransport(Transport tmp)
        {
            Transports[TransportList.SelectedItem.ToString()] = tmp;
        }

        private void RemoveRouteButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit("Remove", "Route");
        }

        private void RemoveRoute(string name)
        {
            Routes.Remove(name);
            RouteList.Items.Remove(name);
        }

        private void EditRouteButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit("Edit", "Route");
        }

        private void EditRoute(Route tmp)
        {
            Routes[RouteList.SelectedItem.ToString()] = tmp;
        }

        private void RemoveStopButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit("Remove", "Stop");
        }

        private void RemoveStop(string name)
        {
            Stops.Remove(name);
            if (ChosenStops.Items.Contains(name)) ChosenStops.Items.Remove(name);
            if (AvailableStops.Items.Contains(name)) AvailableStops.Items.Remove(name);
            AvailableStops.Items.Remove(name);
            StopList.Items.Remove(name);
        }

        private void EditStopButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveOrEdit("Edit", "Stop");
        }

        private void EditStop(Stop tmp)
        {
            Stops[StopList.SelectedItem.ToString()] = tmp;
        }

        private void TypeOfTransportList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (TypeOfTransportList.SelectedItem != null)
            {
                var tmp = TransportTypes[TypeOfTransportList.SelectedItem.ToString()];
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
                var tmp = Transports[TransportList.SelectedItem.ToString()];
                Id.Text = tmp.Id;
                Capacity.Text = tmp.Capacity.ToString();
                TicketPrice.Text = tmp.TicketPrice.ToString();
            }
        }

        private void RouteList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RouteList.SelectedItem != null)
            {
                var tmp = Routes[RouteList.SelectedItem.ToString()];
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
                var tmp = Stops[StopList.SelectedItem.ToString()];
                NameOfStop.Text = tmp.Name;
                Location.Text = tmp.Location;
                OpenedFrom.Text = tmp.OpenedFrom.ToShortTimeString();
                ClosedAt.Text = tmp.ClosedAt.ToShortTimeString();
            }
        }
    }
}
