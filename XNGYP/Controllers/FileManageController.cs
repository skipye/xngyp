﻿using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XNGYP.Controllers
{
    public class FileManageController : Controller
    {
        public ActionResult Index(string TabId)
        {
            ViewBag.TabId = TabId;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UploadImg()
        {
            //定义错误消息
            string msg = "";
            //接受上传文件
            HttpPostedFileBase hp = Request.Files["upImage"];
            if (hp != null)
            {
                //获取上传目录 转换为物理路径
                string uploadPath = Server.MapPath("~/UpLoads/");
                //获取文件名
                string fileName = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(hp.FileName);
                //获取文件大小
                long contentLength = hp.ContentLength;
                //文件不能大于1M
                if (contentLength >20* 1024 * 1024)
                {
                    msg = "文件大小超过限制要求.";
                    return Content("0");
                }
                //保存文件的物理路径
                string saveFile = uploadPath + fileName;
                //保存文件
                hp.SaveAs(saveFile);
                msg = "/UpLoads/" + fileName;
            }

            try
            {
                return Content(msg);
                //msg = "1";
            }
            catch
            {
                msg = "上传失败.";
                return Content(msg);
            }
        }
        [HttpPost]
        public ActionResult UploadImages()
        {
            try
            {
                HttpPostedFileBase hp = Request.Files["Filedata"];
                //HttpPostedFile postedFile = context.Request.Files["Filedata"];
                string tempPath = string.Empty;
                string originalpath = string.Empty;
                string publishedPath = string.Empty;
                string thumbnailsPath = string.Empty;
                //tempPath = "/" + System.Configuration.ConfigurationManager.AppSettings["FolderGalleryPath"] + "/{ReplaceThisPath}/" + date.Year + "/" + date.Month + "/";
                tempPath = "/Uploads/Gallery/{ReplaceThisPath}/";
                originalpath = HttpContext.Server.MapPath(tempPath.Replace("{ReplaceThisPath}", "Original"));
                publishedPath = HttpContext.Server.MapPath(tempPath.Replace("{ReplaceThisPath}", "Published"));
                thumbnailsPath = HttpContext.Server.MapPath(tempPath.Replace("{ReplaceThisPath}", "Thumbnails"));
                tempPath = tempPath.Replace("{ReplaceThisPath}", "Thumbnails");
                //string filename = postedFile.FileName;
                string filename = hp.FileName;
                string sExtension = filename.Substring(filename.LastIndexOf('.'));
                //CreateSubDir(originalpath);
                //CreateSubDir(publishedPath);
                //CreateSubDir(thumbnailsPath);
                string sNewFileName = Guid.NewGuid().ToString().ToUpper();
                //保存原图
                hp.SaveAs(originalpath + @"/" + sNewFileName + sExtension);
                //postedFile.SaveAs(originalpath + @"/" + sNewFileName + sExtension);
                //保存加水印图
                ImgProcess.MakeThumbnail(originalpath + @"/" + sNewFileName + sExtension, publishedPath + @"/" + sNewFileName + sExtension, 660, 450, null, null);
                //保存缩略图
                ImgProcess.MakeThumbnail(originalpath + @"/" + sNewFileName + sExtension, thumbnailsPath + @"/" + sNewFileName + sExtension, 150, 100, null, null);
                //context.Response.Write(tempPath + sNewFileName + sExtension);
                //context.Response.StatusCode = 200;
                return Content(tempPath + sNewFileName + sExtension);
            }
            catch (Exception ex)
            {
                //context.Response.Write("错误: " + ex.Message);
                return Content("错误: " + ex.Message);
            }
        }
        [HttpPost]
        public string UploadTextImages()
        {
            //定义错误消息
            string msg = "";
            //接受上传文件
            HttpPostedFileBase hp = Request.Files["imgFile"];
            if (hp != null)
            {
                //获取上传目录 转换为物理路径
                string uploadPath = Server.MapPath("~/UpLoads/");
                //获取文件名
                string fileName = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(hp.FileName);
                //获取文件大小
                long contentLength = hp.ContentLength;
                //文件不能大于1M
                if (contentLength > 1024 * 1024 * 10)
                {
                    msg = "文件大小超过限制要求.";
                    return msg;
                }
                //保存文件的物理路径
                string saveFile = uploadPath + fileName;
                //保存文件
                hp.SaveAs(saveFile);
                msg = "/UpLoads/" + fileName;

                Hashtable hash = new Hashtable();
                hash["error"] = 0;
                hash["url"] = saveFile;

                //return JsonMapper.ToJson(hash);
            }
            try
            {
                return msg;
                //msg = "1";
            }
            catch
            {
                msg = "上传失败.";
                return msg;
            }
        }
    }
}
