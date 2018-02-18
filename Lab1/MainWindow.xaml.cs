using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab1
{
    public partial class MainWindow : Window
    {
        // TODO: Add int-property to each class and use as key for Dictionaries  (maybe useless)
        // MyDictionary<string, TransportType> TrTp = new MyDictionary<string, TransportType>();
        MyDictionary<string, TransportType> TransportTypes = new MyDictionary<string, TransportType>();
        MyDictionary<string, Transport> Transports = new MyDictionary<string, Transport>();
        MyDictionary<string, Route> Routes = new MyDictionary<string, Route>();
        MyDictionary<string, Stop> Stops = new MyDictionary<string, Stop>();
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

        // TODO: Add Enum for action, currentClass DONE
        private void AddOrRemoveOrEdit(Action action, Class currentClass)
        {
            Type chosenType = ChooseType(Position.Text);
            try
            {
                string currentName = "";
                try
                {
                    currentName = (action == Action.Add) ?
                         ChooseName(currentClass) :
                         ChooseNameFromList(currentClass);
                }
                catch { throw new Exception("No item selected");  }
                
                // TODO: Add method to choose instance of MyDictionary and get it !!!!!!!!!!!!!!!!!!!!!!!


                //dynamic currentElement=null;
                ////MyDictionary<string,dynamic> currentDictionary=null;
                //switch (currentClass)
                //{
                //    case Class.TransportType:
                //        currentElement = new TransportType(currentName, chosenType, AverageTicketPrice.Text, Number.Text, MonthServiceCost.Text, AccidentRate.Text);
                //        //currentDictionary = TransportTypes;
                //        break;
                //    case Class.Transport:
                //        currentElement = new Transport(currentName, TransportTypes[TypeOfTransport.Text], Capacity.Text, TicketPrice.Text);
                //        //currentDictionary = Transports;
                //        break;
                //    case Class.Route:
                //        currentElement= new Route(currentName, StartLocation.Text, EndLocation.Text, stopList, Transports[TransportBox.Text]);
                //        //currentDictionary = Routes;
                //        break;
                //    case Class.Stop:
                //        currentElement= new Stop(currentName, Location.Text, OpenedFrom.Text, ClosedAt.Text);
                //        //currentDictionary = Stops;
                //        break;
                //}

                switch (action)
                {
                    case Action.Remove: RemoveItem(currentClass, currentName); break;
                    case Action.Edit:
                        //currentDictionary.EditElement(currentElement,currentName));
                        switch (currentClass)
                        {
                            case Class.TransportType:
                                // TODO: Create constructor with texts as parameters () DONE 
                                var tmpTransportType = new TransportType(currentName, chosenType, AverageTicketPrice.Text, Number.Text, MonthServiceCost.Text, AccidentRate.Text);
                                TransportTypes.EditElement(tmpTransportType, currentName);
                                break;
                            case Class.Transport:
                                var tmpTransport = new Transport(currentName, TransportTypes.GetElement(TypeOfTransport.Text,"type of transport"), Capacity.Text, TicketPrice.Text);
                                Transports.EditElement(tmpTransport, currentName);
                                break;
                            case Class.Route:
                                List<Stop> stopList = new List<Stop>();
                                foreach (string s in ChosenStops.Items)
                                    stopList.Add(Stops.GetElement(s,"stop"));
                                

                                var tmpRoute = new Route(currentName, StartLocation.Text, EndLocation.Text, stopList, Transports.GetElement(TransportBox.Text,"transport"));
                                Routes.EditElement(tmpRoute, currentName);
                                break;
                            case Class.Stop:

                                var tmpStop = new Stop(currentName, Location.Text, OpenedFrom.Text, ClosedAt.Text);
                                Stops.EditElement(tmpStop, currentName);

                                break;
                        }
                        break;
                    case Action.Add:
                        //currentDictionary.AddElement(currentElement,currentName);
                        switch (currentClass)
                        {
                            case Class.TransportType:
                                var tmpTransportType = new TransportType(currentName, chosenType, AverageTicketPrice.Text, Number.Text, MonthServiceCost.Text, AccidentRate.Text);
                                TransportTypes.AddElement(tmpTransportType, currentName);
                                break;
                            case Class.Transport:
                                var tmpTransport = new Transport(currentName, TransportTypes.GetElement(TypeOfTransport.Text,"type of transport"), Capacity.Text, TicketPrice.Text);
                                Transports.AddElement(tmpTransport, currentName);
                                break;
                            case Class.Route:
                                List<Stop> stopList = new List<Stop>();
                                foreach (string s in ChosenStops.Items)
                                    stopList.Add(Stops.GetElement(s,"stop"));
                                

                                var tmpRoute = new Route(currentName, StartLocation.Text, EndLocation.Text, stopList, Transports.GetElement(TransportBox.Text,"transport"));
                                Routes.AddElement( tmpRoute, currentName);
                                break;
                            case Class.Stop:
                                var tmpStop = new Stop(currentName, Location.Text, OpenedFrom.Text, ClosedAt.Text);
                                Stops.AddElement(tmpStop, currentName);
                                break;
                        }
                        break;
                }
            }
            catch(Exception e) { MessageBox.Show(e.Message); }
        }

        private void RemoveItem(Class currentClass, string currentName)
        {
            switch (currentClass)
            {
                case Class.TransportType: TransportTypes.RemoveElement(currentName); break;
                case Class.Transport: Transports.RemoveElement(currentName); break;
                case Class.Route: Routes.RemoveElement(currentName); break;
                case Class.Stop: Stops.RemoveElement(currentName); break;
            }
        }

        private string ChooseNameFromList(Class currentClass)
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
            }

            return "";
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
            }

            return "";
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
                var tmp = TransportTypes.GetElement(TypeOfTransportList.SelectedItem.ToString());
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
                var tmp = Transports.GetElement(TransportList.SelectedItem.ToString());
                Id.Text = tmp.Id;
                Capacity.Text = tmp.Capacity.ToString();
                TicketPrice.Text = tmp.TicketPrice.ToString();
            }
        }

        private void RouteList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RouteList.SelectedItem != null)
            {
                var tmp = Routes.GetElement(RouteList.SelectedItem.ToString());
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
                var tmp = Stops.GetElement(StopList.SelectedItem.ToString());
                NameOfStop.Text = tmp.Name;
                Location.Text = tmp.Location;
                OpenedFrom.Text = tmp.OpenedFrom.ToShortTimeString();
                ClosedAt.Text = tmp.ClosedAt.ToShortTimeString();
            }
        }
    }
}
