using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace LUISStockbot
{
    public class Yahoobot
    {
        public static async Task<double?> GetStockRateAsync(string StockSymbol)
        {
            try {
                string ServiceURL = "http://finance.yahoo.com/d/quotes.csv?s={StockSymbol}&f=sl1d1nd";
                string ResultInCSV;
                using (WebClient client = new WebClient())
                {
                    ResultInCSV = await client.DownloadStringTaskAsync(ServiceURL).ConfigureAwait(false);
                }
                var FirstLine = ResultInCSV.Split('\n')[0];
                var price = FirstLine.Split(',')[1];
                if (price != null && price.Length >= 0) {
                    double result;
                    if (double.TryParse(price, out result)) {
                        return result;
                    }
                }
                return null;
            }
            catch (WebException ex) {
                throw ex;
            }
        }
    }
}