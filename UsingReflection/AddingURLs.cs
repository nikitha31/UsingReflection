using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using UsingReflection.Models;

namespace UsingReflection
{
    public class AddingURLs
    {
        public static void addToDB()
        {
            ReflectionsEntities db = new ReflectionsEntities();
            GetNames getNames = new GetNames();
            List<string> urlList = getNames.getDetails();
            foreach (var cc in urlList)
            {
                //var checkURL = db.URLs.SingleOrDefault(m => m.URL1.Equals(url));
                var checkURL = db.URLs.FirstOrDefault(u => u.URL1.Equals(cc));
                if (checkURL == null)
                {
                   // Debug.Print(cc);
                    db.URLs.Add(new URL { URL1 = cc.ToString() });
                    db.SaveChanges();
                }
            }

        }
    }
}