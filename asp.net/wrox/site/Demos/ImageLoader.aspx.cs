using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demos_ImageLoader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string url = TextBox1.Text;
        ImageDownloader downloader = new ImageDownloader();
        
        //register observer for log event before start downloading:
        downloader.logTextUpdated += new ImageDownloader.logDelegate(downloader_logTextUpdated);

        if (string.IsNullOrEmpty(url))
            downloader.Start();
        else
            downloader.Start(url);
    }

    void downloader_logTextUpdated(object aText)
    {
        this.TextArea1.Value = this.TextArea1.Value.Insert(0, aText.ToString()+"\n\n");
    }
}