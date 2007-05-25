/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : id3v2frame.cpp from TagLib
 ***************************************************************************/

/***************************************************************************
 *   This library is free software; you can redistribute it and/or modify  *
 *   it  under the terms of the GNU Lesser General Public License version  *
 *   2.1 as published by the Free Software Foundation.                     *
 *                                                                         *
 *   This library is distributed in the hope that it will be useful, but   *
 *   WITHOUT ANY WARRANTY; without even the implied warranty of            *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU     *
 *   Lesser General Public License for more details.                       *
 *                                                                         *
 *   You should have received a copy of the GNU Lesser General Public      *
 *   License along with this library; if not, write to the Free Software   *
 *   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  *
 *   USA                                                                   *
 ***************************************************************************/

using System.Collections;
using System;

namespace TagLib.Id3v2
{
   public class PlayCountFrame : Frame
   {
      #region Private Properties
      private ulong play_count = 0;
      #endregion
      
      
      
      #region Constructors
      public PlayCountFrame (ByteVector data, byte version) : base (data, version)
      {
         SetData (data, 0, version, true);
      }
      
      public PlayCountFrame () : base ("PCNT", 4)
      {}
      
      protected internal PlayCountFrame (ByteVector data, int offset, FrameHeader h, byte version) : base (h)
      {
         SetData (data, offset, version, false);
      }
      #endregion
      
      
      
      #region Public Properties
      public ulong PlayCount
      {
         get {return play_count;}
         set {play_count = value;}
      }
      #endregion
      
      
      
      #region Public Methods
      public override string ToString ()
      {
         return null;
      }
      #endregion
      
      
      
      #region Public Static Methods
      public static PlayCountFrame Get (Tag tag, bool create)
      {
         foreach (Frame f in tag)
            if (f is PlayCountFrame)
               return f as PlayCountFrame;
         
         if (!create)
            return null;
         
         PlayCountFrame frame = new PlayCountFrame ();
         tag.AddFrame (frame);
         return frame;
      }
      #endregion
      
      
      
      #region Protected Methods
      protected override void ParseFields (ByteVector data, byte version)
      {
         play_count = data.ToULong ();
      }
      
      protected override ByteVector RenderFields (byte version)
      {
         ByteVector data = ByteVector.FromULong (play_count);
         while (data.Count > 4 && data [0] == 0)
            data.RemoveAt (0);
         
         return data;
      }
      #endregion
   }
}