using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace PikaScope.ViewModels
{
    public class HeroCardsViewModel
    {
        public ObservableCollection<Hero> Heroes { get; set; }

        public HeroCardsViewModel()
        {
            SetData();
        }


        public async void SetData()
        {
            var client = new HttpClient();
            Heroes = new ObservableCollection<Hero>();
            for (int i = 0 ; i < 1000 ; i++)
            {
                var sNumber = (i + 1).ToString();
                var formattedNumber = (sNumber.Length == 1) ? $"00{ sNumber}" : (sNumber.Length == 2) ? $"0{ sNumber}" : sNumber;

                var response = await client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{i+1}");
                var data = JsonConvert.DeserializeObject<Hero>(response);
                data.Image = $"https://assets.pokemon.com/assets/cms2/img/pokedex/full/{formattedNumber}.png";
                data.Posters = new List<string>()
                    {
                        "ironman_1.png",
                        "ironman_2.png",
                        "ironman_3.png",
                        "ironman_4.png",
                    };
                data.Bio = "Tony Stark is an eccentric self-described genius, billionaire, playboy and philanthropist and the former head of Stark Industries.";
                SetColors(data);
                Heroes.Add(data);
                Debug.WriteLine(data.Name);
            }
        }

        private void SetColors(Hero name)
        {
            // get color for the provided text
            var hexColor = "#FF" + System.Convert.ToString(name.GetHashCode(), 16).Substring(0, 6);

            // fix issue if value is too short
            if (hexColor.Length == 8)
                hexColor += "5";
            name.HeroColor = hexColor;

            // create color from hex value
            var color = Xamarin.Forms.Color.FromHex(hexColor);


            // get brightness and set textcolor
            var brightness = color.R * .3 + color.G * .59 + color.B * .11;
            name.HeroMainTextColor = brightness < 0.5 ?  "#ffffff" : "#000000";
        }
    }
    public class Hero
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string Image { get; set; }
        public string HeroColor { get; set; }
        public string HeroMainTextColor { get; set; }
        public string Bio { get; set; }
        public List<string> Posters { get; set; }
    }
}
