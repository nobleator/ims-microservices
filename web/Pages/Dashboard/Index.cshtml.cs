using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web;
using web.Domain;
using web.Domain.DataTransferObjects;
using web.Utility;

namespace web.Pages
{
    public class DashboardModel : PageModel
    {
        public DashboardModel()
        {
            
        }
        
        public void OnGetAsync()
        {
            // TODO: Get deliveries
        }

        public async void OnGetQueueDeliveryOptimization()
        {
            Console.WriteLine($"DEBUG: Entering {nameof(OnGetQueueDeliveryOptimization)}");
            await ApiHelper.QueueDeliveryOptimization();
            Console.WriteLine($"DEBUG: Exiting {nameof(OnGetQueueDeliveryOptimization)}");
        }
    }
}
