using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UserWindow
{
    class WeatherService
    {
        public string WeatherStatus { get; set; }
        public string Wind { get; set; }
        public string Temperature { get; set; }
        public string Rain { get; private set; }

        public string url = "http://api.pogoda.com/index.php?api_lang=ru&localidad=13088&affiliate_id=4v7j6at7rkya&v=2";


        public string GetActualData()
        {
            XmlTextReader xmlReader = new XmlTextReader(url);

            string optimal_time = FindMyTime();

            

            int day_count = 0;
            while (xmlReader.Read())
            {
                if (xmlReader.Name == "day")
                {
                    if (day_count == 1)
                        break;

                    day_count++;

                    xmlReader.MoveToNextAttribute();

                    while (xmlReader.Read())
                    {

                        if (xmlReader.Name == "hour" && xmlReader.NodeType == XmlNodeType.Element)
                        {
                            xmlReader.MoveToFirstAttribute();

                            string hours_api = xmlReader.Value;
                            hours_api = hours_api.Substring(0, 2);

                            if (optimal_time == hours_api)
                            {
                                while (xmlReader.Read())
                                {
                                    if (xmlReader.Name == "temp")
                                    {
                                        while (xmlReader.MoveToNextAttribute())
                                        {
                                            if (xmlReader.Name == "value")
                                            {
                                                Temperature = xmlReader.Value;
                                                break;
                                            }
                                        }
                                    }

                                    if (xmlReader.Name == "wind")
                                    {
                                        while (xmlReader.MoveToNextAttribute())
                                        {
                                            if (xmlReader.Name == "value")
                                            {
                                                Wind = xmlReader.Value;
                                                break;
                                            }
                                        }
                                    }

                                    if (xmlReader.Name == "symbol")
                                    {
                                        while (xmlReader.MoveToNextAttribute())
                                        {
                                            if (xmlReader.Name == "desc")
                                            {
                                                WeatherStatus = xmlReader.Value;
                                                break;
                                            }
                                        }
                                    }

                                    if (xmlReader.Name == "rain")
                                    {
                                        while (xmlReader.MoveToNextAttribute())
                                        {
                                            if (xmlReader.Name == "value")
                                            {
                                                Rain = xmlReader.Value;
                                                return $"{WeatherStatus},{Wind} km/h,{Temperature}*C,{Rain}mm";
                                            }
                                        }
                                    }

                                }
                            }

                        }
                    }
                }
            }
            return string.Empty;
        }


        string _currentHours = DateTime.Now.Hour.ToString();
        private string FindMyTime()
        {
            XmlTextReader xmlReader = new XmlTextReader(url);

            int day_count = 0;
            while (xmlReader.Read())
            {
                if (xmlReader.Name == "day")
                {
                    day_count++;
                    if (day_count == 2)
                        break;
                }

                if (xmlReader.Name == "hour" && xmlReader.NodeType == XmlNodeType.Element)
                {
                    xmlReader.MoveToFirstAttribute();

                    string api_hours = xmlReader.Value.Substring(0, 2);
                    if (_currentHours == api_hours)
                    {
                        return api_hours;
                    }
                }
            }

            int hours = int.Parse(_currentHours);
            hours--;
            _currentHours = hours.ToString();

            FindMyTime();

            return _currentHours;
            //return String.Empty;
        }

    }
}
