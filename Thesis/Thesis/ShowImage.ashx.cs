using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Thesis
{
    /// <summary>
    /// Summary description for ShowImage
    /// </summary>
    public class ShowImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String empno;
            if (context.Request.QueryString["id"] != null)
                empno = context.Request.QueryString["id"].ToString();
            else
                throw new ArgumentException("No parameter specified");

            context.Response.ContentType = "image/jpeg";
            Stream strm = ShowEmpImage(empno);
            byte[] buffer = new byte[4096];
            int byteSeq = strm.Read(buffer, 0, 4096);

            while (byteSeq > 0)
            {
                context.Response.OutputStream.Write(buffer, 0, byteSeq);
                byteSeq = strm.Read(buffer, 0, 4096);
            }       
        }

        public Stream ShowEmpImage(String empno)
        {
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {
                var data=entity.Users.Where(p => p.faculty_id .Equals(empno)).FirstOrDefault();
                object img = data.image;
                return new MemoryStream((byte[])img);

            }
            
           
           
           
        }





        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}