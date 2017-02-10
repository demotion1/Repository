using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Finovex;
using System.IO;
using System.Web;
using System.Net.Http.Headers;

namespace Finovex.Controllers
{
    public class filesController : ApiController
    {
        private FinovexEntities db = new FinovexEntities();

        // GET: api/files
        public IEnumerable<FileList> GetFileLists()
        {
            try
            {
            // Authenticate user
            // return BadRequest("403");

            List<FileList> listOfFiles = new List<FileList>();
            FileList FileList = new FileList();
            foreach (var file in db.FileLists)
            {
                FileList.fileid = file.fileid;
                FileList.filename = file.filename;
                FileList.creationdDate = file.creationdDate;
                FileList.modificationDate = file.modificationDate;
                FileList.filesize = file.filesize;
                FileList.mimeType = file.mimeType;
                FileList.fileUser = file.fileUser;
                listOfFiles.Add(FileList);
                FileList = new FileList();
            }
            IEnumerable<FileList> files = listOfFiles;
            //return request.CreateResponse(HttpStatusCode.OK, user); return Ok("200");
            return files;

            }
            catch
            {
                return null;
                //return BadRequest("500");
            }
        }

        // Download File
        public HttpResponseMessage GetFile(string filename, string mimeType)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            string root = HttpContext.Current.Server.MapPath("~/App_Data/");

            try
            {
                // file exists
                if (!File.Exists(root + filename))
                    return response;

                response.Content = new StreamContent(new FileStream(root + filename, FileMode.Open, FileAccess.Read));
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = filename;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            }
            catch
            {
            }

            return response;
        }

        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> UploadFile(string filename, string mimeType, string filesize, int userid)
        {
            string root = HttpContext.Current.Server.MapPath("~/App_Data/");
            BinaryWriter Writer = null;

            try
            {
                // Authenticate
                // return BadRequest("403");

                // Validations
                // file size
                if (Int32.Parse(filesize) > 1000000)
                    return BadRequest("400");
                // file exists
                if (File.Exists(root + filename))
                    return BadRequest("400");

                // Read the form data.
                string strFile = await Request.Content.ReadAsStringAsync();

                // Save File
                Byte[] bytes = Convert.FromBase64String(strFile.Substring(strFile.IndexOf(",") + 1));
                Writer = new BinaryWriter(File.OpenWrite(root + filename));

                // Writer raw data                
                Writer.Write(bytes);
                Writer.Flush();
                Writer.Close();

                // Add entry in database
                FileList FileList = new FileList();
                FileList.filename = filename;
                FileList.filesize = filesize;
                FileList.fileUser = userid;
                FileList.mimeType = mimeType;
                FileList.creationdDate = DateTime.Now;
                FileList.modificationDate = DateTime.Now;

                db.FileLists.Add(FileList);

                // Add entry in UploadHistory
                UploadHistory log = new UploadHistory();
                log.userid = userid;
                log.uploadTimestamp = DateTime.Now;
                log.filename = filename;
                db.UploadHistories.Add(log);

                db.SaveChanges();
            }
            catch (System.Exception e)
            {
                Writer.Flush();
                Writer.Close();

                return BadRequest("500");
            }

            return Ok("204");
        }

        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> PatchFile(int fileid, string mimeType, string filesize, int userid)
        {
            string root = HttpContext.Current.Server.MapPath("~/App_Data/");
            BinaryWriter Writer = null;

            try
            {
                // authenticate
                // return BadRequest("403");

                // get file info
                var files = (from j in db.FileLists
                             where j.fileid == fileid
                             select j);
                var file = files.FirstOrDefault();  // needed to get the file name

                // validation
                if (files.Count() != 1)
                    return BadRequest("400");
                if (Int32.Parse(filesize) > 1000000)
                    return BadRequest("400");
                if (!File.Exists(root + file.filename))
                    return BadRequest("400");

                // Read the form data.
                string strFile = await Request.Content.ReadAsStringAsync();

                // Delete File
                File.Delete(root + file.filename);

                // Save File
                Byte[] bytes = Convert.FromBase64String(strFile.Substring(strFile.IndexOf(",") + 1));
                Writer = new BinaryWriter(File.OpenWrite(root + file.filename));

                // Writer raw data                
                Writer.Write(bytes);
                Writer.Flush();
                Writer.Close();

                // Update entry in database
                file.filesize = filesize;
                file.mimeType = mimeType;
                file.modificationDate = DateTime.Now;

                // Add entry in UpdateHistory
                UpdateHistory log = new UpdateHistory();
                log.userid = userid;
                log.updateTimestamp = DateTime.Now;
                log.prevfilename = file.filename;
                log.filename = file.filename;
                db.UpdateHistories.Add(log);

                db.SaveChanges();
            }
            catch (System.Exception e)
            {
                Writer.Flush();
                Writer.Close();
                
                return BadRequest("500");
            }

            return Ok("204");
        }

        // DELETE: api/FileLists/id
        [ResponseType(typeof(FileList))]
        public async Task<IHttpActionResult> DeleteFileList(int id)
        {
            string root = HttpContext.Current.Server.MapPath("~/App_Data/");

            try
            {
                // if user not authenticated
                // return BadRequest("403");

                FileList fileList = await db.FileLists.FindAsync(id);
                if (fileList == null)
                {
                    return BadRequest("500");
                }

                // If file does not exist
                if (!File.Exists(root + fileList.filename))
                    return BadRequest("400");

                // Delete file
                File.Delete(root + fileList.filename);
                
                // Delete database entry
                db.FileLists.Remove(fileList);

                //// Add entry in DeleteHistory
                //DeleteHistory log = new DeleteHistory();
                //log.userid = userid;
                //log.deleteTimestamp = DateTime.Now;
                //log.filename = file.filename;
                //db.DeleteHistories.Add(log);

                await db.SaveChangesAsync();
                
            }
            catch (System.Exception e)
            {
                return BadRequest("500");
            }

            return Ok("204");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FileListExists(int id)
        {
            return db.FileLists.Count(e => e.fileid == id) > 0;
        }
    }
}