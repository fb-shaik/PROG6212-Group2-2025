using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Tracker_WPF
{
    //Custom class representing a parcel
    class Parcel
    {
    
        //define 3 fields (length, width, height)
        public int Lenght { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        //constructor to initialize the above
        public Parcel(int l, int w, int h) 
        {
            Lenght = l;
            Width = w; 
            Height = h;
        }

        //operator overloading: combine the dimensions of two parcels
        public static Parcel operator +(Parcel a, Parcel b) 
        {
            return new Parcel
                (
                    a.Lenght + b.Lenght, //combines the length
                    Math.Max(a.Width, b.Width),//Max width taken
                    Math.Max(a.Height, b.Height) //Max height taken
                    );
                
        } 

    }
}
