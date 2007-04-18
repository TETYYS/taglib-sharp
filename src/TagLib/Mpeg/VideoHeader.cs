using System;

namespace TagLib.Mpeg
{
	public class VideoHeader
	{
		int width;
      int height;
      int frame_rate_index;
      int bitrate;
      
      private static readonly double[] frame_rates = new double[9]
      {
         0, 24000d/1001d, 24, 25, 30000d/1001d, 30, 50, 60000d/1001d, 60
      };
      
      public int    Width     {get {return width;}}
      public int    Height    {get {return height;}}
      public double FrameRate {get {return frame_rate_index < 9 ? frame_rates [frame_rate_index] : 0;}} 
      public int    Bitrate   {get {return bitrate;}}
      
		public VideoHeader (TagLib.File file, long offset)
		{
         file.Seek (offset);
         ByteVector data = file.ReadBlock (7);
         
         width = data.Mid (0, 2).ToShort () >> 4;
         height = data.Mid (1, 2).ToShort () & 0x0FFF;
         
         frame_rate_index = data [3] & 0x0F;
         
         bitrate = (int)((data.Mid (4, 3).ToUInt () >> 6) & 0x3FFFF);
		}
	}
}