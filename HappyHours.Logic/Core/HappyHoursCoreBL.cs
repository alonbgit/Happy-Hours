using HappyHours.Logic.BL;
using HappyHours.Logic.Helpers;
using HappyHours.Models.Common;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.Core
{
    public class HappyHoursCoreBL
    {
        public HappyHourSummary GetSummary(HappyHoursLoginParameters parameters)
        {
            var data = HackData(parameters);

            var parsedData = ParseData(data);

            parsedData.Hours = FilterDays(parsedData.Hours, parameters.StartDate, parameters.EndDate);

            var result = CalculateData(parsedData);

            return result;
        }

        private HappyHourSummary CalculateData(HappyHourData happyHourData)
        {
            var hours = happyHourData.Hours;

            var monthlyHours = hours.Count() * 9;

            var minutesPerDay = 60 * 9;

            IList<DayHours> dayHours = new List<DayHours>();

            int totalLackMinutes = 0;
            int totalExtraMinutes = 0;

            foreach (var hour in hours)
            {
                int lackMinutes = 0;
                int extraMinutes = 0;

                if (hour.StartTime == null || hour.EndTime == null)
                {
                    lackMinutes = minutesPerDay;
                    totalLackMinutes += minutesPerDay;
                }
                else
                {
                    var diff = (hour.EndTime - hour.StartTime).Value.TotalMinutes;

                    if (diff > minutesPerDay)
                    {
                        extraMinutes = (int)diff - minutesPerDay;
                        totalExtraMinutes += extraMinutes;
                    }
                    else if (diff < minutesPerDay)
                    {
                        lackMinutes = minutesPerDay - (int)diff;
                        totalLackMinutes += lackMinutes;
                    }
                }


                var dayOur = new DayHours()
                {
                    Date = hour.Date,
                    LackMinutes = lackMinutes,
                    ExtraMinutes = extraMinutes,
                    StartTime = hour.StartTime,
                    EndTime = hour.EndTime,
                    Day = hour.Date.DayOfWeek.ToString()
                };
                dayHours.Add(dayOur);
            }

            if (totalExtraMinutes > totalLackMinutes)
            {
                totalExtraMinutes -= totalLackMinutes;
                totalLackMinutes = 0;
            }
            else if (totalLackMinutes > totalExtraMinutes)
            {
                totalLackMinutes -= totalExtraMinutes;
                totalExtraMinutes = 0;
            }

            return new HappyHourSummary()
            {
                ExtraMinutes = totalExtraMinutes,
                LackMinutes = totalLackMinutes,
                DayDetails = dayHours,
                User = happyHourData.User
            };
        }

        private IEnumerable<HappyHourItem> FilterDays(IEnumerable<HappyHourItem> hours, DateTime startDate, DateTime endDate)
        {
            return hours.Where(c => (int)c.Date.DayOfWeek != 5 &&
                                    (int)c.Date.DayOfWeek != 6 &&
                                    c.Date.Day <= endDate.Day &&
                                    c.Date.Date >= startDate.Date).ToList();
        }

        private HappyHourData ParseData(string data)
        {
            var startStr = "App.ContentPlaceHolderWorkAreaMid_GridEmployeeAttendanceStore.proxy.data =";
            var startIndex = data.IndexOf(startStr) + startStr.Length;

            var arr = data.Substring(startIndex, data.Length - startIndex);

            var endStr = "App.ContentPlaceHolderWorkAreaMid_GridEmployeeAttendanceStore.load();";
            var endIndex = arr.IndexOf(endStr);

            if (endIndex == -1)
                throw new HappyHourException(ErrorCode.InvalidYearForEmployee);

            arr = arr.Substring(0, endIndex - 1);

            var json = arr.Trim();

            json = json.Replace(@"\", string.Empty).Replace("חוה\"מ סוכות", string.Empty);

            var hours = JsonConvert.DeserializeObject<IEnumerable<HourItem>>(json);

            var happyHours = ParseHoursToHappyHours(hours);

            var userStr = "App.lblEmployeeName.setText(\"\"";
            var userIndex = data.IndexOf("App.lblEmployeeName.setText(") + userStr.Length;
            var userData = data.Substring(userIndex, data.Length - userIndex);

            var endUserStr = "\"";
            var endUserIndex = userData.IndexOf(endUserStr) - 1;
            userData = userData.Substring(0, endUserIndex);

            return new HappyHourData()
            {
                Hours = happyHours,
                User = userData
            };
        }

        private IEnumerable<HappyHourItem> ParseHoursToHappyHours(IEnumerable<HourItem> hours)
        {
            IList<HappyHourItem> happyHours = new List<HappyHourItem>();
            foreach(var hour in hours)
            {
                var date = HappyHourTimestampProvider.ParseDate(hour.DateString).Value;

                var startTime = HappyHourTimestampProvider.ParseTime(hour.In1);
                if (startTime != null)
                    startTime = new DateTime(date.Year, date.Month, date.Day, startTime.Value.Hour, startTime.Value.Minute, 0);

                var endTime = HappyHourTimestampProvider.ParseTime(hour.Out1);
                if (endTime != null)
                    endTime = new DateTime(date.Year, date.Month, date.Day, endTime.Value.Hour, endTime.Value.Minute, 0);

                HappyHourItem item = new HappyHourItem()
                {
                    Date = date,
                    StartTime = startTime,
                    EndTime = endTime,
                };
                happyHours.Add(item);
            }
            return happyHours;
        }

        private string HackData(HappyHoursLoginParameters loginParameters)
        {
            var url = "https://rt-pay.com/WebRP/";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            
            var nodes = doc.DocumentNode.
                Descendants("input").
                Where(x => x.Attributes["type"].Value == "hidden").ToList();

            var viewState = nodes.FirstOrDefault(c => c.Id == "__VIEWSTATE").Attributes["value"].Value;
            var viewStateGenerator = nodes.FirstOrDefault(c => c.Id == "__VIEWSTATEGENERATOR").Attributes["value"].Value;
            var eventValidation = nodes.FirstOrDefault(c => c.Id == "__EVENTVALIDATION").Attributes["value"].Value;

            var values = new Dictionary<string, string>
            {
                { "__VIEWSTATE", viewState },
                { "__VIEWSTATEGENERATOR", viewStateGenerator },
                { "__EVENTVALIDATION", eventValidation },
                { "txtUsername", loginParameters.Credentials.Username },
                { "txtPassword", loginParameters.Credentials.Password },
                { "txtEmployeeNo", loginParameters.Credentials.Number },
                { "btnLogin", "" },
                { "Remember", "rbRememberEmailEmployee" }

            };

            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            HttpClient client = new HttpClient(handler);
            var content = new FormUrlEncodedContent(values);
            var response = client.PostAsync("https://rt-pay.com/WebRP/Default.aspx", content);
            var responseString = response.Result.Content.ReadAsStringAsync().Result;
            Uri uri = new Uri("https://rt-pay.com/WebRP/Default.aspx");
            IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
            var webKitFormBoundary = GenerateWebKitFormBoundary();



            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(responseString);

            var failedElement = htmlDocument.DocumentNode.Descendants("span").Where(c => c.Id == "lblLogin_Failed").FirstOrDefault();
            if (failedElement != null)
                throw new HappyHourException(ErrorCode.InvalidCredentials);

            var specialValue = htmlDocument.DocumentNode.
                Descendants("input").
                Where(x => x.Attributes["type"].Value == "hidden" && x.Attributes["name"].Value == "ctl00$txtSK").FirstOrDefault().Attributes["value"].Value;

            HttpWebRequest oRequest = null;
            oRequest = (HttpWebRequest)HttpWebRequest.Create("https://rt-pay.com/WebRP/Private/Employee.aspx");
            oRequest.ContentType = "multipart/form-data; boundary=" + webKitFormBoundary.Substring(2, webKitFormBoundary.Length - 2);
            oRequest.Method = "POST";
            
            oRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            oRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            oRequest.KeepAlive = true;
            oRequest.Host = "rt-pay.com";
            oRequest.Referer = "https://rt-pay.com/WebRP/Private/Employee.aspx";
            oRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";

            oRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            oRequest.Headers.Add("Accept-Language", "en-US,en;q=0.8");
            oRequest.Headers.Add("Cache-Control", "max-age=0");
            oRequest.Headers.Add("Origin", "https://rt-pay.com");
            oRequest.Headers.Add("Upgrade-Insecure-Requests", "1");

            if (oRequest.CookieContainer == null)
            {
                oRequest.CookieContainer = new CookieContainer();
            }

            foreach (var cookie in responseCookies)
                oRequest.CookieContainer.Add(cookie);

            var parameters = CreateRequestStringFromChromeRequest();

            PostData pData = new PostData(webKitFormBoundary);

            foreach (var parameter in parameters)
                pData.Params.Add(new PostDataParam(parameter.Parameter, parameter.Value, parameter.Type));


            pData.Params.Where(c => c.Name == "__VIEWSTATEGENERATOR").FirstOrDefault().Value = viewStateGenerator;
            pData.Params.Where(c => c.Name == "ctl00$txtSK").FirstOrDefault().Value = specialValue;

            pData.Params.Where(c => c.Name == "drpYears").FirstOrDefault().Value = loginParameters.StartDate.Year.ToString();
            pData.Params.Where(c => c.Name == "_drpYears_state").FirstOrDefault().Value = "[{\"value\":" + loginParameters.StartDate.Year + ",\"text\":\"" + loginParameters.StartDate.Year +  "\",\"index\":0}]";

            pData.Params.Where(c => c.Name == "drpMonths").FirstOrDefault().Value = loginParameters.StartDate.Month.ToString();
            pData.Params.Where(c => c.Name == "_drpMonths_state").FirstOrDefault().Value = "[{\"value\":" + loginParameters.StartDate.Month +  ",\"text\":\"\u05d9\u05d5\u05dc\u05d9\",\"index\":6}]";

            var data = pData.GetPostData();

            var encoding = Encoding.UTF8;

            byte[] buffer = encoding.GetBytes(data);
            // Set content length of our data
            oRequest.ContentLength = buffer.Length;
            // Dump our buffered postdata to the stream, booyah
            var stream = oRequest.GetRequestStream();
            stream.Write(buffer, 0, buffer.Length);
            stream.Close();

            // get the response
            var oResponse = (HttpWebResponse)oRequest.GetResponse();
            string responseText = null;
            using (var reader = new System.IO.StreamReader(oResponse.GetResponseStream(), encoding))
            {
                responseText = reader.ReadToEnd();
            }

            return responseText;   
        }

        private string GenerateWebKitFormBoundary()
        {
            var str = "------WebKitFormBoundary";

            List<char> letters = new List<char>();
            for(var i = 'a'; i <= 'z'; i++)
            {
                letters.Add(i);
            }
            for (var i = 'A'; i <= 'Z'; i++)
            {
                letters.Add(i);
            }
            for (var i = 0; i <= 9; i++)
            {
                letters.Add(char.Parse(i.ToString()));
            }

            var rand = new Random();

            for(var i = 0; i < 16; i++)
            {
                var index = rand.Next(0, letters.Count);
                str += letters[index];
            }

            return str;
        }

        private string GetNameFromLine(string line)
        {
            var str = line.Substring(38, line.Length - 39);
            var index = str.IndexOf(';');
            if (index == -1)
                return str;

            str = str.Substring(0, index - 1);
            return str;
        }

        private IEnumerable<PostParameter> CreateRequestStringFromChromeRequest()
        {
            IList<PostParameter> parameters = new List<PostParameter>();

            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader(ConfigHelper.Config.ChromeRequestFile);

            while ((line = file.ReadLine()) != null)
            {
                PostDataParamType type;

                if (line.Contains("WebKitFormBoundary") && line.EndsWith("--"))
                    break;

                if (line.Contains("WebKitFormBoundary"))
                    line = file.ReadLine();

                if (line == null)
                    break;

                var parameter = GetNameFromLine(line);

                var value = file.ReadLine();

                if (value == "Content-Type: application/octet-stream")
                {
                    value = file.ReadLine();
                    type = PostDataParamType.File;
                }
                else
                {
                    type = PostDataParamType.Field;
                }

                value = file.ReadLine();

                parameters.Add(new PostParameter()
                {
                    Parameter = parameter,
                    Value = value,
                    Type = type
                });
            }

            file.Close();

            return parameters;
        }
    }
}
