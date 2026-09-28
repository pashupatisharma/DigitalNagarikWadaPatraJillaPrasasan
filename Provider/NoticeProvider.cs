
using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using System.Data.Entity;
namespace SchoolWeb.Areas.Administrator.Provider
{
    public class NoticeProvider
    {
      
        private ApplicationDbContext ent = new ApplicationDbContext();
        public void NoticeInsert(NoticeModel model)
        {

            try
            {
                var data = new Notice();
               // data.Image = model.FileName;
                data.Description = model.DescriptionNotice;
                data.Title = model.Title;
                data.CreateDate = model.CreateDate;
                ent.Notice.Add(data);
                ent.SaveChanges();


            }
            catch (Exception ex)
            {
             

            }


        }
        public List<NoticeModel> getNoticeList()
        {
            var list = new List<NoticeModel>();
            try
            {
                list = ent.Notice.Select(m => new NoticeModel()
                {
                   // FileName = m.Image,
                    DescriptionNotice = m.Description,
                    Title=m.Title,
                    CreateDate = m.CreateDate,
                    NoticeId=m.NoticeId

                }).ToList();
            }
            catch (Exception ex)
            {
       

            }

            return list;
        }


        public NoticeModel getNoticeDetail(int? id)
        {
            var list = new NoticeModel();
            try
            {
                list = ent.Notice.Where(x => x.NoticeId == id).Select(m => new NoticeModel()
                {
                   // FileName = m.Image,
                    DescriptionNotice = m.Description,
                    Title=m.Title,
                    CreateDate = m.CreateDate,

                }).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }

            return list;
        }

        internal void UpdateNotie(NoticeModel model)
        {
            try
            {
                var objToEdit = ent.Notice.Where(x => x.NoticeId == model.NoticeId).FirstOrDefault();
                if (objToEdit != null)
                {
                    if(!string.IsNullOrEmpty(model.FileName))
                    //objToEdit.Image = model.FileName;
                    objToEdit.Description = model.DescriptionNotice;
                    objToEdit.Title = model.Title;
                    objToEdit.CreateDate = model.CreateDate;
                   
                    ent.Entry(objToEdit).State = EntityState.Modified;
                    ent.SaveChanges();



                }
            }
            catch (Exception ex)
            {
              

            }





        }

        public string DeleteNotice(int id)
        {

            try
            {
                var objToremove = ent.Notice.Where(x => x.NoticeId == id).FirstOrDefault();
                ent.Notice.Remove(objToremove);
                ent.SaveChanges();
                return "";
            }

            catch (Exception ex)
            {
           

            }

            return "";
        }
        public string DeleteNoticeFiles(int NoticeFilesId)
        {
            try
            {
                NoticeFiles noticeFiles = ((IQueryable<NoticeFiles>)this.ent.NoticeFiles.Where(x => x.NoticeFilesId == NoticeFilesId)).FirstOrDefault<NoticeFiles>();
                this.ent.NoticeFiles.Remove(noticeFiles);
                ((DbContext)this.ent).SaveChanges();
                return noticeFiles.Image;
            }
            catch (Exception ex)
            {
            }
            return "";
        }
    }

}
