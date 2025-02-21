﻿namespace FlaNium.Desktop.Driver.CommandExecutors
{
    using System;
    using System.Drawing.Imaging;
    using System.IO;
    using FlaNium.Desktop.Driver.Common;
    using global::FlaUI.Core.Capturing;


    class CustomScreenshotExecutor : CommandExecutorBase
        {
        protected override string DoImpl()
        {
            var imageFormatStr = this.ExecutedCommand.Parameters["format"].ToString();


            ImageFormat imageFormat = ImFormat.GetImageFormat(imageFormatStr);

            CaptureImage captureImage = Capture.Screen();
            MemoryStream memoryStream = new MemoryStream();

            captureImage.Bitmap.Save((Stream)memoryStream, imageFormat);


            return this.JsonResponse(ResponseStatus.Success, (object)Convert.ToBase64String(memoryStream.ToArray()));
        }
    }
}
