using System;

namespace IOT_emulator
{
    class Program
    {
        static int activityId = 0;
        static int duration = 560;

        static void Main(string[] args)
        {
            OnEventHappened(5);
        }

        public static void OnEventHappened(int UserId)
        {
            string url = buildUrl(5,activityId, duration);
            HttpPost(url,String.Empty);
            Console.WriteLine("Okey");
            Console.ReadLine();
        }

        private static string buildUrl(int userId,int activityId, int duration)
        {
            return "http://43a77d1f.ngrok.io/" + userId + "/" + activityId + "/" + duration;
        }

        public static string HttpPost(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            req.Proxy = new System.Net.WebProxy("http://43a77d1f.ngrok.io", true);
            //Add these, as we're doing a POST
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }
    }
}
