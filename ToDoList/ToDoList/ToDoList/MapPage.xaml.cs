using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ToDoList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            Title = AppResources.Map;
            BindingContext = new MapViewModel(myMap);

            Polyline polyline = new Polyline
            {
                StrokeColor = Color.Red,
                StrokeWidth = 15,
                Geopath =
                {
                  
                    //new Position(53, 50)
                    new Position(53.215230, 50.172948),
                    new Position(53.214675, 50.173437),
                        new Position(53.215133, 50.174408),

                        new Position(53.212995, 50.177309),
                        new Position(53.212552, 50.178387),
                         new Position(53.212208, 50.177174)
                    //new Position(position.Latitude, position.Longitude)
                     //   new Position(position.Latitude, position.Longitude)

                }
            };
            myMap.MapElements.Add(polyline);
        }
    }
}