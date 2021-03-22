using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DetailsPage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var request = WebRequest.Create("https://graph.facebook.com/utc2hcmc/posts?access_token=EAAAAZAw4FxQIBANSjvi4KIFmMIBc2IEiR9dpYLxnMXaR0w7a8qKYlSzHwkTBAMKw2qbR3fcZCh20powxvhbPAYufY4PEgRUHILNxFZBBzcXQwjJ01XbQnEHZB8GCjJeyzNUgcPBMyMKJGOEDzQRlcAE3DsZAUe68sBLn7HTEelsIV5TZAUeRuKCKdrX0Iz67gZD");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseString = reader.ReadToEnd();
            dynamic jsonData = JsonConvert.DeserializeObject(responseString);
            var results = new List<InfoPost>();
            foreach (var item in jsonData.data)
            {
                results.Add(new InfoPost
                {
                    Time = item.created_time,
                    Content = item.message,
                    Link = item.actions[0].link,
                });
            }
            string s = "";
            for (int i = 0; i < 3; i++)
            {
                s += "<b>Bài thứ " + (i + 1) + ": </b>" + "</br>";
                s += "<b>Ngày đăng: </b>" + results[i].Time + "</br>";
                s += "<b>Nội dung: </b>" + results[i].Content + "</br>";
                s += "<b> Link bài viết: </b>" + results[i].Link + "</br>";
            }
            lbResult.Text = s;
        }
    }
}