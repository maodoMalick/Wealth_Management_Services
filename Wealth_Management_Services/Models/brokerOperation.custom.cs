using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace Wealth_Management_Services.Models
{
    [MetadataType(typeof(brokerMetadata))]
    public partial class brokerOperation
    {

    }

    public class brokerMetadata
    {
        // 'Transaction ID' will be auto-generated from the db
        // that's why it is hidden in the View
        [HiddenInput(DisplayValue = false)]
        public string transactionID { get; set; }

        [Required]
        public IEnumerable<SelectListItem> trader
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem(){ Text = "Acorns", Value = "01"},
                    new SelectListItem(){ Text = "Merryll Lynch", Value = "02"},
                    new SelectListItem(){ Text = "eTrade", Value = "03"},
                    new SelectListItem(){ Text = "Stash", Value = "04"},
                    new SelectListItem(){ Text = "TradeStation", Value = "05"},
                    new SelectListItem(){ Text = "Vanguard", Value = "06"},
                    new SelectListItem(){ Text = "Fidelity", Value = "07"},
                    new SelectListItem(){ Text = "FirsTrade", Value = "08"},
                    new SelectListItem(){ Text = "Charles Schwab", Value = "09"},
                    new SelectListItem(){ Text = "Ameritrade", Value = "10"}
                };
            }
        }

        [Required]
        public IEnumerable<SelectListItem> shares
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem(){ Text = "Stocks", Value = "01"},
                    new SelectListItem(){ Text = "Bonds", Value = "02"},
                    new SelectListItem(){ Text = "ETFs", Value = "03"},
                    new SelectListItem(){ Text = "Mutual-Funds", Value = "04"},
                    new SelectListItem(){ Text = "Hedge-Funds", Value = "05"}
                };
            }
        }

        [Required]
        public IEnumerable<SelectListItem> item
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem(){ Text = "AAPL", Value = "01"},
                    new SelectListItem(){ Text = "DOW", Value = "02"},
                    new SelectListItem(){ Text = "NFLX", Value = "03"},
                    new SelectListItem(){ Text = "UAA", Value = "04"},
                    new SelectListItem(){ Text = "TSLA", Value = "05"},
                    new SelectListItem(){ Text = "AMZN", Value = "06"}
                };
            }
        }

        [Required]
        [RegularExpression("^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$")]
        [DisplayName("broker Id")]
        public Nullable<int> brokerID { get; set; }

        [Required]
        [DisplayName("purchase Date")]
        public Nullable<System.DateTime> purchaseDate { get; set; }

        [Required]
        [RegularExpression("^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$")]
        [DisplayName("client Id")]
        public Nullable<int> clientID { get; set; }

        [Required]
        [RegularExpression("^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$")]
        [DisplayName("quantity")]
        public Nullable<int> amount { get; set; }
    }
}


	
	
	
	
	







