
    #region Calling Api
    string MyUrl = string.Format("http://192.168.1.210:4422/");
    string MyUrlApiUri = string.Format("api/in-out-pallet-permission/has-permission?rfid={0}&readerIp={1}&antenna={2}", "any", "any", "any");

    System.Net.Http.HttpClient cons = new System.Net.Http.HttpClient();
    cons.BaseAddress = new Uri(MyUrl);
    cons.DefaultRequestHeaders.Accept.Clear();
    cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    HttpResponseMessage response = cons.GetAsync(MyUrlApiUri).Result;
    if (response.StatusCode == System.Net.HttpStatusCode.OK)
    {
      
        Console.WriteLine("Success");
    }
    else
    {
        Console.WriteLine("Error");
       
        //Something has gone wrong, handle it here
    }
    #endregion



//string[] lines = File.ReadAllLines("F:\\integration\\integration_20230621.txt");
//List<string> result = new List<string>();
//for (int i=0;i< lines.Length;i++)
//{
//    if (lines[i].StartsWith("Request completed in "))
//    {
//        int index= 20;
//        string seconds= lines[i].Substring(index, lines[i].LastIndexOf("s")- 21);
//        seconds= seconds.Trim();
//        int secondsCount= int.Parse(seconds);
//        if(secondsCount>3000)
//        {
//            for(int k=i;k<=i+4 ;k++)
//            {
//                result.Add(lines[k]);
//            }
//        }
//    }
//}
//using (TextWriter tw = new StreamWriter("F:\\New folder\\integration_20230621.txt"))
//{
//    foreach (String s in result)
//        tw.WriteLine(s);
//}
//Console.WriteLine("done...");
//Console.ReadKey();
