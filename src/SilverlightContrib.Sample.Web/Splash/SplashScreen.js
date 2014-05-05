function onSourceDownloadProgressChanged(sender, eventArgs) {
    var myHost = document.getElementById("Xaml1");
    var rectBar = myHost.content.findName("rectBar");
    var rectBorder = myHost.content.findName("rectBorder");
    if(eventArgs.progress)
        rectBar.Width = eventArgs.progress * rectBorder.Width;
    else
        rectBar.Width = eventArgs.get_progress() * rectBorder.Width;
}

