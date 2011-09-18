using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
//using System.IO.IsolatedStorage;

/// <summary>
/// LoadImage from specific HTML url
/// </summary>
public class ImageDownloader
{
    //for observers:
    public delegate void logDelegate(object aText);
    public event logDelegate logTextUpdated;
    object _logText;
    public object LogText
    {
        set {
            _logText = value;
            //fire event
            logTextUpdated(_logText);
        }
        get {
            return _logText; 
        }
    }


	public ImageDownloader()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private List<Uri> imgUriList = new List<Uri>();
    private List<Uri> savedImgList = new List<Uri>();
    private Dictionary<Uri, string> dict = new Dictionary<Uri, string>();
    private int topicPageCount = 0; 
    private int downloadingImgListIndex = 0;

    public void Start()
    {
        //for  test: temp stop getting images before storage full!
        BeginParseTopicPageURL("http://web.6park.com/bbs/first1.shtml");
        //initializeFileList();
    }

    public void Start(string url)
    {
        BeginParseTopicPageURL(url); 
    }

    //async topPage url parsing:
    private void BeginParseTopicPageURL(string url)
    {
        WebClient wc = new WebClient();
        wc.DownloadStringCompleted +=new DownloadStringCompletedEventHandler(wc_topic_DownloadStringCompleted);
        wc.DownloadStringAsync(new Uri(url));
    }

    void wc_topic_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
    { 
        string htmlPage = e.Result;
        System.Diagnostics.Debug.WriteLine("---------topic html---------"+sender.ToString());                                    
        System.Diagnostics.Debug.WriteLine(htmlPage);                                    

        //string imgUrlPattern = "<img\\ssrc[^>]*jpg\">";

        //for top head only
        //string topicUrlPattern = "<br><!--top:\\s\\d+--><li><a href=\"[^>]*html";

        /***
         * when verify regex with www.regexlib.com, use un-escaped expression,
         * otherwise it wont recognise. e.g: <!--top:\s\d+--><li><a href="[^>]*html
         * with \\s or \\d wont work. 
         ***/
        string topicUrlPattern = "<!--top:\\s\\d+--><li><a href=\"[^>]*html";
        MatchCollection matches = Regex.Matches(htmlPage, topicUrlPattern,
                                            RegexOptions.IgnoreCase);

        topicPageCount = matches.Count;
        foreach (Match m in matches)
        {
            System.Diagnostics.Debug.WriteLine(m.ToString());
            //BeginParseImageURL(m.ToString().Remove(0,34));
            BeginParseImageURL(m.ToString().Remove(0,30));

            //for test, to be save time by only get one topic.
//            break;
        }
    }

    // asych url string downloading functions
    private void BeginParseImageURL(string url)
    {
        WebClient wc = new WebClient();
        wc.Encoding = System.Text.Encoding.GetEncoding(936);
        wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
        wc.DownloadStringAsync(new Uri(url));
    }

    // start parsing image file url after containing html page downloaded
    void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
    {
        string htmlPage = e.Result;
        System.Diagnostics.Debug.WriteLine("---------html---------"+sender.ToString());                                    
        System.Diagnostics.Debug.WriteLine(htmlPage);                                    

        int startIndex = htmlPage.IndexOf("<title>");
        int endIndex = htmlPage.IndexOf("</title>");
        string titleText = htmlPage.Substring(startIndex + 7, endIndex - startIndex - 7);

        //string imgUrlPattern = "<img\\ssrc[^>]*jpg\">";
        string imgUrlPattern = "http:[^>]*jpg";
        MatchCollection matches = Regex.Matches(htmlPage, imgUrlPattern,
                                            RegexOptions.IgnoreCase);

        foreach (Match m in matches)
        {
            System.Diagnostics.Debug.WriteLine(m.ToString());
            try
            {
                Uri imgUri = new Uri(m.ToString());
                imgUriList.Add(imgUri);
                dict.Add(imgUri, titleText);
           }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                continue;
            }
       }

        System.Diagnostics.Debug.WriteLine("\n---------hiee---------"+matches.Count);                                    
        //throw new NotImplementedException();

        topicPageCount -= 1;
        if (topicPageCount == 0)
            BeginDownloadImage(imgUriList.ElementAt(0));
    }

    void BeginDownloadImage(Uri imgUri)
    { 
        //get image file
        WebClient wc = new WebClient();
        wc.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(wc_DownloadFileCompleted);
        string fileName = imgUri.ToString();//imgUriList.ElementAt(downloadingImgListIndex).ToString();
        string url = fileName;
        string title = dict[imgUri];
        title = title.Replace('/','_').Replace(':','_').Replace('[','_').Replace(']','_');
        fileName = fileName.Replace('/','_').Replace(':','_');
        string dirName = @"D:\TDDOWNLOAD\6park_pix\";
        string filePath = dirName + title + fileName;
        if (!File.Exists(filePath))
        {
            wc.DownloadFileAsync(imgUri, filePath);
            this.LogText=DateTime.Now.ToString()+"---downloading:"+ filePath;
            System.Diagnostics.Debug.WriteLine(LogText);
        }
        else
        {
            downloadNextImage();
            this.LogText=DateTime.Now.ToString()+"---FILE SKIPED:"+ filePath;
            System.Diagnostics.Debug.WriteLine(LogText);
        }
    }

    void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        downloadNextImage();
    }

    private void downloadNextImage()
    {
        this.downloadingImgListIndex += 1;
        if (downloadingImgListIndex < imgUriList.Count())
            BeginDownloadImage(imgUriList.ElementAt(downloadingImgListIndex));
        else
        {
            this.LogText = "---All image download task DONE.";
            System.Diagnostics.Debug.WriteLine(LogText);
        }
    }
}
