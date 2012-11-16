using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Varldsklass.Web.ViewModels;
using Varldsklass.Domain.Repositories.Abstract;
using Varldsklass.Domain.Entities;
using System.Data.Entity;

namespace Varldsklass.Web.Controllers
{
    public class FileUploadController : Controller 
    {
        private IRepository<Post> _postRepo;
        private IRepository<Category> _categoryRepo;
        private IRepository<Image> _imgRepo;
        private IRepository<Event> _eventRepo;
        private IRepository<Location> _locationRepo;

        public FileUploadController(IRepository<Post> repo, IRepository<Category> category, IRepository<Image> Image, IRepository<Event> Event, IRepository<Location> Location)
        {
            _imgRepo = Image;
            _postRepo = repo;
            _categoryRepo = category;
            _eventRepo = Event;
            _locationRepo = Location;
        }

        public ActionResult _FileUploadPartial(int postID = -1,  int categoryID = -1, int eventID = -1, int locationID = -1, bool badges = false)
        {
            var fuVM = new FileUploadViewModel();

            Post post = new Post();
            Category category = new Category();
            Event Event = new Event();
            Location Location = new Location();
            if (badges == true) {
                fuVM.Badges = true;
            }

            if (postID != -1) {
                post = _postRepo.FindAll().Where(p => p.ID == postID).Include(i => i.Images).Include(b => b.Badges).FirstOrDefault();
                fuVM.post = post;
            } if (categoryID != -1)
            {
                category = _categoryRepo.FindByID(categoryID);
                fuVM.Category = category;
            } if (eventID != -1)
            {
                Event = _eventRepo.FindByID(eventID);
                fuVM.Event = Event;
            } if (locationID != -1)
            {
                Location = _locationRepo.FindByID(locationID);
                fuVM.Location = Location;
            }
            var uploadedFiles = new List<UploadedFile>();

            var files = Directory.GetFiles(Server.MapPath("~/Content/image-uploads"));

            foreach (var file in files)
            {

                var uploadedFile = new UploadedFile() { Title = Path.GetFileName(file) };

                uploadedFile.PathUrl = ("/Content/image-uploads/") + Path.GetFileName(file);
                if (_imgRepo.FindAll().Where(i => i.ImagePath == uploadedFile.PathUrl).FirstOrDefault() == null)
                {
                    Image imageObj = new Image();
                    imageObj.ImagePath = uploadedFile.PathUrl;
                    _imgRepo.Save(imageObj);
                }
                
                var fileInfo = new FileInfo(file);
                if (postID >= 1 && (badges == false)) {
                        foreach (var postimage in post.Images)
                        {
                            if (postimage.ImagePath == uploadedFile.PathUrl)
                            {
                                uploadedFile.Checked = true;
                            }
                        }
                } if (postID >= 1 && (badges == true))
                {
                    foreach (var badgeImage in post.Badges)
                    {
                        if (badgeImage.ImagePath == uploadedFile.PathUrl)
                        {
                            uploadedFile.Checked = true;
                        }
                    }
                } if (categoryID >= 1)
                { 
                        foreach (var categoryimage in category.Images)
                        {
                            if (categoryimage.ImagePath == uploadedFile.PathUrl)
                            {
                                uploadedFile.Checked = true;
                            }
                        }
                } if (eventID >= 1)
                {
                    foreach (var eventimage in Event.Images)
                    {
                        if (eventimage.ImagePath == uploadedFile.PathUrl)
                        {
                            uploadedFile.Checked = true;
                        }
                    }
                } if (locationID >= 1)
                {

                    foreach (var locationimage in Location.Images)
                    {
                        if (locationimage.ImagePath == uploadedFile.PathUrl)
                        {
                            uploadedFile.Checked = true;
                        }
                    }
                }
                
                uploadedFiles.Add(uploadedFile);
            }
            fuVM.UploadedFiles = uploadedFiles;
  
            return PartialView("_FileUploadPartialView", fuVM);
        }


        public ActionResult FileUpload()
        {
            var fuVM = new FileUploadViewModel();
            
            var uploadedFiles = new List<UploadedFile>();

            var files = Directory.GetFiles(Server.MapPath("~/Content/image-uploads"));

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);

                var uploadedFile = new UploadedFile() { Title = Path.GetFileName(file) };

                uploadedFile.PathUrl = ("/Content/image-uploads/") + Path.GetFileName(file);
                uploadedFiles.Add(uploadedFile);
                
            }
            fuVM.UploadedFiles = uploadedFiles;
            return View(fuVM); 
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            var fuVM = new FileUploadViewModel();
            var uploadedFiles = new List<UploadedFile>();

