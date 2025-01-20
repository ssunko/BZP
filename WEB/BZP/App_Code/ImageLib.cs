using System;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

/// <summary>
/// Summary description for ImageLib
/// </summary>
public static class ImageLib {

    public const int ThumbnailWidth = 80;
	public const int ThumbnailHeight = 80;
    public const int ImageWidth = 400;
	public const int ImageHeight = 400;


    public static string GetFileName(string sFN){
        int iLIO = sFN.LastIndexOf('\\') + 1;
        string sFName;
        if (iLIO == 0) {
            sFName = sFN;
        } else {
            sFName = sFN.Substring(iLIO);
        }
        return sFName;
    }

	public static string UploadImage(System.Web.HttpPostedFile file, string szImgPath, string szTmbPath, string szFileName, ref Int16 iPicNo){
		try{
		    Image image;
		    using(image = Image.FromStream(file.InputStream)){
                if ( image.Width > ImageWidth || image.Height > ImageHeight )
		            image = resizeImage(image, new Size(ImageWidth,ImageHeight),true);
                using(Bitmap bitmap = new Bitmap(image)){
                    saveJpeg(szImgPath + "\\" + szFileName, bitmap, 85L);
		        }
                //Thumbnail 
                image = CropToSize(image, ThumbnailWidth, ThumbnailHeight);
                using(Bitmap bitmap = new Bitmap(image)){				   
                    saveJpeg(szTmbPath + "\\" + szFileName, bitmap, 85L);
		        }
            }
            iPicNo += 1;
        }catch(Exception ex){
            string ErrCode = "0";
            if (ex.Message == "Parameter is not valid.")
                ErrCode = "510"; // NOT AN IMAGE
            return ErrCode;
        }
        return "";
	}

	public static void saveJpeg(string path, Bitmap img, long quality){
       // Encoder parameter for image quality            
       EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);
       // Jpeg image codec
       ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");
       if(jpegCodec == null)
          return;
       EncoderParameters encoderParams = new EncoderParameters(1);
       encoderParams.Param[0] = qualityParam;
       img.Save(path, jpegCodec, encoderParams);
    }

    public static ImageCodecInfo getEncoderInfo(string mimeType){
       // Get image codecs for all image formats
       ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
       // Find the correct image codec
       for (int i = 0; i < codecs.Length; i++)
          if (codecs[i].MimeType == mimeType)
             return codecs[i];
       return null;
    }

    public static Image resizeImage(Image imgToResize, Size size, bool ToSmallestSize){
        int sourceWidth = imgToResize.Width;
        int sourceHeight = imgToResize.Height;
       

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)size.Width / (float)sourceWidth);
        nPercentH = ((float)size.Height / (float)sourceHeight);

        if(ToSmallestSize){
            nPercent = (nPercentH < nPercentW) ? nPercentH : nPercentW;
        }else{
            nPercent = (nPercentH > nPercentW) ? nPercentH : nPercentW;
        }        

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap b = new Bitmap(destWidth, destHeight);
        Graphics g = Graphics.FromImage((Image)b);
        g.InterpolationMode =  InterpolationMode.HighQualityBicubic;

        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        g.Dispose();

        return (Image)b;
    }
    public static Image cropImage(Image img, Rectangle cropArea){
        Bitmap bmpImage = new Bitmap(img);
        Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        return (Image)(bmpCrop);
    }
    public static Image CropToSize(Image imgToCrop, int iWidth, int iHeight){
        Image RetImg = resizeImage(imgToCrop,  new Size(iWidth + 2, iHeight + 2), false);
        int sourceWidth = RetImg.Width;
        int sourceHeight = RetImg.Height;
        int X = 0, Y = 0;
        Size size = new Size(iWidth, iHeight);
        if(sourceWidth > size.Width){
            X = (sourceWidth - size.Width) / 2;
        }
        if(sourceHeight > size.Height){
            Y = (sourceHeight - size.Height) / 2;
        }
        RetImg = cropImage(RetImg, new Rectangle(X, Y, size.Width, size.Height));
        return RetImg;
    }

}//static class ClassifiedLib
