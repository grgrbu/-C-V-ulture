using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Routes
{
    public class User
    {
        private string login, password;
        public List<int> preferences = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public User(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
    }

    // to load images from csv:
    // https://stackoverflow.com/questions/5282999/reading-csv-file-and-storing-values-into-an-array
    //

    public class Painting
    {
        public string name, author, info, receipt;
        public string picture_url;
        public int style, place;
        List<string> tags;
        public Painting(string name, string author, string info = "", string receipt = "", string picture_url = "", int style = -1, int place = -1)
        {
            this.name = name;
            this.author = author;
            this.info = info;
            this.receipt = receipt;
            this.picture_url = picture_url;
            this.style = style;
            this.place = place;
        }
        public void setTags(List<string> tags)
        {
            this.tags = tags;
        }
        public int countTags(List<string> search_tags)
        {
            int cnt = 0;
            foreach (string t1 in tags)
            {
                foreach (string t2 in search_tags)
                {
                    if (t1 == t2)
                    {
                        cnt++;
                        break;
                    }
                }
            }
            return cnt;
        }
    }

    public class Route
    {
        private string name, description;
        string picture_url;
        List<Painting> paintings = new List<Painting>();
        int duration;
        public Route(string name, string description, string picture_url = "", int duration=0)
        {
            this.name = name;
            this.description = description;
            this.picture_url = picture_url;
            this.duration = duration;
        }
    }

}