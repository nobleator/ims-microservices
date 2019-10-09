using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using web.Domain.DataTransferObjects;
using web.Utility;

namespace web.Pages
{
    public class IndexModel : PageModel
    {
        public string InspirationalQuote;
        public void OnGetAsync()
        {
            Console.WriteLine($"DEBUG: Entering {nameof(OnGetAsync)}");
            
            string[] quotes = {
                "There is nothing noble in being superior to your fellow man; true nobility is being superior to your former self.",
                "When you arise in the morning give thanks for the food and for the joy of living. If you see no reason for giving thanks, the fault lies only in yourself.",
                "When it comes your time to die, be not like those whose hearts are filled with the fear of death, so that when their time comes they weep and pray for a little more time to live their lives over again in a different way. Sing your death song and die like a hero going home.",
                "Be yourself; everyone else is already taken.",
                "Two things are infinite: the universe and human stupidity; and I'm not sure about the universe.",
                "Be the change that you wish to see in the world.",
                "No one can make you feel inferior without your consent.",
                "The people must learn how well I govern them. How would they know if I did not tell them?"
            };

            InspirationalQuote = quotes[new Random().Next(0, quotes.Length)];
            // return Page();
        }
    }
}