            var files = Directory.GetFiles(Server.MapPath("~/Content/image-uploads"));
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                var fullFileName = Path.GetFileName(file.FileName);
                var fileName = Path.GetFileNameWithoutExtension(fullFileName);
                var folder = "~/Content/image-uploads";
                FileInfo fileInfo = new FileInfo(fullFileName);
                if (fileInfo.Extension.ToLower() == ".jpg" || fileInfo.Extension.ToLower() == ".jpeg" ||
                    fileInfo.Extension.ToLower() == ".png" || fileInfo.Extension.ToLower() == ".gif")
                {
                    var path = Path.Combine(Server.MapPath(folder), fullFileName);

                    var num = 2;
                    while (System.IO.File.Exists(path))
                    {
                        fullFileName = fileName + "(" + num + ")" + fileInfo.Extension;
                        path = Path.Combine(Server.MapPath(folder), fullFileName);
                        num++;
                    }

                    if (!System.IO.File.Exists(path))
                    {

                        file.SaveAs(path);
                        files = Directory.GetFiles(Server.MapPath("~/Content/image-uploads"));
                        foreach (var image in files)
                        {
                            var imageInfo = new FileInfo(image);

                            var uploadedFile = new UploadedFile() { Title = Path.GetFileName(image) };

                            uploadedFile.PathUrl = ("/Content/image-uploads/") + Path.GetFileName(image);
                            uploadedFiles.Add(uploadedFile);
                            fuVM.UploadedFiles = uploadedFiles;
                            Image imageObj = new Image();
                            imageObj.ImagePath = uploadedFile.PathUrl;
                            _imgRepo.Save(imageObj);
                        }
                        ViewData["message"] = "Bilden har blivit uppladdad";
                        return View(fuVM);  
                        
                    }
                }
            }

            foreach (var image in files)
            {
                var fileInfo = new FileInfo(image);

                var uploadedFile = new UploadedFile() { Title = Path.GetFileName(image) };

                uploadedFile.PathUrl = ("/Content/image-uploads/") + Path.GetFileName(image);
                uploadedFiles.Add(uploadedFile);
                fuVM.UploadedFiles = uploadedFiles;
            }
            ViewData["message"] = "Det gick inte att ladda upp bilden";
            return PartialView("_FileUploadPartialView", fuVM);  
        }

        [HttpPost]
        public bool SelectUpload(FileUploadViewModel fuVM, FormCollection form)
        {
            Post post = new Post();
            Category category = new Category();
            if (fuVM.post != null) {
                post = _postRepo.FindByID(fuVM.post.ID);
                post.Images = new List<Image>();
            } if (fuVM.Category != null)
            {
                category = _categoryRepo.FindByID(fuVM.Category.ID);
                category.Images = new List<Image>();
            }
            
            var listOfImagesPaths = form["images"];
            var arrayOfImagesPaths = listOfImagesPaths.Split(',');
            
            foreach (var path in arrayOfImagesPaths)
            {
                Image image = new Image();
                image.ImagePath = path;
                if (fuVM.post != null)
                {
                    post.Images.Add(image);
                    _postRepo.Save(post);
                } if (fuVM.Category != null)
                {
                    category.Images.Add(image);
                    _categoryRepo.Save(category);
                }
                
            }

            if (fuVM.post != null)
            {
                return true;
            } if (fuVM.Category != null)
            {
                return true;
            } return false;
           
        }

        public ActionResult DeleteFile(string path)
        {
        
        System.IO.File.Delete(Server.MapPath(path));

        var imageObj = _imgRepo.FindAll().Where(i => i.ImagePath == path).FirstOrDefault();
        _imgRepo.Delete(imageObj);

        var fuVM = new FileUploadViewModel();

        var uploadedFiles = new List<UploadedFile>();

        var files = Directory.GetFiles(Server.MapPath("~/Content/image-uploads"));

        foreach (var file in files)
        {
            var fileInfo = new FileInfo(file);

            var uploadedFile = new UploadedFile() { Title = Path.GetFileName(file) };

            uploadedFile.PathUrl = ("/Content/image-uploads/") + Path.GetFileName(file);
            uploadedFiles.Add(uploadedFile);
        }
        fuVM.UploadedFiles = uploadedFiles;

        return View("FileUpload", fuVM);        
        }

    }
}
