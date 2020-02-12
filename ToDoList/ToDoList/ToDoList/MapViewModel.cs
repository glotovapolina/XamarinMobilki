using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;


namespace ToDoList
{
    class MapViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // private Xamarin.Forms.GoogleMaps.Polyline polyline;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Plugin.Geolocator.Abstractions.Position position;
        public MapViewModel(BindableMap myMap)
        {
            System.Threading.Tasks.Task.Run(async () =>
            {
                position = await Plugin.Geolocator.CrossGeolocator.Current.GetPositionAsync();
                MyPosition = new Position(position.Latitude, position.Longitude);
                //_myPosition = new Position(position.Latitude, position.Longitude);

                //BindableMap MapPositionProperty = new BindableMap();
                //MapPositionProperty.MapPosition = MyPosition;
                PinCollection.Add(new Pin() { Position = MyPosition, Type = PinType.Generic, Label = "I'm a Pin" });
                

            });

/*
            System.Threading.Tasks.Task.Run(async () =>
            {
                while (position == null)
                {
                    Thread.Sleep(1);
                }

                Polyline polyline = new Polyline
                {
                    StrokeColor = Color.Red,
                    StrokeWidth = 15,
                    Geopath =
                {
                    new Position(MyPosition.Latitude, MyPosition.Longitude),
                    //new Position(53, 50)
                    new Position(53.213383, 50.176535),
                    new Position(53.212969, 50.1775006),
                        new Position(53.212502, 50.178328),

                        new Position(53.212336, 50.177048),
                        new Position(53.213673, 50.176805)
                    //new Position(position.Latitude, position.Longitude)
                     //   new Position(position.Latitude, position.Longitude)

                }
                };
                myMap.MapElements.Add(polyline);

            });
            */
            



        }



        private ObservableCollection<Pin> _pinCollection = new ObservableCollection<Pin>();
        public ObservableCollection<Pin> PinCollection { get { return _pinCollection; } set { _pinCollection = value; OnPropertyChanged(); } }
        
        private Position _myPosition = new Position(53.212969, 50.1775006);
        public Position MyPosition { get { return _myPosition; } set { _myPosition = value; OnPropertyChanged(); } }


    }
}

