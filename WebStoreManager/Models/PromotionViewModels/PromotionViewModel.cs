using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreManager.Models.PromotionViewModels
{
    public class PromotionViewModel
    {
        public DateTime Lifetime { get; set; }
        public DateTime Created { get; set; }
        public decimal Percent { get; set; }

    }
}
