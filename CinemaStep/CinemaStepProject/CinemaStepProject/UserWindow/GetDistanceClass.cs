using Newtonsoft.Json.Linq;

namespace UserWindow
{
    class GetDistanceClass
    {

        public string getDistance(string origin, string destination)
        {
            System.Threading.Thread.Sleep(1000);
            string distance = string.Empty;
            string url = "http://maps.googleapis.com/maps/api/directions/json?origin=" + origin + "&destination=" + destination + "&sensor=false";
            string requesturl = url;
            string content = fileGetContents(requesturl);
            JObject o = JObject.Parse(content);
            try
            {
                distance = o.SelectToken("routes[0].legs[0].distance.text").ToString();
                return distance;
            }
            catch
            {
                return distance;
            }
        }

        private string fileGetContents(string fileName)
        {
            string sContents = string.Empty;
            string me = string.Empty;
            try
            {
                if (fileName.ToLower().IndexOf("http:") > -1)
                {
                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] response = wc.DownloadData(fileName);
                    sContents = System.Text.Encoding.ASCII.GetString(response);

                }
                else
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
                    sContents = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch { sContents = "unable to connect to server "; }
            return sContents;
        }


    }
}
